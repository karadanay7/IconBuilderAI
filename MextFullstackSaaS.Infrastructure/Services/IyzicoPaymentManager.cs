﻿using Iyzipay.Model;
using Iyzipay.Request;
using MextFullstackSaaS.Application;
using MextFullstackSaaS.Application.Common.Interfaces;

using MextFullstackSaaS.Domain;
using MextFullstackSaaS.Domain.Settings;
using Microsoft.Extensions.Options;
using Options = Iyzipay.Options;

namespace MextFullstackSaaS.Infrastructure.Services
{
    public class IyzicoPaymentManager : IPaymentService
    {
        private readonly Options _options;
        private readonly ICurrentUserService _currentUserService;

        public IyzicoPaymentManager(IOptions<IyzicoSettings> settings, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;

            _options = new Options
            {
                ApiKey = settings.Value.ApiKey,
                SecretKey = settings.Value.SecretKey,
                BaseUrl = settings.Value.BaseUrl
            };
        }

        private const int OneCreditPrice = 10;
        private const string CallbackUrl = "http://localhost:5121/api/Payments/complete-payment/";

        public PaymentsCreateCheckoutFormResponse CreateCheckoutForm(PaymentsCreateCheckoutFormRequest userRequest)
        {
            var price = userRequest.Credits * OneCreditPrice;
            var paidPrice = price;
            var conversationId = Guid.NewGuid().ToString();
            var basketId = Guid.NewGuid().ToString();

            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = conversationId,
                Price = price.ToString(),
                PaidPrice = paidPrice.ToString(),
                Currency = Currency.TRY.ToString(),
                BasketId = basketId,
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = CallbackUrl
            };

            List<int> enabledInstallments = new List<int>();

            enabledInstallments.Add(3);

            enabledInstallments.Add(6);

            enabledInstallments.Add(9);

            request.EnabledInstallments = enabledInstallments;

            // UserAddress

            Buyer buyer = new Buyer
            {
                Id = _currentUserService.UserId.ToString(),
                Name = userRequest.UserPaymentDetail.FirstName,
                Surname = userRequest.UserPaymentDetail.LastName,
                GsmNumber = userRequest.UserPaymentDetail.PhoneNumber,
                Email = userRequest.UserPaymentDetail.Email,
                IdentityNumber = "74300864791",
                LastLoginDate = userRequest.UserPaymentDetail.LastLoginDate.ToString("yyyy-MM-dd HH:mm:ss"),
                RegistrationDate = "2013-04-21 15:12:09",
                RegistrationAddress = userRequest.UserPaymentDetail.Address,
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732"
            };

            // UserAddress
            request.Buyer = buyer;

            Address billingAddress = new Address
            {
                ContactName = $"{userRequest.UserPaymentDetail.FirstName} {userRequest.UserPaymentDetail.LastName}",
                City = "Istanbul",
                Country = "Turkey",
                Description = userRequest.UserPaymentDetail.Address,
                ZipCode = "34742"
            };
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            BasketItem firstBasketItem = new BasketItem
            {
                Id = "BI101",
                Name = $"IconBuilderAI {userRequest.Credits} credits",
                ItemType = BasketItemType.VIRTUAL.ToString(),
                Price = paidPrice.ToString(),
                Category1 = "Credits"
            };
            basketItems.Add(firstBasketItem);

            request.BasketItems = basketItems;

            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, _options);

            // Check the response if it is not successful throw an exception

            return MapCheckoutFormInitializeResponse(checkoutFormInitialize, price, paidPrice, conversationId, basketId);
        }

        private PaymentsCreateCheckoutFormResponse MapCheckoutFormInitializeResponse(CheckoutFormInitialize checkoutFormInitialize, decimal price, decimal paidPrice, string conversationId, string basketId)
        {
            return new PaymentsCreateCheckoutFormResponse
            {
                Price = price,
                PaidPrice = paidPrice,
                ConversationId = conversationId,
                BasketId = basketId,
                Token = checkoutFormInitialize.Token,
                TokenExpireTime = checkoutFormInitialize.TokenExpireTime,
                CheckoutFormContent = checkoutFormInitialize.CheckoutFormContent,
                PaymentPageUrl = checkoutFormInitialize.PaymentPageUrl
            };
        }

        public PaymentsCheckPaymentByTokenResponse CheckPaymentByToken(string token)
        {
            var conversationId = Guid.NewGuid().ToString();

            RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest
            {
                ConversationId = conversationId,
                Token = token,
                Locale = Locale.TR.ToString()
            };

            CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, _options);

            return new PaymentsCheckPaymentByTokenResponse()
            {
                ConversationId = conversationId,
                IsSuccess = checkoutForm.PaymentStatus.ToLowerInvariant() == "success",
                PaymentStatus = checkoutForm.PaymentStatus,
                ErrorCode = checkoutForm.ErrorCode,
                ErrorGroup = checkoutForm.ErrorGroup,
                ErrorMessage = checkoutForm.ErrorMessage
            };


        }
    }
}
