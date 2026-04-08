using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Petrov_Tema_16.Models
{
    public class UserModel : INotifyPropertyChanged
    {
        private int id;
        private string username;
        private string passwordHash;
        private string role; // "Manager" или "Guest"

        public int Id { get => id; set { id = value; OnPropertyChanged(); } }
        public string Username { get => username; set { username = value; OnPropertyChanged(); } }
        public string PasswordHash { get => passwordHash; set { passwordHash = value; OnPropertyChanged(); } }
        public string Role { get => role; set { role = value; OnPropertyChanged(); } }

        public UserModel() { }
        public UserModel(int id, string username, string passwordHash, string role)
        {
            Id = id; Username = username; PasswordHash = passwordHash; Role = role;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}