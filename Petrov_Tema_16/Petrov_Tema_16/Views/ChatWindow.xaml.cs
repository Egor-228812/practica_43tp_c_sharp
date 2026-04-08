using System.Windows;
using Petrov_Tema_16.ViewModels;

namespace Petrov_Tema_16.Views
{
    public partial class ChatWindow : Window
    {
        public ChatWindow(bool isServer)
        {
            InitializeComponent();
            DataContext = new ChatViewModel(isServer);
        }
    }
}