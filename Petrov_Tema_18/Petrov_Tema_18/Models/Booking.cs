using System;

namespace Petrov_Tema_18.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string CustomerName { get; set; } = "";
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsActive { get; set; } = true;

       
        public Room Room { get; set; }
    }
}