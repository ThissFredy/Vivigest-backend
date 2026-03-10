using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Vivigest_backend.Domain.Entities
{
    public class Payment
    {
        public int IdPayment { get; set; }
        public int IdUnit { get; set; }
        public int IdPaymentPeriod { get; set; }
        public decimal Amount { get; set; }
        public int IdPaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public int IdRegisterdBy { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relations
        public Unit Unit { get; set; } = null!;
        public PaymentPeriod PaymentPeriod { get; set; } = null!;
        public PaymentMethod PaymentMethod { get; set; } = null!;
        public User RegisteredBy { get; set; } = null!;
    }
}
