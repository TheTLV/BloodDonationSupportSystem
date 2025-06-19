using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.Include(e => e.User).ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.Include(e => e.User)
                .FirstOrDefaultAsync(e => e.EventId == id);
        }

        public async Task AddEventAsync(Event eventItem)
        {
            await _context.Events.AddAsync(eventItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event eventItem)
        {
            _context.Events.Update(eventItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventItem = await GetEventByIdAsync(id);
            if (eventItem != null)
            {
                _context.Events.Remove(eventItem);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task AddEventAsync(Event eventItem);
        Task UpdateEventAsync(Event eventItem);
        Task DeleteEventAsync(int id);
    }
}