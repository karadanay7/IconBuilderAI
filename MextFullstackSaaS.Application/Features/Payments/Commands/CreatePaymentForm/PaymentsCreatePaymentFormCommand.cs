using MediatR;
using MextFullstackSaaS.Application.Common.Models;

namespace MextFullstackSaaS.Application;

public class PaymentsCreatePaymentFormCommand:IRequest<ResponseDto<PaymentsCreatePaymentFormDto>>
{
  public int Credits { get; set; }
}
