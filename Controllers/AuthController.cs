using Microsoft.AspNetCore.Mvc;
using Vivigest_backend.Application.DTOs.Users;
using Vivigest_backend.Application.Interfaces.IService;

namespace Vivigest_backend.Controllers
{
    /// <summary>
    /// Controller for handling authentication and user registration.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticates a user and returns a token.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>An IActionResult containing the user details upon success.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _userService.loginAsync(request);

            // Check if authentication failed
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

            // Set the authentication token in a cookie
            SetTokenCookie(result.Value.Token);
            SetRefreshTokenCookie(result.Value.RefreshToken);

            return Ok(result.Value.User);
        }

        /// <summary>
        /// Registers a new user and returns a token.
        /// </summary>
        /// <param name="request">The registration request containing user details.</param>
        /// <returns>An IActionResult containing the registered user details upon success.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto request)
        {
            var result = await _userService.registerAsync(request);

            // Check if registration failed
            if (!result.IsSuccess)
            {
                if (result.Error.Code == "AlreadyExists")
                {
                    return Unauthorized(new { message = result.Error.Description });
                }
                return BadRequest("Something bad went wrong");
            }

            // Set the authentication token in a cookie
            SetTokenCookie(result.Value.Token);
            SetRefreshTokenCookie(result.Value.RefreshToken);

            return Ok(result.Value.User);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { message = "Refresh token is missing" });
            }
            var result = await _userService.refreshTokenAsync(refreshToken);
            if (!result.IsSuccess)
            {
                return Unauthorized(new { message = result.Error.Description });
            }
            // Set the new authentication token in a cookie
            SetTokenCookie(result.Value.Token);
            SetRefreshTokenCookie(result.Value.RefreshToken);

            return Ok(new {message = "Token refreshed succesfully"});
        }



        /// <summary>
        /// Logs out the current user by deleting the token cookie.
        /// </summary>
        /// <returns>An IActionResult confirming the logout operation.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Remove the authentication token cookie
            Response.Cookies.Delete("token");
            var refreshToken = Request.Cookies["refreshToken"];
            await _userService.revokeRefreshToken(refreshToken);
            Response.Cookies.Delete("refreshToken");

            return Ok(new { message = "Sesión cerrada correctamente" });
        }


        /// <summary>
        /// Sets the JWT token in an HTTP-only cookie.
        /// </summary>
        /// <param name="token">The JWT token to store.</param>
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };

            Response.Cookies.Append("token", token, cookieOptions);
        }

        /// <summary>
        /// Sets the JWT token in an HTTP-only cookie.
        /// </summary>
        /// <param name="token">The JWT token to store.</param>
        private void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
