using HW_22Api.Services;
using IdentityShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [AllowAnonymous]
        [HttpGet("getall")]
        public IEnumerable<PhoneBook> GetAll() => _phoneBookRepository.GetAllData;

        // /api/phonebook/get
        [Authorize(Roles = "Admins")]
        [HttpGet("Get/{id}")]
        public PhoneBook Get(int id) => _phoneBookRepository.Get(id);

        // /api/phonebook/create
        [Authorize(Roles = "Admins, User")]
        [HttpPost("Create")]
        public IResult Create([FromBody]PhoneBook phoneBook)
        {
            if (phoneBook == null) return Results.NotFound();

            _phoneBookRepository.Add(phoneBook);

            return Results.Ok();
        }

        // /api/phonebook/edit
        [Authorize(Roles = "Admins")]
        [HttpPut("Edit")]
        public IResult Edit([FromBody]PhoneBook phoneBook)
        {
            if (phoneBook == null) return Results.NotFound();

            _phoneBookRepository.Update(phoneBook);

            return Results.Ok();
        }

        // /api/phonebook/delete/{id}
        [Authorize(Roles = "Admins")]
        [HttpDelete("Delete/{id}")]
        public IResult Delete(int id)
        {
            _phoneBookRepository.Remove(id);
            return Results.Ok();
        }
    }
}
