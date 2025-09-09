using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Application.Interface;
using PL.Domain.Dto;

namespace PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto obj)
        {
            if (string.IsNullOrEmpty(obj.Username) || string.IsNullOrEmpty(obj.PasswordHash))
                return BadRequest(new { message = "Email e senha são obrigatórios." });

            var loginResponse = await _userService.AuthenticateAsync(obj.Username, obj.PasswordHash);

            if (loginResponse == null)
                return Unauthorized(new { message = "Credenciais inválidas." });

            return Ok(loginResponse);
        }

        [HttpPost("register")]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] RegisterDto obj)
        {
            try
            {
                var userResult = await _userService.Registeruser(obj);
                if (!userResult.Succeded || !userResult.HasData || userResult.StatusCode != System.Net.HttpStatusCode.OK)
                    return StatusCode(userResult.StatusCode.GetHashCode(), userResult.LastMessage);

                return Ok(userResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno durante o registro: {ex.Message}");
            }
        }
    }
}