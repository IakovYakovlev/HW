using IdentityShared;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HW_22Api.Services
{
    public interface IUserService
    {
        Task<UsersManager> GetAllUsers();

        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

        Task<UserManagerResponse> EditAsync(Users item);

        Task<UserManagerResponse> DeleteUserAsync(string id);
    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IUserValidator<IdentityUser> _userValidator;
        private IPasswordValidator<IdentityUser> _passwordValidator;
        private IPasswordHasher<IdentityUser> _passwordHasher;
        private IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, 
            IUserValidator<IdentityUser> userValidator, IConfiguration configuration, IPasswordValidator<IdentityUser> passwordValidator,
            IPasswordHasher<IdentityUser> passwordHasher)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userValidator = userValidator;
            _configuration = configuration;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsersManager> GetAllUsers()
        {
            List<Users> users = await _userManager.Users.Select(x => new Users
            {
                Id = x.Id,
                Email = x.Email
            }).ToListAsync();

            if(users != null)
            {
                return new UsersManager 
                { 
                    Message = "Список с пользователями",
                    IsSuccess = true,
                    Users = users 
                };
            }

            return new UsersManager
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
            var role = await _userManager.GetRolesAsync(user);
            
            if (user == null)
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
                new Claim(ClaimTypes.Role, role.First())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);



            return new UserManagerResponse
            {
                Message = tokenAsString,
                UserEmail = user.Email,
                UserRole = role.First(),
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<UserManagerResponse> EditAsync(Users item)
        {
            IdentityUser user = await _userManager.FindByIdAsync(item.Id);

            if(user != null)
            {
                user.Email = item.Email;

                IdentityResult validEmail = await _userValidator.ValidateAsync(_userManager, user);

                IdentityResult validPass = null;

                if (!string.IsNullOrEmpty(item.Password))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager, user, item.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, item.Password);
                    }
                }

                if((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && item.Password != String.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return new UserManagerResponse
                        {
                            Message = $"Данные пользователя {item.Id} изменены",
                            IsSuccess = true,
                        };
                    }
                }
                return new UserManagerResponse
                {
                    Message = $"",
                    IsSuccess = false,
                };
            }
            else
            {
                return new UserManagerResponse
                {
                    Message = $"",
                    IsSuccess = false,
                };
            }
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
                else
                    return new UserManagerResponse
                    {
                        Message = $"Пользователесь {user.Email} не удален",
                        IsSuccess = false,
                    };
            }
            else
            {
                return new UserManagerResponse
                {
                    Message = $"Пользователесь {id} не найден",
                    IsSuccess = false,
                };
            }
        }
    }
}
