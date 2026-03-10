namespace Vivigest_backend.Domain.Entities
{
    public class Correspondence
    {
        public int IdCorrespondence { get; set; }
        public int IdUnit { get; set; }
        public int IdCorrespondenceType { get; set; }
        public string Sender { get; set; } = string.Empty;
        public DateTime DateReceipt { get; set; }
        public int IdCorrespondenceState { get; set; }
        public DateTime? DateNotified { get; set; }
        public DateTime? DateDelivered { get; set; }
        public string DeliveredTo { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int IdRegisteredBy { get; set; }

        // Relations
        public Unit Unit { get; set; } = null!;
        public CorrespondenceType CorrespondenceType { get; set; } = null!;
        public CorrespondenceState CorrespondenceState { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
