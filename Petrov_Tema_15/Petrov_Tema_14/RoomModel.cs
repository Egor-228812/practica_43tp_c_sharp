using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Petrov_Tema_14
{
    public class RoomModel : INotifyPropertyChanged
    {
        private int number;
        private string type;
        private bool isAvailable;
        private decimal pricePerNight;

        public int Number
        {
            get => number;
            set { number = value; OnPropertyChanged(); }
        }

        public string Type
        {
            get => type;
            set { type = value; OnPropertyChanged(); }
        }

        public bool IsAvailable
        {
            get => isAvailable;
            set { isAvailable = value; OnPropertyChanged(); }
        }

        public decimal PricePerNight
        {
            get => pricePerNight;
            set { pricePerNight = value; OnPropertyChanged(); }
        }

        public RoomModel(int number, string type, bool isAvailable, decimal pricePerNight)
        {
            Number = number;
            Type = type;
            IsAvailable = isAvailable;
            PricePerNight = pricePerNight;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}