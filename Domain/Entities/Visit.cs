namespace Vivigest_backend.Domain.Entities
{
    public class Visit
    {
        public int IdVisit { get; set; }
        public int IdVisitor { get; set; }
        public int IdUnit { get; set; }
        public string Note { get; set; } = string.Empty;
        public string VehiclePlate { get; set; } = string.Empty;
        public int IdUserRegister { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relations
        public Visitor Visitor { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
