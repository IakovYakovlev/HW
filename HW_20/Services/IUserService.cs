using IdentityShared;
using Newtonsoft.Json;
using System.Text;

namespace HW_20.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsers();

        Task<Users> GetUser(string id);

        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<string> LoginUserAsync(LoginViewModel model);

        Task<UserManagerResponse> EditAsync(Users item);

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

        public async Task<string> LoginUserAsync(LoginViewModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("http://localhost:16962/api/auth/login", content);

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async Task<UserManagerResponse> DeleteUserAsync(string id)
        {
            var response = await _client.DeleteAsync($"http://localhost:16962/api/auth/delete/{id}");

            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<UserManagerResponse>(responseBody);

            return result;
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

        public Task<UserManagerResponse> EditAsync(Users item)
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
