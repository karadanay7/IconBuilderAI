namespace MextFullstackSaaS.Application;

public class PaymentsCreatePaymentFormDto
{
 public string PaymentPageUrl { get; set; }

 public PaymentsCreatePaymentFormDto(string paymentPageUrl)
 {
     PaymentPageUrl = paymentPageUrl;
 }
 public PaymentsCreatePaymentFormDto()
 {
 }
}
