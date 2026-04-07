using System.Threading.Tasks;
using System.Windows;

namespace Petrov_Tema_14
{
    public class BookingService
    {
        public async Task<bool> ConfirmBookingAsync(RoomModel room, BookingModel booking)
        {
            await Task.Delay(3000);
            if (room.IsAvailable)
            {
                room.IsAvailable = false;
                return true;
            }
            return false;
        }

        public async Task<bool> CancelBookingAsync(BookingModel booking, RoomModel room)
        {
            await Task.Delay(2000);
            if (booking != null)
            {
                room.IsAvailable = true;
                return true;
            }
            return false;
        }
    }
}