using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Petrov_Tema_16.Models
{
    public class BookingModel : INotifyPropertyChanged
    {
        private int id;
        private int userId;
        private string customerName;
        private string contacts;
        private int roomNumber;
        private DateTime checkInDate;
        private DateTime checkOutDate;

        public int Id { get => id; set { id = value; OnPropertyChanged(); } }
        public int UserId { get => userId; set { userId = value; OnPropertyChanged(); } }
        public string CustomerName { get => customerName; set { customerName = value; OnPropertyChanged(); } }
        public string Contacts { get => contacts; set { contacts = value; OnPropertyChanged(); } }
        public int RoomNumber { get => roomNumber; set { roomNumber = value; OnPropertyChanged(); } }
        public DateTime CheckInDate { get => checkInDate; set { checkInDate = value; OnPropertyChanged(); } }
        public DateTime CheckOutDate { get => checkOutDate; set { checkOutDate = value; OnPropertyChanged(); } }

        public BookingModel() { }
        public BookingModel(int id, int userId, string customerName, string contacts, int roomNumber, DateTime checkIn, DateTime checkOut)
        {
            Id = id; UserId = userId; CustomerName = customerName; Contacts = contacts;
            RoomNumber = roomNumber; CheckInDate = checkIn; CheckOutDate = checkOut;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}