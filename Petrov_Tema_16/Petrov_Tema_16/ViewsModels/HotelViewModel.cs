using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Petrov_Tema_16.Models;
using Petrov_Tema_16.Services;
using Petrov_Tema_16.Views;

namespace Petrov_Tema_16.ViewModels
{
    public class HotelViewModel : INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private readonly BookingService _bookingService;
        private readonly AuthService _authService;
        private readonly NotificationService _notificationService;
        private RoomModel _selectedRoom;
        private BookingModel _currentBooking;
        private BookingModel _selectedBooking;
        private bool _isProcessing;
        private UserModel _currentUser;
        private string _statusMessage;

        public ObservableCollection<RoomModel> Rooms { get; set; }
        public ObservableCollection<BookingModel> Bookings { get; set; }

        public RoomModel SelectedRoom { get => _selectedRoom; set { _selectedRoom = value; OnPropertyChanged(); } }
        public BookingModel CurrentBooking { get => _currentBooking; set { _currentBooking = value; OnPropertyChanged(); } }
        public BookingModel SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                _selectedBooking = value;
                OnPropertyChanged();
                if (value != null && IsManager)
                {
                    CurrentBooking = new BookingModel
                    {
                        Id = value.Id,
                        CustomerName = value.CustomerName,
                        Contacts = value.Contacts,
                        RoomNumber = value.RoomNumber,
                        CheckInDate = value.CheckInDate,
                        CheckOutDate = value.CheckOutDate
                    };
                    SelectedRoom = Rooms.FirstOrDefault(r => r.Number == value.RoomNumber);
                }
            }
        }
        public bool IsProcessing { get => _isProcessing; set { _isProcessing = value; OnPropertyChanged(); } }
        public string StatusMessage { get => _statusMessage; set { _statusMessage = value; OnPropertyChanged(); } }
        public bool IsManager => _currentUser?.Role == "Manager";

        public ICommand BookCommand { get; }
        public ICommand CancelBookingCommand { get; }
        public ICommand EditBookingCommand { get; }
        public ICommand OpenChatCommand { get; }

        public HotelViewModel(IDataService dataService, BookingService bookingService, AuthService authService)
        {
            _dataService = dataService;
            _bookingService = bookingService;
            _authService = authService;
            _notificationService = new NotificationService();
            _notificationService.NotificationReceived += OnNotificationReceived;
            _notificationService.StartListening();

            Rooms = new ObservableCollection<RoomModel>();
            Bookings = new ObservableCollection<BookingModel>();
            CurrentBooking = new BookingModel();

            BookCommand = new RelayCommand(async () => await BookAsync(), () => SelectedRoom != null && !IsProcessing);
            CancelBookingCommand = new RelayCommand(async () => await CancelBookingAsync(), () => SelectedBooking != null && !IsProcessing && IsManager);
            EditBookingCommand = new RelayCommand(async () => await EditBookingAsync(), () => SelectedBooking != null && !IsProcessing && IsManager);
            OpenChatCommand = new RelayCommand(() => OpenChat());

            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            IsProcessing = true;
            var rooms = await _dataService.LoadRoomsAsync();
            if (rooms.Count == 0)
            {
                rooms = new System.Collections.Generic.List<RoomModel>
                {
                    new RoomModel(1, 101, "Стандарт", true, 2500),
                    new RoomModel(2, 102, "Стандарт+", true, 3000),
                    new RoomModel(3, 103, "Люкс", true, 5000),
                    new RoomModel(4, 104, "Стандарт", false, 2500)
                };
                await _dataService.SaveRoomsAsync(rooms);
            }
            Rooms.Clear();
            foreach (var r in rooms) Rooms.Add(r);

            var bookings = await _dataService.LoadBookingsAsync();
            Bookings.Clear();
            foreach (var b in bookings) Bookings.Add(b);

            _currentUser = Application.Current.Properties["CurrentUser"] as UserModel;
            OnPropertyChanged(nameof(IsManager));
            IsProcessing = false;
        }

        private async Task BookAsync()
        {
            if (SelectedRoom == null || string.IsNullOrWhiteSpace(CurrentBooking.CustomerName) ||
                CurrentBooking.CheckInDate == default || CurrentBooking.CheckOutDate == default)
            {
                MessageBox.Show("Заполните все поля и выберите номер.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CurrentBooking.CheckInDate >= CurrentBooking.CheckOutDate)
            {
                MessageBox.Show("Дата заезда должна быть раньше даты выезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IsProcessing = true;
            try
            {
                if (_currentUser == null || _currentUser.Role == "Guest")
                {
                    _currentUser = await _authService.RegisterGuestAsync(CurrentBooking.CustomerName, CurrentBooking.Contacts);
                    Application.Current.Properties["CurrentUser"] = _currentUser;
                    OnPropertyChanged(nameof(IsManager));
                }
                bool success = await _bookingService.ConfirmBookingAsync(SelectedRoom, CurrentBooking, _currentUser);
                if (success)
                {
                    await LoadDataAsync();
                    CurrentBooking = new BookingModel();
                    SelectedRoom = null;
                    MessageBox.Show("Бронь добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    _notificationService.SendNotification($"Новая бронь: {CurrentBooking.CustomerName} - номер {SelectedRoom?.Number}");
                }
                else
                {
                    MessageBox.Show("Номер уже забронирован.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
            finally { IsProcessing = false; }
        }

        private async Task CancelBookingAsync()
        {
            if (SelectedBooking == null) return;
            if (MessageBox.Show($"Отменить бронь для {SelectedBooking.CustomerName}?", "Подтверждение", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            IsProcessing = true;
            try
            {
                var room = Rooms.FirstOrDefault(r => r.Number == SelectedBooking.RoomNumber);
                bool success = await _bookingService.CancelBookingAsync(SelectedBooking, room);
                if (success)
                {
                    await LoadDataAsync();
                    MessageBox.Show("Бронь отменена.");
                    _notificationService.SendNotification($"Отмена брони: номер {SelectedBooking.RoomNumber}");
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
            finally { IsProcessing = false; SelectedBooking = null; }
        }

        private async Task EditBookingAsync()
        {
            if (SelectedBooking == null) return;
            if (SelectedRoom == null || string.IsNullOrWhiteSpace(CurrentBooking.CustomerName) ||
                CurrentBooking.CheckInDate == default || CurrentBooking.CheckOutDate == default)
            {
                MessageBox.Show("Заполните все поля и выберите номер.");
                return;
            }
            if (CurrentBooking.CheckInDate >= CurrentBooking.CheckOutDate)
            {
                MessageBox.Show("Дата заезда должна быть раньше выезда.");
                return;
            }

            bool isFree = !Bookings.Any(b => b.RoomNumber == SelectedRoom.Number &&
                                             b.Id != SelectedBooking.Id &&
                                             CurrentBooking.CheckInDate < b.CheckOutDate &&
                                             CurrentBooking.CheckOutDate > b.CheckInDate);
            if (!isFree)
            {
                MessageBox.Show("Номер уже забронирован на эти даты.");
                return;
            }

            SelectedBooking.CustomerName = CurrentBooking.CustomerName;
            SelectedBooking.Contacts = CurrentBooking.Contacts;
            SelectedBooking.RoomNumber = SelectedRoom.Number;
            SelectedBooking.CheckInDate = CurrentBooking.CheckInDate;
            SelectedBooking.CheckOutDate = CurrentBooking.CheckOutDate;

            await LoadDataAsync();
            CurrentBooking = new BookingModel();
            SelectedRoom = null;
            SelectedBooking = null;
            MessageBox.Show("Бронь обновлена.");
        }

        private void OpenChat()
        {
            bool isManager = IsManager;
            var chatWindow = new ChatWindow(isManager);
            chatWindow.Show();
        }

        private void OnNotificationReceived(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return;
            App.Current.Dispatcher.Invoke(() =>
            {
                StatusMessage = msg;
                Task.Run(async () => { await Task.Delay(5000); App.Current.Dispatcher.Invoke(() => { if (StatusMessage == msg) StatusMessage = ""; }); });
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}