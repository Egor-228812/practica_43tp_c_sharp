using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Petrov_Tema_16.Models;

namespace Petrov_Tema_16.Services
{
    public class AuthService
    {
        private readonly IDataService _dataService;

        public AuthService(IDataService dataService)
        {
            _dataService = dataService;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return System.Convert.ToBase64String(hash);
        }

        public async Task<UserModel> LoginAsync(string username, string password)
        {
            var users = await _dataService.LoadUsersAsync();
            var hash = HashPassword(password);
            return users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);
        }

        public async Task<bool> RegisterManagerAsync(string username, string password)
        {
            var users = await _dataService.LoadUsersAsync();
            if (users.Any(u => u.Username == username))
                return false;
            var newUser = new UserModel
            {
                Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1,
                Username = username,
                PasswordHash = HashPassword(password),
                Role = "Manager"
            };
            users.Add(newUser);
            await _dataService.SaveUsersAsync(users);
            return true;
        }

        public async Task<UserModel> RegisterGuestAsync(string name, string contacts)
        {
            var users = await _dataService.LoadUsersAsync();
            var guest = new UserModel
            {
                Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1,
                Username = name,
                PasswordHash = "",
                Role = "Guest"
            };
            users.Add(guest);
            await _dataService.SaveUsersAsync(users);
            return guest;
        }
    }
}