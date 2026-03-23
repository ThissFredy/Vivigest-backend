namespace Vivigest_backend.Application.DTOs.Users
{
    public class RegisterUserResponseDto
    {
        public int IdUser { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
