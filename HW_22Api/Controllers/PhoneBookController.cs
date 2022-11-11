using HW_22Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_22Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        public IPhoneBookRepository _phoneBookRepository;

        public PhoneBookController(IPhoneBookRepository phoneBookRepository)
        {
            _phoneBookRepository = phoneBookRepository;
        }

        // /api/phonebook/getall
        [HttpGet("getall")]
        public IEnumerable<PhoneBook> Get() => _phoneBookRepository.GetAllData;

        // /api/phonebook/create
        [HttpPost("Create")]
        public IResult Create([FromBody]PhoneBook phoneBook)
        {
            if (phoneBook == null) return Results.NotFound();

            _phoneBookRepository.Add(phoneBook);

            return Results.Ok();
        }

        // /api/phonebook/edit
        [HttpPut("Edit")]
        public IResult Edit([FromBody]PhoneBook phoneBook)
        {
            if (phoneBook == null) return Results.NotFound();

            _phoneBookRepository.Update(phoneBook);

            return Results.Ok();
        }

        // /api/phonebook/delete/{id}
        [HttpDelete("Delete/{id}")]
        public IResult Delete(int id)
        {
            _phoneBookRepository.Remove(id);
            return Results.Ok();
        }
    }
}
