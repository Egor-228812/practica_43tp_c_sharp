using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Petrov_Tema_16.Models;
using Petrov_Tema_16.Services;
using Petrov_Tema_16.Views;

namespace Petrov_Tema_16.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        private string _username;
        private string _password;
        private bool _isLoading;

        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(async () => await LoginAsync(), () => !IsLoading);
            RegisterCommand = new RelayCommand(async () => await RegisterAsync(), () => !IsLoading);
        }

        private async Task LoginAsync()
        {
            IsLoading = true;
            var user = await _authService.LoginAsync(Username, Password);
            if (user != null)
            {
                Application.Current.Properties["CurrentUser"] = user;
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0]?.Close();
            }
            else
            {
                MessageBox.Show("Неверные имя или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            IsLoading = false;
        }

        private async Task RegisterAsync()
        {
            IsLoading = true;
            bool success = await _authService.RegisterManagerAsync(Username, Password);
            if (success)
                MessageBox.Show("Менеджер зарегистрирован. Теперь войдите.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Пользователь уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}