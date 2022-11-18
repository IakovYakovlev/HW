using IdentityShared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace HW_22WPF.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsers();

        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

        Task<UserManagerResponse> DeleteUserAsync(string id);
    }
    public class UserService : IUserService
    {
        HttpClient _client;

        public UserService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            var response = await _client.GetAsync("http://localhost:16962/api/auth/getall").ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<UsersManager>(responseBody);

            var result = users.Users.ToList();

            return result;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("http://localhost:16962/api/auth/login", content).ConfigureAwait(false);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

                return result;
            }
            return new UserManagerResponse
            {
                Message = response.StatusCode.ToString(),
                IsSuccess = false,
            };
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("http://localhost:16962/api/auth/register", content);

            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

            return result;
        }

        public async Task<UserManagerResponse> DeleteUserAsync(string id)
        {
            var response = await _client.DeleteAsync($"http://localhost:16962/api/auth/delete/{id}");

            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

            return result;
        }
    }
}
