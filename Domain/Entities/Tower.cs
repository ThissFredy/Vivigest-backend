namespace Vivigest_backend.Domain.Entities
{
    public class Tower
    {
        public int IdTower { get; set; }
        public int IdComplex { get; set; }
        public string NameTower { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Complex Complex { get; set; } = null!;
    }
}
