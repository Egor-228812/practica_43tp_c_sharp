using System.ComponentModel;

namespace Petrov_Tema_17
{
    public class Room : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isBooked;
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool IsBooked
        {
            get => isBooked;
            set
            {
                if (isBooked != value)
                {
                    isBooked = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBooked)));
                }
            }
        }
    }
}