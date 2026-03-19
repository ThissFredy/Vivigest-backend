namespace Vivigest_backend.Application.DTOs.Users
{
    public class UserResponseDto
    {
        public int IdUser { get; set; }
        public string Names { get; set; } = string.Empty;
        public string LastNames { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
