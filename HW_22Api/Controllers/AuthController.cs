using HW_22Api.Services;
using IdentityShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_22Api.Controllers
{
    [Authorize(Roles = "Admins")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService) => _userService = userService;

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
        [AllowAnonymous]
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

        // /api/auth/edit
        [HttpPut("Edit")]
        public async Task<IActionResult> EditAsync([FromBody]Users user)
        {
            var result = await _userService.EditAsync(user);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // /api/auth/delete/{id}
        [Authorize(Roles = "Admins")]
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
