using HW_20.Services;
using IdentityShared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace HW_20.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(string userRole)
        {
            if (userRole != null && userRole == "Admins")
                return View(_userService.GetAllUsers().Result);

            return View("AccessDenied");
        }

        public IActionResult Register()
        {
            return View("Register", new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if(model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(nameof(model.ConfirmPassword), "Пароли не совпадают.");
            }
            if (ModelState.IsValid)
            {
                var result = _userService.RegisterUserAsync(model).Result;
            }
            else
            {
                return View();
            }
            

            return RedirectToAction(nameof(Index), new { userRole = "Admins"});
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var responseBody = _userService.LoginUserAsync(model).Result;

            try
            {
                var responseObject = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

                if (responseObject.IsSuccess)
                {
                    CookieOptions cookOpt = new CookieOptions();
                    cookOpt.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("Token", responseObject.Message, cookOpt);
                    Response.Cookies.Append("UserRole", responseObject.UserRole, cookOpt);
                    Response.Cookies.Append("UserEmail", responseObject.UserEmail, cookOpt);

                    return Redirect("/");
                }
            }
            catch
            {
                return View(model);
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            if(Request.Cookies["Token"] != null)
            {
                Response.Cookies.Delete("Token");
                Response.Cookies.Delete("UserRole");
                Response.Cookies.Delete("UserEmail");
            }

            return Redirect("/");
        }

        public IActionResult Delete(string id = null)
        {
            if(id != null)
            {
                _userService.DeleteUserAsync(id);
            }
            return RedirectToAction(nameof(Index), new { userRole = "Admins" });
        }
    }
}
