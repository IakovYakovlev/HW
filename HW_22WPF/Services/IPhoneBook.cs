using IdentityShared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HW_22WPF.Services
{
    public interface IPhoneBook
    {
        Task<IEnumerable<PhoneBook>> GetAll();
    }

    public class PhoneBookRepository : IPhoneBook
    {
        HttpClient _client;

        public PhoneBookRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PhoneBook>> GetAll()
        {
            var response = await _client.GetAsync("http://localhost:16962/api/phonebook/getall").ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync();
            var phoneBooks = JsonConvert.DeserializeObject<IEnumerable<PhoneBook>>(responseBody);

            return phoneBooks;
        }
    }
}
