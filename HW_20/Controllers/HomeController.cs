using HW_20.Services;
using IdentityShared;
using Microsoft.AspNetCore.Mvc;

namespace HW_20.Controllers
{
    public class HomeController : Controller
    {
        IPhoneBookRepository _phoneBook;

        public HomeController(IPhoneBookRepository phoneBook)
        {
            _phoneBook = phoneBook;
        }

        public IActionResult Index() => View(_phoneBook.GetAllData().Result);

        public IActionResult Create()
        {
            if (Request.Cookies["Token"] == null)
                return View("../Account/AccessDenied");

            ViewBag.CreateModel = true;
            return View("About", new PhoneBook());
        }

        [HttpPost]
        public IActionResult Create(PhoneBook phoneBook)
        {
            var result = _phoneBook.Add(phoneBook, Request.Cookies["Token"]).Result;

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return View("../Account/AccessDenied");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateModel = false;

            var result = _phoneBook.Get(id, Request.Cookies["Token"]).Result;

            if (result == null)
                return View("../Account/AccessDenied");

            return View("About", result);
        }

        [HttpPost]
        public IActionResult Edit(PhoneBook phoneBook)
        {
            var result = _phoneBook.Update(phoneBook, Request.Cookies["Token"]).Result;

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _phoneBook.Remove(id, Request.Cookies["Token"]);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View("../Account/AccessDenied");
        }
    }
}
