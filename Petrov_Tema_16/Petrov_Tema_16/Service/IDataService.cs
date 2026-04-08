using System.Collections.Generic;
using System.Threading.Tasks;
using Petrov_Tema_16.Models;

namespace Petrov_Tema_16.Services
{
    public interface IDataService
    {
        Task<List<RoomModel>> LoadRoomsAsync();
        Task SaveRoomsAsync(List<RoomModel> rooms);
        Task<List<BookingModel>> LoadBookingsAsync();
        Task SaveBookingsAsync(List<BookingModel> bookings);
        Task<List<UserModel>> LoadUsersAsync();
        Task SaveUsersAsync(List<UserModel> users);
    }
}