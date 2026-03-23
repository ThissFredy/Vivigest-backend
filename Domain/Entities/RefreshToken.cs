namespace Vivigest_backend.Domain.Entities
{
    public class RefreshToken
    {
        public int IdToken { get; set; }
        public int IdUser { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool IsExpired { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
    }
}
