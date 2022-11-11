using HW_22Api.Services;
using IdentityShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_22Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        // /api/auth/getall
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAllUsers();
            
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        } 

        // /api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model)
        {
            var result = await _userService.RegisterUserAsync(model);

            if(result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        // /api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginViewModel model)
        {
            var result = await _userService.LoginUserAsync(model);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // /api/auth/delete/{id}
        //[Authorize(Roles = "Admins")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
