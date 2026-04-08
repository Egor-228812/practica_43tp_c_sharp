using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Petrov_Tema_16.Services;

namespace Petrov_Tema_16.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        private readonly ChatService _chatService;
        private readonly bool _isServerMode;
        private string _newMessage;
        private bool _isConnected;

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();
        public string NewMessage { get => _newMessage; set { _newMessage = value; OnPropertyChanged(); } }
        public bool IsConnected { get => _isConnected; set { _isConnected = value; OnPropertyChanged(); } }

        public ICommand SendCommand { get; }

        public ChatViewModel(bool isServer)
        {
            _isServerMode = isServer;
            _chatService = new ChatService();
            _chatService.MessageReceived += OnMessageReceived;
            _chatService.StatusChanged += OnStatusChanged;
            SendCommand = new RelayCommand(async () => await SendAsync(), () => IsConnected && !string.IsNullOrWhiteSpace(NewMessage));
            _ = Task.Run(() => ConnectAsync());
        }

        private async Task ConnectAsync()
        {
            bool success;
            if (_isServerMode)
                success = await _chatService.StartServerAsync();
            else
                success = await _chatService.ConnectClientAsync();
            IsConnected = success;
        }

        private async Task SendAsync()
        {
            if (!IsConnected) return;
            await _chatService.SendMessageAsync(NewMessage, _isServerMode);
            Messages.Add($"Вы: {NewMessage}");
            NewMessage = "";
        }

        private void OnMessageReceived(string msg)
        {
            App.Current.Dispatcher.Invoke(() => Messages.Add($"Собеседник: {msg}"));
        }

        private void OnStatusChanged(string status)
        {
            App.Current.Dispatcher.Invoke(() => Messages.Add($"[СТАТУС] {status}"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}