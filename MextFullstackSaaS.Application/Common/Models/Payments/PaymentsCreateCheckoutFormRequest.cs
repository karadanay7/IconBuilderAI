using MextFullstackSaaS.Domain;

namespace MextFullstackSaaS.Application;

public class PaymentsCreateCheckoutFormRequest
{
    public UserPaymentDetail UserPaymentDetail { get; set; }
    public int Credits { get; set; }

    public PaymentsCreateCheckoutFormRequest(UserPaymentDetail userPaymentDetail, int credits)
    {
        UserPaymentDetail = userPaymentDetail;
        Credits = credits;
    }
    public PaymentsCreateCheckoutFormRequest()
    {
    }
    

}
