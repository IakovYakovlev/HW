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

        public IActionResult About(int id) => View(_phoneBookRepository.Get(id));
    }
}
