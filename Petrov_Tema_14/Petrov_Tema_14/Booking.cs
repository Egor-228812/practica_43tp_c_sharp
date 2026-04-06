using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Petrov_Tema_14
{
    public class Booking : INotifyPropertyChanged
    {
        private int id;
        private string customerName;
        private string contacts;
        private int roomNumber;
        private DateTime checkInDate;
        private DateTime checkOutDate;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

        public string CustomerName
        {
            get => customerName;
            set { customerName = value; OnPropertyChanged(); }
        }

        public string Contacts
        {
            get => contacts;
            set { contacts = value; OnPropertyChanged(); }
        }

        public int RoomNumber
        {
            get => roomNumber;
            set { roomNumber = value; OnPropertyChanged(); }
        }

        public DateTime CheckInDate
        {
            get => checkInDate;
            set { checkInDate = value; OnPropertyChanged(); }
        }

        public DateTime CheckOutDate
        {
            get => checkOutDate;
            set { checkOutDate = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
