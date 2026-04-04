namespace Vivigest_backend.Application.DTOs.Users
{
    public class UserGetByIdResponseDto
    {
        public int IdUser { get; set; }
        public int IdPerson { get; set; }
        public int IdDocumentType { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string LastNames { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activated { get; set; }
    }
}
