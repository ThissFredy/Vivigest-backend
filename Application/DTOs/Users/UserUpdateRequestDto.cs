namespace Vivigest_backend.Application.DTOs.Users
{
    public class UserUpdateRequestDto
    {
        public int IdDocumentType { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string LastNames { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
