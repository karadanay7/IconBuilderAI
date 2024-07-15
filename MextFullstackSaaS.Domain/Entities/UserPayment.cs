using MextFullstackSaaS.Domain.Common;
using MextFullstackSaaS.Domain.Identity;

namespace MextFullstackSaaS.Domain;

public class UserPayment:EntityBase<Guid>
{
   
    public string BasketId { get; set; }
    public string Token { get; set; }
    public string Price { get; set; }
    public string PaidPrice { get; set; }
    public CurrencyCode CurrencyCode { get; set; }

    public PaymentStatus Status { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public string? Note { get; set; }
    public decimal? RefundedAmount { get; set; }
    public UserPaymentDetail UserPaymentDetail{ get; set; }

    public ICollection<UserPaymentHistory> Histories { get; set; }

}
