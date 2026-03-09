namespace Vivigest_backend.Domain.Entities
{
    public class UserRol
    {
        public int IdUser { get; set; }
        public int IdRol { get; set; }

        // Relations
        public User User { get; set; } = null!;
        public Rol Rol { get; set; } = null!;
    }
}
