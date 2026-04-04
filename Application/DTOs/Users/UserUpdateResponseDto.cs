namespace Vivigest_backend.Application.DTOs.Users
{
    public class UserUpdateResponseDto
    {
        public int IdUser { get; set; }
        public string Names { get; set; } = string.Empty;
        public string LastNames { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
