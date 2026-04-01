using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Petrov_Tema_13
{
    public class Room : INotifyPropertyChanged
    {
        public int NumberOfroom { get; set; }
        public string Type { get; set; }
        private bool isAvailable;
        public bool IsAvailable
        {
            get => isAvailable;
            set { isAvailable = value; OnPropertyChanged(); }
        }
        public decimal PricePerNight { get; set; }

        public Room(int numberOfroom, string type, bool isAvailable, decimal pricePerNight)
        {
            NumberOfroom = numberOfroom;
            Type = type;
            IsAvailable = isAvailable;
            PricePerNight = pricePerNight;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
