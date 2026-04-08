using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Petrov_Tema_16.Models;

namespace Petrov_Tema_16.Services
{
    public class JsonDataService : IDataService
    {
        private readonly string _roomsFile = "hotel_data_rooms.json";
        private readonly string _bookingsFile = "hotel_data_bookings.json";
        private readonly string _usersFile = "users.json";

        public async Task<List<RoomModel>> LoadRoomsAsync()
        {
            if (!File.Exists(_roomsFile))
                return new List<RoomModel>();
            var json = await File.ReadAllTextAsync(_roomsFile);
            return JsonConvert.DeserializeObject<List<RoomModel>>(json) ?? new List<RoomModel>();
        }

        public async Task SaveRoomsAsync(List<RoomModel> rooms)
        {
            var json = JsonConvert.SerializeObject(rooms, Formatting.Indented);
            await File.WriteAllTextAsync(_roomsFile, json);
        }

        public async Task<List<BookingModel>> LoadBookingsAsync()
        {
            if (!File.Exists(_bookingsFile))
                return new List<BookingModel>();
            var json = await File.ReadAllTextAsync(_bookingsFile);
            return JsonConvert.DeserializeObject<List<BookingModel>>(json) ?? new List<BookingModel>();
        }

        public async Task SaveBookingsAsync(List<BookingModel> bookings)
        {
            var json = JsonConvert.SerializeObject(bookings, Formatting.Indented);
            await File.WriteAllTextAsync(_bookingsFile, json);
        }

        public async Task<List<UserModel>> LoadUsersAsync()
        {
            if (!File.Exists(_usersFile))
                return new List<UserModel>();
            var json = await File.ReadAllTextAsync(_usersFile);
            return JsonConvert.DeserializeObject<List<UserModel>>(json) ?? new List<UserModel>();
        }

        public async Task SaveUsersAsync(List<UserModel> users)
        {
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            await File.WriteAllTextAsync(_usersFile, json);
        }
    }
}