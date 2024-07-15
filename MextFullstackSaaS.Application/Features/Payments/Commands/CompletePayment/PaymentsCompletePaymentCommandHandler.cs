using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Domain;
using MextFullstackSaaS.Domain.Entities;
using MextFullstackSaaS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MextFullstackSaaS.Application;

public class PaymentsCompletePaymentCommandHandler : IRequestHandler<PaymentsCompletePaymentCommand, ResponseDto<bool>>
{
    private readonly IPaymentService _paymentService;
    private readonly IApplicationDbContext _applicationDbContext;
    public PaymentsCompletePaymentCommandHandler(IPaymentService paymentService, IApplicationDbContext applicationDbContext)
    {
        _paymentService = paymentService;
        _applicationDbContext = applicationDbContext;
    }
    public async Task<ResponseDto<bool>> Handle(PaymentsCompletePaymentCommand request, CancellationToken cancellationToken)
    {
        var userPayment = await _applicationDbContext
               .UserPayments
               .FirstOrDefaultAsync(x => x.Token == request.Token, cancellationToken);

        var checkResponse = _paymentService.CheckPaymentByToken(request.Token);

        UpdateUserPayment(ref userPayment, checkResponse.IsSuccess);

        var userPaymentHistory = CreateUserPaymentHistory(userPayment, checkResponse);

        _applicationDbContext.UserPaymentHistories.Add(userPaymentHistory);

        if (checkResponse.IsSuccess)
        await UpdateUserBalanceAsync(amount:10, userId: userPayment.UserId, cancellationToken);
       var message = checkResponse.IsSuccess ? "Your payment process has been successful!" : "Your payment process has been failed!";

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<bool>(checkResponse.IsSuccess, "Your payment process has been successful!");
    }

      private async Task UpdateUserBalanceAsync(int amount, Guid userId, CancellationToken cancellationToken)
        {


            var userBalance = await _applicationDbContext
                .UserBalances
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            userBalance.Credits += amount;
            userBalance.ModifiedByUserId = userId.ToString();
            userBalance.ModifiedOn = DateTimeOffset.UtcNow;

            _applicationDbContext.UserBalances.Update(userBalance);

            var userBalanceHistory = new UserBalanceHistory
            {
                Id = Guid.NewGuid(),
                UserBalanceId = userBalance.Id,
                Amount = amount,
                CreatedByUserId = userId.ToString(),
                CreatedOn = DateTimeOffset.UtcNow,
                CurrentCredits = userBalance.Credits - amount,
                PreviousCredits = userBalance.Credits,
                Type = UserBalanceHistoryType.AddCredits
            };

            _applicationDbContext.UserBalanceHistories.Add(userBalanceHistory);

        }

    private void UpdateUserPayment(ref UserPayment userPayment, bool isPaymentSuccess )
    {
        userPayment.Status = PaymentStatus.Success;
        userPayment.ModifiedByUserId = userPayment.UserId.ToString();
        userPayment.ModifiedOn = DateTimeOffset.UtcNow;

    }
    private UserPaymentHistory CreateUserPaymentHistory(UserPayment userPayment, PaymentsCheckPaymentByTokenResponse checkPaymentByTokenResponse)
    {
        return new UserPaymentHistory
        {
            Id = Guid.NewGuid(),
            ConversationId = checkPaymentByTokenResponse.ConversationId,
            CreatedByUserId = userPayment.UserId.ToString(),
            CreatedOn = DateTimeOffset.UtcNow,
            Status = PaymentStatus.Success,
            UserPaymentId = userPayment.Id
        };
    }
}
