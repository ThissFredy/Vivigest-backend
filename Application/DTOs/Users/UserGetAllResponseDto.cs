namespace Vivigest_backend.Application.DTOs.Users
{
    public class UserGetAllResponseDto
    {
        public IEnumerable<UserGetByIdResponseDto> Users { get; set; } = new List<UserGetByIdResponseDto>();
    }
}
