namespace Vivigest_backend.Domain.Entities
{
    public class Unit
    {
        public int IdUnit { get; set; }
        public int IdTower { get; set; }
        public string CodeUnit { get; set; } = string.Empty;
        public string FloorUnit { get; set; } = string.Empty;
        public decimal AreaM2Unit { get; set; }
        public int IdState { get; set; }
        public DateTime CreatedAt { get; set; }

        public State State { get; set; } = null!;
    }
}
