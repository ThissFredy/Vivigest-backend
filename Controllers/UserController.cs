using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vivigest_backend.Application.DTOs.Users;
using Vivigest_backend.Application.Interfaces.IService;

namespace Vivigest_backend.Controllers
{
    /// <summary>
    /// Controller for managing system users.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>An IActionResult containing the list of users.</returns>
        [HttpGet("get")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.getAllUsersAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error.Description);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves the current authenticated user's profile.
        /// </summary>
        /// <returns>An IActionResult containing the user's profile details.</returns>
        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _userService.getUserByIdAsync(userId);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error.Description);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Updates the current authenticated user's profile.
        /// </summary>
        /// <param name="request">The request body containing the updated details.</param>
        /// <returns>An IActionResult containing the updated profile details.</returns>
        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateRequestDto request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _userService.updateUserAsync(userId, request);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error.Description);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Deactivates the current authenticated user's account (logical delete).
        /// </summary>
        /// <returns>An IActionResult containing the deactivation result.</returns>
        [HttpPut("deactivate")]
        [Authorize]
        public async Task<IActionResult> DeactivateProfile()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _userService.deactivateUserAsync(userId);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error.Description);
            }

            return Ok(result.Value);
        }
    }
}
