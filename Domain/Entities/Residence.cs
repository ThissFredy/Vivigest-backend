namespace Vivigest_backend.Domain.Entities
{
    public class Residence
    {
        public int IdResidence { get; set; }
        public int IdUser { get; set; }
        public int IdUnit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public User User { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
    }
}
