using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petrov_Tema_18.Models;
using Petrov_Tema_18.Services;

namespace Petrov_Tema_18.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private List<Room> _rooms;
        private readonly DataStorage _storage;

        public RoomRepository(DataStorage storage)
        {
            _storage = storage;
            _rooms = new List<Room>();
        }

        public async Task LoadAsync()
        {
            var (rooms, _) = await _storage.LoadDataAsync();
            _rooms = rooms ?? new List<Room>();
        }

        private async Task SaveIfNeededAsync()
        {
            var (_, bookings) = await _storage.LoadDataAsync();
            await _storage.SaveDataAsync(_rooms, bookings);
        }

        public Task<List<Room>> GetAllAsync() => Task.FromResult(_rooms.ToList());

        public Task<Room> GetByIdAsync(int id) => Task.FromResult(_rooms.FirstOrDefault(r => r.Id == id));

        public Task AddAsync(Room entity)
        {
            entity.Id = _rooms.Any() ? _rooms.Max(r => r.Id) + 1 : 1;
            _rooms.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Room entity)
        {
            var index = _rooms.FindIndex(r => r.Id == entity.Id);
            if (index >= 0) _rooms[index] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room != null) _rooms.Remove(room);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await SaveIfNeededAsync();
        }
    }
}