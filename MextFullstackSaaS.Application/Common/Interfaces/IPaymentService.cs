namespace MextFullstackSaaS.Application;

public interface IPaymentService
{
     PaymentsCreateCheckoutFormResponse CreateCheckoutForm(PaymentsCreateCheckoutFormRequest userRequest);
   PaymentsCheckPaymentByTokenResponse CheckPaymentByToken (string token);
    
}
