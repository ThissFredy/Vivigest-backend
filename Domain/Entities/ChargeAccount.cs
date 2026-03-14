namespace Vivigest_backend.Domain.Entities
{
    public class ChargeAccount
    {
        public int IdChargeAccount { get; set; }
        public int IdUnit { get; set; }
        public int IdPaymentPeriod { get; set; }
        public decimal Amount { get; set; }
        public string concept { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Unit Unit { get; set; } = null!;
        public PaymentPeriod PaymentPeriod { get; set; } = null!;
    }
}
