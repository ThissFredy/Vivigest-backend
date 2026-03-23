using Microsoft.AspNetCore.Mvc;
using Vivigest_backend.Application.DTOs.Users;
using Vivigest_backend.Application.Interfaces.IService;

namespace Vivigest_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _userService.loginAsync(request);

            if (!result.IsSuccess)
            {
                if (result.Error.Code == "InvalidCredentials")
                {
                    return Unauthorized(new { message = result.Error.Description });
                }
                
                if (result.Error.Code == "NotFound")
                {
                    return NotFound(new { message = result.Error.Description });
                }

                return BadRequest("Something bad went wrong");
            }

            SetTokenCookie(result.Value.Token);

            return Ok(result.Value.User);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto request)
        {
            var result = await _userService.registerAsync(request);

            if (!result.IsSuccess)
            {

                if (result.Error.Code == "AlreadyExists")
                {
                    return Unauthorized(new { message = result.Error.Description });
                }
                return BadRequest("Something bad went wrong");
            }

            SetTokenCookie(result.Value.Token);

            return Ok(result.Value.User);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // TODO: CAMBIAR EN PRODUCCIÓN
                SameSite = SameSiteMode.Lax, // TODO: CAMBIAR EN PRODUCCIÓN
                Expires = DateTime.UtcNow.AddHours(1)
            };

            Response.Cookies.Append("token", token, cookieOptions);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");

            return Ok(new { message = "Sesión cerrada correctamente" });
        }
    }
}
