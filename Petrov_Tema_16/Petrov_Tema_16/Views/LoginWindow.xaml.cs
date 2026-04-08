using System.Windows;
using Petrov_Tema_16.Services;
using Petrov_Tema_16.ViewModels;

namespace Petrov_Tema_16.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            var dataService = new JsonDataService();
            var authService = new AuthService(dataService);
            DataContext = new LoginViewModel(authService);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
                vm.Password = ((System.Windows.Controls.PasswordBox)sender).Password;
        }
    }
}