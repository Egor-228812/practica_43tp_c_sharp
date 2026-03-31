using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Petrov_Tema_12
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Room> rooms;
        private ObservableCollection<Booking> bookings;

        public MainWindow()
        {
            InitializeComponent(); 
            InitializeData();
            bookButton.Click += BookButton_Click;
        }

        private void InitializeData()
        {
            rooms = new ObservableCollection<Room>
            {
                new Room(132, "Стандарт", true, 55.00m),
                new Room(145, "Президентский", false, 479.00m),
                new Room(146, "Люкс", true, 210.00m),
                new Room(112, "Стандарт", false, 55.00m)
            };
            roomsGrid.ItemsSource = rooms;

            bookings = new ObservableCollection<Booking>();
            bookingsGrid.ItemsSource = bookings;
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string customerName = nameBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(customerName))
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

                bool isAvailable = true;
                foreach (Booking b in bookings)
                {
                    if (b.RoomNumber == selectedRoom.NumberOfroom)
                    {
                        if (checkIn.Value < b.CheckOutDate && checkOut.Value > b.CheckInDate)
                        {
                            isAvailable = false;
                            break;
                        }
                    }
                }

                if (!isAvailable)
                {
                    MessageBox.Show($"Номер {selectedRoom.NumberOfroom} уже забронирован на выбранные даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Booking newBooking = new Booking
                {
                    Id = bookings.Count + 1,
                    CustomerName = customerName,
                    RoomNumber = selectedRoom.NumberOfroom,
                    CheckInDate = checkIn.Value.Date,
                    CheckOutDate = checkOut.Value.Date
                };
                bookings.Add(newBooking);

                selectedRoom.IsAvailable = false;
                roomsGrid.Items.Refresh();

                nameBox.Text = "";
                checkInPicker.SelectedDate = null;
                checkOutPicker.SelectedDate = null;
                roomsGrid.SelectedItem = null;

                MessageBox.Show("Бронирование выполнено успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при бронировании: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}