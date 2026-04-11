using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Petrov_Tema_18.Helpers;
using Petrov_Tema_18.Models;
using Petrov_Tema_18.Repositories;
using Petrov_Tema_18.Services;

namespace Petrov_Tema_18.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly RoomRepository _roomRepo;
        private readonly BookingRepository _bookingRepo;

        public ObservableCollection<Room> Rooms { get; } = new ObservableCollection<Room>();
        public ObservableCollection<Booking> Bookings { get; } = new ObservableCollection<Booking>();

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set { _selectedRoom = value; OnPropertyChanged(); }
        }

        private Booking _selectedBooking;
        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set { _selectedBooking = value; OnPropertyChanged(); }
        }

        private string _customerName = "";
        public string CustomerName
        {
            get => _customerName;
            set { _customerName = value; OnPropertyChanged(); }
        }

        private DateTime _checkInDate = DateTime.Today;
        public DateTime CheckInDate
        {
            get => _checkInDate;
            set { _checkInDate = value; OnPropertyChanged(); }
        }

        private DateTime _checkOutDate = DateTime.Today.AddDays(1);
        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set { _checkOutDate = value; OnPropertyChanged(); }
        }

        public ICommand LoadRoomsCommand { get; }
        public ICommand LoadBookingsCommand { get; }
        public ICommand BookRoomCommand { get; }
        public ICommand CancelBookingCommand { get; }

        public MainViewModel()
        {
            var storage = new DataStorage();
            _roomRepo = new RoomRepository(storage);
            _bookingRepo = new BookingRepository(storage);

            LoadRoomsCommand = new AsyncRelayCommand(async () => await LoadRoomsAsync());
            LoadBookingsCommand = new AsyncRelayCommand(async () => await LoadBookingsAsync());
            BookRoomCommand = new AsyncRelayCommand(async () => await BookRoomAsync(), () => SelectedRoom != null && !SelectedRoom.IsBooked && !string.IsNullOrWhiteSpace(CustomerName));
            CancelBookingCommand = new AsyncRelayCommand(async () => await CancelBookingAsync(), () => SelectedBooking != null);

            // Синхронная загрузка данных при старте
            Task.Run(async () => await InitializeAsync()).Wait();
        }

        private async Task InitializeAsync()
        {
            await ((RoomRepository)_roomRepo).LoadAsync();
            await ((BookingRepository)_bookingRepo).LoadAsync();
            await LoadRoomsAsync();
            await LoadBookingsAsync();
        }

        private async Task LoadRoomsAsync()
        {
            var rooms = await _roomRepo.GetAllAsync();
            Rooms.Clear();
            foreach (var r in rooms) Rooms.Add(r);
        }

        private async Task LoadBookingsAsync()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            Bookings.Clear();
            foreach (var b in bookings) Bookings.Add(b);
        }

        private async Task BookRoomAsync()
        {
            if (SelectedRoom == null || SelectedRoom.IsBooked) return;

            var booking = new Booking
            {
                RoomId = SelectedRoom.Id,
                CustomerName = CustomerName,
                CheckInDate = CheckInDate,
                CheckOutDate = CheckOutDate,
                IsActive = true,
                Room = SelectedRoom
            };

            await _bookingRepo.AddAsync(booking);
            SelectedRoom.IsBooked = true;
            await _roomRepo.UpdateAsync(SelectedRoom);
            await _bookingRepo.SaveChangesAsync();
            await _roomRepo.SaveChangesAsync();

            await LoadRoomsAsync();
            await LoadBookingsAsync();

            CustomerName = "";
            CheckInDate = DateTime.Today;
            CheckOutDate = DateTime.Today.AddDays(1);
        }

        private async Task CancelBookingAsync()
        {
            if (SelectedBooking == null) return;

            var room = await _roomRepo.GetByIdAsync(SelectedBooking.RoomId);
            if (room != null)
            {
                room.IsBooked = false;
                await _roomRepo.UpdateAsync(room);
            }

            await _bookingRepo.DeleteAsync(SelectedBooking.Id);
            await _bookingRepo.SaveChangesAsync();
            await _roomRepo.SaveChangesAsync();

            await LoadRoomsAsync();
            await LoadBookingsAsync();
        }
    }
}