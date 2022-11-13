using IdentityShared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HW_20.Controllers
{
    public class HomeController : Controller
    {
        HttpClient _client;

        public HomeController(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("http://localhost:16962/api/phonebook/getall");
            var responseBody = await response.Content.ReadAsStringAsync();
            var phoneBooks = JsonConvert.DeserializeObject<IEnumerable<PhoneBook>>(responseBody);
            return View(phoneBooks);
        }

        public IActionResult Create()
        {
            ViewBag.CreateModel = true;
            return View("About", new PhoneBook());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PhoneBook phoneBook)
        {
            var jsonPhoneBook = JsonConvert.SerializeObject(phoneBook);
            StringContent content = new StringContent(jsonPhoneBook, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("http://localhost:16962/api/phonebook/create", content);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.CreateModel = false;

            var response = await _client.GetAsync($"http://localhost:16962/api/phonebook/get/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var phoneBook = JsonConvert.DeserializeObject<PhoneBook>(responseBody);

            return View("About", phoneBook);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PhoneBook phoneBook)
        {
            var jsonPhoneBook = JsonConvert.SerializeObject(phoneBook);
            StringContent content = new StringContent(jsonPhoneBook, Encoding.UTF8, "application/json");
            await _client.PutAsync("http://localhost:16962/api/phonebook/edit", content);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _client.DeleteAsync($"http://localhost:16962/api/phonebook/delete/{id}");

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return View("../Account/AccessDenied");

            return RedirectToAction(nameof(Index));
        }
    }
}
