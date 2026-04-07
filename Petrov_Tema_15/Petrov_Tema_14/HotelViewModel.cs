using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Petrov_Tema_14
{
    public class HotelViewModel : INotifyPropertyChanged
    {
        private readonly BookingService _bookingService;
        private RoomModel selectedRoom;
        private BookingModel currentBooking;
        private bool isProcessing;

        public ObservableCollection<RoomModel> Rooms { get; set; }
        public ObservableCollection<BookingModel> Bookings { get; set; }

        public RoomModel SelectedRoom
        {
            get => selectedRoom;
            set { selectedRoom = value; OnPropertyChanged(); }
        }

        public BookingModel CurrentBooking
        {
            get => currentBooking;
            set { currentBooking = value; OnPropertyChanged(); }
        }

        public bool IsProcessing
        {
            get => isProcessing;
            set { isProcessing = value; OnPropertyChanged(); }
        }

        public ICommand BookCommand { get; }
        public ICommand CancelBookingCommand { get; }

        public HotelViewModel()
        {
            _bookingService = new BookingService();
            Rooms = new ObservableCollection<RoomModel>
            {
                new RoomModel(101, "Стандарт", true, 2500),
                new RoomModel(102, "Стандарт+", true, 3000),
                new RoomModel(103, "Люкс", true, 5000),
                new RoomModel(104, "Стандарт", false, 2500)
            };
            Bookings = new ObservableCollection<BookingModel>();
            CurrentBooking = new BookingModel();

            BookCommand = new RelayCommand(async () => await BookAsync(), () => SelectedRoom != null && !IsProcessing);
            CancelBookingCommand = new RelayCommand(async () => await CancelBookingAsync(), () => SelectedRoom != null && !IsProcessing);
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
                bool confirmed = await _bookingService.ConfirmBookingAsync(SelectedRoom, CurrentBooking);
                if (confirmed)
                {
                    Bookings.Add(new BookingModel
                    {
                        Id = Bookings.Count + 1,
                        CustomerName = CurrentBooking.CustomerName,
                        Contacts = CurrentBooking.Contacts,
                        RoomNumber = SelectedRoom.Number,
                        CheckInDate = CurrentBooking.CheckInDate,
                        CheckOutDate = CurrentBooking.CheckOutDate
                    });
                    CurrentBooking = new BookingModel();
                    SelectedRoom = null;
                    MessageBox.Show("Бронь добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show("Номер уже забронирован.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { IsProcessing = false; }
        }

        private async Task CancelBookingAsync()
        {
            if (SelectedRoom == null) return;
            var booking = Bookings.FirstOrDefault(b => b.RoomNumber == SelectedRoom.Number);
            if (booking == null) { MessageBox.Show("Бронь не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
            if (MessageBox.Show($"Отменить бронь для {booking.CustomerName}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            IsProcessing = true;
            try
            {
                bool cancelled = await _bookingService.CancelBookingAsync(booking, SelectedRoom);
                if (cancelled) Bookings.Remove(booking);
                MessageBox.Show("Бронь отменена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally { IsProcessing = false; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}