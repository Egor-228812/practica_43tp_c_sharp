using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petrov_Tema_18.Models;
using Petrov_Tema_18.Services;

namespace Petrov_Tema_18.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private List<Booking> _bookings;
        private readonly DataStorage _storage;

        public BookingRepository(DataStorage storage)
        {
            _storage = storage;
            _bookings = new List<Booking>();
        }

        public async Task LoadAsync()
        {
            var (_, bookings) = await _storage.LoadDataAsync();
            _bookings = bookings ?? new List<Booking>();
        }

        public Task<List<Booking>> GetAllAsync() => Task.FromResult(_bookings.ToList());

        public Task<Booking> GetByIdAsync(int id) => Task.FromResult(_bookings.FirstOrDefault(b => b.Id == id));

        public Task AddAsync(Booking entity)
        {
            entity.Id = _bookings.Any() ? _bookings.Max(b => b.Id) + 1 : 1;
            _bookings.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Booking entity)
        {
            var index = _bookings.FindIndex(b => b.Id == entity.Id);
            if (index >= 0) _bookings[index] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == id);
            if (booking != null) _bookings.Remove(booking);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            var (rooms, _) = await _storage.LoadDataAsync();
            await _storage.SaveDataAsync(rooms, _bookings);
        }
    }
}