using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Petrov_Tema_18.Models;

namespace Petrov_Tema_18.Services
{
    public class DataStorage
    {
        private readonly string _filePath = "hotel_data.json";

        private class StorageContainer
        {
            public List<Room> Rooms { get; set; }
            public List<Booking> Bookings { get; set; }
        }

        public Task SaveDataAsync(List<Room> rooms, List<Booking> bookings)
        {
            return Task.Run(() =>
            {
                var container = new StorageContainer { Rooms = rooms, Bookings = bookings };
                var json = JsonConvert.SerializeObject(container, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            });
        }

        public async Task<(List<Room> rooms, List<Booking> bookings)> LoadDataAsync()
        {
            if (!File.Exists(_filePath))
            {
                var rooms = new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Type = "Стандарт", Price = 80, IsBooked = true },
                    new Room { Id = 2, RoomNumber = "102", Type = "Стандарт", Price = 80, IsBooked = false },
                    new Room { Id = 3, RoomNumber = "103", Type = "Улучшенный", Price = 120, IsBooked = false },
                    new Room { Id = 4, RoomNumber = "104", Type = "Люкс", Price = 180, IsBooked = false },
                    new Room { Id = 5, RoomNumber = "105", Type = "Стандарт", Price = 80, IsBooked = true },
                    new Room { Id = 6, RoomNumber = "106", Type = "Улучшенный", Price = 120, IsBooked = false }
                };
                var bookings = new List<Booking>();
                await SaveDataAsync(rooms, bookings);
                return (rooms, bookings);
            }

            var json = await Task.Run(() => File.ReadAllText(_filePath));
            var container = JsonConvert.DeserializeObject<StorageContainer>(json);

            foreach (var b in container.Bookings)
                b.Room = container.Rooms.Find(r => r.Id == b.RoomId);

            return (container.Rooms, container.Bookings);
        }
    }
}