using MasterHub.IServices;
using MasterHub.Model;
using MasterHub.ModelDTO;
using MasterHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> AddUser(Auth auth)
        {
            var apiresponse = await _authService.AddUserAsync(auth);
            if (apiresponse.Sucess)
                return Ok(apiresponse);
            else
                return BadRequest(apiresponse);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDTO login)
        {
            var apiresponse = await _authService.LoginAsync(login);
            if (apiresponse.Sucess)
                return Ok(apiresponse);
            else
                return BadRequest(apiresponse);
        }
    }
}