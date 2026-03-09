namespace Vivigest_backend.Domain.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public int IdPerson { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public bool Activated { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relations
        public Person Person { get; set; } = null!;
        public ICollection<UserRol> UserRols { get; set; } = new List<UserRol>();
    }
}
