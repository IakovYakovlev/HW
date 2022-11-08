using HW_22Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_22Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        IPhoneBookRepository _phonebookRepository;

        public PhoneBookController(IPhoneBookRepository phonebookRepository)
        {
            _phonebookRepository = phonebookRepository;
        }
        
        // /api/phonebook/string
        [HttpGet("string")]
        public string GetString() => "This is a string response";
    }
}
