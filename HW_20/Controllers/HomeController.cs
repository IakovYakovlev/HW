using HW_20.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_20.Controllers
{
    public class HomeController : Controller
    {
        public IPhoneBookRepository _phoneBookRepository;
        
        public HomeController(IPhoneBookRepository phoneBookRepository)
        {
            _phoneBookRepository = phoneBookRepository;
        }

        public IActionResult Index() => View(_phoneBookRepository.GetAllData);

        public IActionResult Create()
        {
            ViewBag.CreateModel = true;
            return View("About", new PhoneBook());
        }

        [HttpPost]
        public IActionResult Create(PhoneBook phoneBook)
        {
            _phoneBookRepository.Add(phoneBook);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateModel = false;
            return View("About", _phoneBookRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Edit(PhoneBook phoneBook)
        {
            _phoneBookRepository.Update(phoneBook);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _phoneBookRepository.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
