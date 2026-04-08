using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Petrov_Tema_16.Models
{
    public class RoomModel : INotifyPropertyChanged
    {
        private int id;
        private int number;
        private string type;
        private bool isAvailable;
        private decimal pricePerNight;

        public int Id { get => id; set { id = value; OnPropertyChanged(); } }
        public int Number { get => number; set { number = value; OnPropertyChanged(); } }
        public string Type { get => type; set { type = value; OnPropertyChanged(); } }
        public bool IsAvailable { get => isAvailable; set { isAvailable = value; OnPropertyChanged(); } }
        public decimal PricePerNight { get => pricePerNight; set { pricePerNight = value; OnPropertyChanged(); } }

        public RoomModel() { }
        public RoomModel(int id, int number, string type, bool isAvailable, decimal pricePerNight)
        {
            Id = id; Number = number; Type = type; IsAvailable = isAvailable; PricePerNight = pricePerNight;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}