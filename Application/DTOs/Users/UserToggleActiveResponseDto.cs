namespace Vivigest_backend.Application.DTOs.Users
{
    public class UserToggleActiveResponseDto
    {
        public int IdUser { get; set; }
        public bool Activated { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
