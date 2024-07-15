using MextFullstackSaaS.Domain.Common;

namespace MextFullstackSaaS.Domain;

public class UserPaymentHistory : EntityBase<Guid>
{
     public string? ConversationId { get; set; }
     public PaymentStatus Status { get; set; }

    public string? Note { get; set; }
    public Guid UserPaymentId { get; set; }
    public UserPayment UserPayment { get; set; }

}
