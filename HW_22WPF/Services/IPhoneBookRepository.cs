using IdentityShared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HW_22WPF.Services
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
        Task<PhoneBook> Get(int id);

        /// <summary>
        /// Добавить одну запись в базу данных
        /// </summary>
        /// <param name="item">Модель</param>
        void Add(PhoneBook item, string token);

        /// <summary>
        /// Обновить информацию одной записи в базе данных
        /// </summary>
        /// <param name="item">Модель</param>
        void Update(PhoneBook item, string token);

        /// <summary>
        /// Удалить одну запись из базы данных
        /// </summary>
        /// <param name="id">Номер записи</param>
        void Remove(int id, string token);
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
            var response = await _client.GetAsync("http://localhost:16962/api/phonebook/getall").ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync();
            var phoneBooks = JsonConvert.DeserializeObject<IEnumerable<PhoneBook>>(responseBody);

            return phoneBooks;
        }

        public Task<PhoneBook> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(PhoneBook item, string token)
        {
            var jsonPhoneBook = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonPhoneBook, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

             var result = _client.PostAsync("http://localhost:16962/api/phonebook/create", content).Result;
        }

        public void Update(PhoneBook item, string token)
        {
            var jsonPhoneBook = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonPhoneBook, Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _client.PutAsync("http://localhost:16962/api/phonebook/edit", content).Result;
        }

        public void Remove(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result = _client.DeleteAsync($"http://localhost:16962/api/phonebook/delete/{id}").Result;
        }
    }
}
