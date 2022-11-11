using IdentityShared;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW_22Api.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> GetAllUsers();

        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

        Task<UserManagerResponse> DeleteUserAsync(string id);
    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<UserManagerResponse> GetAllUsers()
        {
            List<Users> users = await _userManager.Users.Select(x => new Users
            {
                Id = x.Id,
                Email = x.Email
            }).ToListAsync();

            if(users != null)
            {
                new AllUsers { Users = users };
                return new UserManagerResponse
                {
                    Message = "Список с пользователями",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "Список с пользователями пуст",
                IsSuccess = false,
            };
        } 
        
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null) throw new NullReferenceException("Пустое значение");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Пароль не совпадает",
                    IsSuccess = false,
                };

            string role = _configuration["Admin:UserRole:Role"];
            if (await _roleManager.FindByNameAsync(role) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(identityUser);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, role);

                return new UserManagerResponse
                {
                    Message = "Успешная регистрация",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "Ошибка регистрации",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                return new UserManagerResponse
                {
                    Message = "Пользователесь не существует",
                    IsSuccess = false,
                };
            }

            var claims = new[]
{
                    new Claim("Email", model.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<UserManagerResponse> DeleteUserAsync(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            
            if(user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return new UserManagerResponse
                    {
                        Message = $"Пользователесь {user.Email} удален",
                        IsSuccess = true,
                    };
                
                return new UserManagerResponse
                {
                    Message = $"Пользователесь {user.Email} не удален",
                    IsSuccess = false,
                };
            }
            return new UserManagerResponse
            {
                Message = $"Пользователесь {id} не найден",
                IsSuccess = false,
            };
        }
    }
}
