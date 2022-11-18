using IdentityShared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace HW_20.Services
{
    public interface IPhoneBookRepository
    {
        /// <summary>
        /// Все записи из базы данных
        /// </summary>
        Task<IEnumerable<PhoneBook>> GetAllData();

        /// <summary>
        /// Получить одну запись из базы данных
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ода запись</returns>
        Task<PhoneBook> Get(int id, string token);

        /// <summary>
        /// Добавить одну запись в базу данных
        /// </summary>
        /// <param name="item">Модель</param>
        Task<HttpResponseMessage> Add(PhoneBook item, string token);

        /// <summary>
        /// Обновить информацию одной записи в базе данных
        /// </summary>
        /// <param name="item">Модель</param>
        Task<HttpResponseMessage> Update(PhoneBook item, string token);

        /// <summary>
        /// Удалить одну запись из базы данных
        /// </summary>
        /// <param name="id">Номер записи</param>
        HttpResponseMessage Remove(int id, string token);
    }

    public class PhoneBookRepository : IPhoneBookRepository
    {
        HttpClient _client;

        public PhoneBookRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<PhoneBook>> GetAllData()
        {
            var response = await _client.GetAsync("http://localhost:16962/api/phonebook/getall");
            var responseBody = await response.Content.ReadAsStringAsync();
            var phoneBooks = JsonConvert.DeserializeObject<IEnumerable<PhoneBook>>(responseBody);
            return phoneBooks;
        }

        public async Task<HttpResponseMessage> Add(PhoneBook item, string token)
        {
            var jsonPhoneBook = JsonConvert.SerializeObject(item);

            StringContent content = new StringContent(jsonPhoneBook, Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = await _client.PostAsync("http://localhost:16962/api/phonebook/create", content);

            return result;
        }

        public async Task<PhoneBook> Get(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"http://localhost:16962/api/phonebook/get/{id}");
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var phoneBook = JsonConvert.DeserializeObject<PhoneBook>(responseBody);
                return phoneBook;
            }

            return null; 
        }

        public async Task<HttpResponseMessage> Update(PhoneBook item, string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
            var jsonPhoneBook = JsonConvert.SerializeObject(item);
            
            StringContent content = new StringContent(jsonPhoneBook, Encoding.UTF8, "application/json");
            
            var result = await _client.PutAsync("http://localhost:16962/api/phonebook/edit", content);
            
            return result;
        }

        public HttpResponseMessage Remove(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _client.DeleteAsync($"http://localhost:16962/api/phonebook/delete/{id}").Result;

            return result;
        }


    }
}
