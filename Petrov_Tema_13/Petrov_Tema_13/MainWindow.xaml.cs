using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Petrov_Tema_13;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Petrov_Tema_12
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }

        public ICommand AddBookingCommand { get; }
        public ICommand EditBookingCommand { get; }
        public ICommand CancelBookingCommand { get; }

        public MainWindow()
        {
            InitializeComponent();

            Rooms = new ObservableCollection<Room>
            {
                new Room(132, "Стандарт", true, 55.00m),
                new Room(145, "Президентский", false, 479.00m),
                new Room(146, "Люкс", true, 210.00m),
                new Room(112, "Стандарт", false, 55.00m)
            };
            Bookings = new ObservableCollection<Booking>();

            roomsGrid.ItemsSource = Rooms;
            bookingsGrid.ItemsSource = Bookings;

            AddBookingCommand = new RelayCommand(AddBooking, CanAddEditBooking);
            EditBookingCommand = new RelayCommand(EditBooking, CanEditDeleteBooking);
            CancelBookingCommand = new RelayCommand(CancelBooking, CanEditDeleteBooking);

            DataContext = this;
        }

        private bool CanAddEditBooking() => true;
        private bool CanEditDeleteBooking() => bookingsGrid.SelectedItem != null;

        private void AddBooking()
        {
            try
            {
                string name = nameBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Введите ФИО клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DateTime? checkIn = checkInPicker.SelectedDate;
                DateTime? checkOut = checkOutPicker.SelectedDate;
                if (!checkIn.HasValue || !checkOut.HasValue)
                {
                    MessageBox.Show("Выберите даты заезда и выезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (checkIn.Value.Date >= checkOut.Value.Date)
                {
                    MessageBox.Show("Дата заезда должна быть раньше даты выезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (roomsGrid.SelectedItem == null)
                {
                    MessageBox.Show("Выберите номер в списке.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Room selectedRoom = (Room)roomsGrid.SelectedItem;
                if (!selectedRoom.IsAvailable)
                {
                    MessageBox.Show("Выбранный номер уже забронирован на другие даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool isFree = !Bookings.Any(b => b.RoomNumber == selectedRoom.NumberOfroom &&
                                                 checkIn.Value < b.CheckOutDate &&
                                                 checkOut.Value > b.CheckInDate);
                if (!isFree)
                {
                    MessageBox.Show("Номер уже забронирован на выбранные даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Booking newBooking = new Booking
                {
                    Id = Bookings.Count + 1,
                    CustomerName = name,
                    RoomNumber = selectedRoom.NumberOfroom,
                    CheckInDate = checkIn.Value.Date,
                    CheckOutDate = checkOut.Value.Date
                };
                Bookings.Add(newBooking);
                UpdateRoomAvailability(selectedRoom.NumberOfroom, false);

                ClearForm();
                MessageBox.Show("Бронирование добавлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditBooking()
        {
            try
            {
                Booking selected = bookingsGrid.SelectedItem as Booking;
                if (selected == null) return;

                string name = nameBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Введите ФИО клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DateTime? checkIn = checkInPicker.SelectedDate;
                DateTime? checkOut = checkOutPicker.SelectedDate;
                if (!checkIn.HasValue || !checkOut.HasValue)
                {
                    MessageBox.Show("Выберите даты заезда и выезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (checkIn.Value.Date >= checkOut.Value.Date)
                {
                    MessageBox.Show("Дата заезда должна быть раньше даты выезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool isFree = !Bookings.Any(b => b.RoomNumber == selected.RoomNumber &&
                                                 b.Id != selected.Id &&
                                                 checkIn.Value < b.CheckOutDate &&
                                                 checkOut.Value > b.CheckInDate);
                if (!isFree)
                {
                    MessageBox.Show("Номер уже забронирован на выбранные даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selected.CustomerName = name;
                selected.CheckInDate = checkIn.Value.Date;
                selected.CheckOutDate = checkOut.Value.Date;
                bookingsGrid.Items.Refresh();

                UpdateAllRoomsAvailability();
                ClearForm();
                MessageBox.Show("Бронирование изменено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBooking()
        {
            Booking selected = bookingsGrid.SelectedItem as Booking;
            if (selected == null) return;

            if (MessageBox.Show($"Отменить бронь для {selected.CustomerName}?",
                                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Bookings.Remove(selected);
                UpdateAllRoomsAvailability();
                ClearForm();
                MessageBox.Show("Бронь отменена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BookingsGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (bookingsGrid.SelectedItem is Booking selected)
            {
                nameBox.Text = selected.CustomerName;
                checkInPicker.SelectedDate = selected.CheckInDate;
                checkOutPicker.SelectedDate = selected.CheckOutDate;
            }
        }

        private void ClearForm()
        {
            nameBox.Text = "";
            checkInPicker.SelectedDate = null;
            checkOutPicker.SelectedDate = null;
            roomsGrid.SelectedItem = null;
        }

        private void UpdateRoomAvailability(int roomNumber, bool isAvailable)
        {
            var room = Rooms.FirstOrDefault(r => r.NumberOfroom == roomNumber);
            if (room != null) room.IsAvailable = isAvailable;
        }

        private void UpdateAllRoomsAvailability()
        {
            foreach (var room in Rooms)
                room.IsAvailable = true;

            var booked = Bookings.Select(b => b.RoomNumber).Distinct();
            foreach (var roomNum in booked)
                UpdateRoomAvailability(roomNum, false);
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();
        private void RefreshRooms_Click(object sender, RoutedEventArgs e) => UpdateAllRoomsAvailability();
        private void About_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Система бронирования отеля\nВерсия 1.0", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    
}