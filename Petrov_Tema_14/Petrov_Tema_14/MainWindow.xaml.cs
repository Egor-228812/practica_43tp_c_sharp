using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Petrov_Tema_14
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get => selectedRoom;
            set { selectedRoom = value; OnPropertyChanged(); }
        }

        private Booking selectedBooking;
        public Booking SelectedBooking
        {
            get => selectedBooking;
            set
            {
                selectedBooking = value;
                OnPropertyChanged();
                if (selectedBooking != null)
                {
                    CurrentBooking = new Booking
                    {
                        Id = selectedBooking.Id,
                        CustomerName = selectedBooking.CustomerName,
                        Contacts = selectedBooking.Contacts,
                        RoomNumber = selectedBooking.RoomNumber,
                        CheckInDate = selectedBooking.CheckInDate,
                        CheckOutDate = selectedBooking.CheckOutDate
                    };
                }
                else
                {
                    CurrentBooking = new Booking();
                }
            }
        }

        private Booking currentBooking = new Booking();
        public Booking CurrentBooking
        {
            get => currentBooking;
            set { currentBooking = value; OnPropertyChanged(); }
        }

        public ICommand AddBookingCommand { get; }
        public ICommand EditBookingCommand { get; }
        public ICommand CancelBookingCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Rooms = new ObservableCollection<Room>
            {
                new Room(101, "Стандарт", true, 165),
                new Room(102, "Стандарт+", true, 210),
                new Room(103, "Люкс", true, 305),
                new Room(104, "Стандарт", false, 165)
            };
            Bookings = new ObservableCollection<Booking>();
            CurrentBooking = new Booking();

            AddBookingCommand = new RelayCommand(AddBooking, () => SelectedRoom != null);
            EditBookingCommand = new RelayCommand(EditBooking, () => SelectedBooking != null);
            CancelBookingCommand = new RelayCommand(CancelBooking, () => SelectedBooking != null);
        }

        private void AddBooking()
        {
            try
            {
                if (SelectedRoom == null)
                {
                    MessageBox.Show("Выберите номер.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(CurrentBooking.CustomerName))
                {
                    MessageBox.Show("Введите ФИО.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(CurrentBooking.Contacts))
                {
                    MessageBox.Show("Введите контакты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (CurrentBooking.CheckInDate == default || CurrentBooking.CheckOutDate == default)
                {
                    MessageBox.Show("Выберите даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (CurrentBooking.CheckInDate >= CurrentBooking.CheckOutDate)
                {
                    MessageBox.Show("Дата заезда должна быть раньше даты выезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool isFree = !Bookings.Any(b => b.RoomNumber == SelectedRoom.Number &&
                                                 CurrentBooking.CheckInDate < b.CheckOutDate &&
                                                 CurrentBooking.CheckOutDate > b.CheckInDate);
                if (!isFree)
                {
                    MessageBox.Show("Номер уже забронирован на эти даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Booking newBooking = new Booking
                {
                    Id = Bookings.Count + 1,
                    CustomerName = CurrentBooking.CustomerName,
                    Contacts = CurrentBooking.Contacts,
                    RoomNumber = SelectedRoom.Number,
                    CheckInDate = CurrentBooking.CheckInDate,
                    CheckOutDate = CurrentBooking.CheckOutDate
                };
                Bookings.Add(newBooking);
                SelectedRoom.IsAvailable = false;

                CurrentBooking = new Booking();
                SelectedRoom = null;
                MessageBox.Show("Бронь добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
                if (SelectedBooking == null) return;
                if (string.IsNullOrWhiteSpace(CurrentBooking.CustomerName) ||
                    string.IsNullOrWhiteSpace(CurrentBooking.Contacts))
                {
                    MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (CurrentBooking.CheckInDate >= CurrentBooking.CheckOutDate)
                {
                    MessageBox.Show("Некорректные даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SelectedBooking.CustomerName = CurrentBooking.CustomerName;
                SelectedBooking.Contacts = CurrentBooking.Contacts;
                SelectedBooking.CheckInDate = CurrentBooking.CheckInDate;
                SelectedBooking.CheckOutDate = CurrentBooking.CheckOutDate;

                bookingsGrid.Items.Refresh();
                CurrentBooking = new Booking();
                SelectedBooking = null;
                MessageBox.Show("Бронь изменена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBooking()
        {
            if (SelectedBooking == null) return;
            if (MessageBox.Show($"Отменить бронь для {SelectedBooking.CustomerName}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Bookings.Remove(SelectedBooking);
                var room = Rooms.FirstOrDefault(r => r.Number == SelectedBooking.RoomNumber);
                if (room != null && !Bookings.Any(b => b.RoomNumber == room.Number))
                    room.IsAvailable = true;
                CurrentBooking = new Booking();
                SelectedBooking = null;
                MessageBox.Show("Бронь отменена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshRooms_Click(object sender, RoutedEventArgs e)
        {
            foreach (var room in Rooms)
            {
                bool hasActiveBooking = Bookings.Any(b => b.RoomNumber == room.Number);
                room.IsAvailable = !hasActiveBooking;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void About_Click(object sender, RoutedEventArgs e) =>
            MessageBox.Show("Система бронирования отеля\nВерсия 1.0", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}