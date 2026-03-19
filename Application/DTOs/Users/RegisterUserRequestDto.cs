namespace Vivigest_backend.Application.DTOs.Users
{
    public class RegisterUserRequestDto
    {
        public int IdDocumentType { get; set; }
        public int NitNumber { get; set; }
        public string Names { get; set; } = string.Empty;
        public string LastNames { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
