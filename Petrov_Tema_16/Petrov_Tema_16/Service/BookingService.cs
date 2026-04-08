using System;
using System.Linq;
using System.Threading.Tasks;
using Petrov_Tema_16.Models;

namespace Petrov_Tema_16.Services
{
    public class BookingService
    {
        private readonly IDataService _dataService;
        private readonly AuthService _authService;

        public BookingService(IDataService dataService, AuthService authService)
        {
            _dataService = dataService;
            _authService = authService;
        }

        public async Task<bool> ConfirmBookingAsync(RoomModel room, BookingModel booking, UserModel currentUser)
        {
            await Task.Delay(3000);
            if (room.IsAvailable)
            {
                room.IsAvailable = false;
                var bookings = await _dataService.LoadBookingsAsync();
                booking.Id = bookings.Count > 0 ? bookings.Max(b => b.Id) + 1 : 1;
                booking.UserId = currentUser.Id;
                bookings.Add(booking);
                await _dataService.SaveBookingsAsync(bookings);
                var rooms = await _dataService.LoadRoomsAsync();
                var r = rooms.Find(rr => rr.Id == room.Id);
                if (r != null) r.IsAvailable = false;
                await _dataService.SaveRoomsAsync(rooms);
                return true;
            }
            return false;
        }

        public async Task<bool> CancelBookingAsync(BookingModel booking, RoomModel room)
        {
            await Task.Delay(2000);
            var bookings = await _dataService.LoadBookingsAsync();
            var toRemove = bookings.Find(b => b.Id == booking.Id);
            if (toRemove != null)
            {
                bookings.Remove(toRemove);
                await _dataService.SaveBookingsAsync(bookings);
                var rooms = await _dataService.LoadRoomsAsync();
                var r = rooms.Find(rr => rr.Id == room.Id);
                if (r != null) r.IsAvailable = true;
                await _dataService.SaveRoomsAsync(rooms);
                return true;
            }
            return false;
        }
    }
}