using Microsoft.AspNetCore.Mvc;
using auth_session.API.Models.Auth;
using auth_session.API.Filters.Response;
using auth_session.Core.Interfaces.Auth;

namespace auth_session.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.AuthenticateAsync(dto.Username, dto.Password);
            if (user == null) return NotFound(new Response<string>(error: "User not found"));

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            if (dto.RememberMe ?? false) HttpContext.Session.SetString("RememberMe", "true");

            return Ok(new Response<string>(data: "Login successful", message: "Login successful"));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var response = await _userService.RegisterAsync(dto.Username, dto.Password, dto.Role);
            return Ok(new Response<string>(data: response, message: response));
        }

        [HttpPut("change_password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var response = await _userService.ChangePasswordAsync(dto.OldPassword, dto.NewPassword);
            return Ok(new Response<string>(data: response, message: response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound(new Response<string>(error: "User not found"));

            var userDto = new UserDto(user.Id, user.Username, user.Role, user.CreatedAt, user.IsActive);
            return Ok(new Response<UserDto>(userDto, message: "Get user successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.DeleteAsync(id);
            return Ok(new Response<string>(data: response, message: response));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAllAsync();
            return Ok(new Response<List<UserDto>>(data: [.. response.Select(user => new UserDto(user.Id, user.Username, user.Role, user.CreatedAt, user.IsActive))], message: "Get all user(s) successfully"));
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("RememberMe");
            return Ok(new Response<string>(data: "Logged out successfully", message: "Logged out successfully"));
        }
    }
}