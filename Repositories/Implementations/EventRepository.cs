using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;

namespace BloodDonationSupportSystem.Repositories.Implementations
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public int CreateEvent(Event ev)
        {
            _context.Events.Add(ev);
            _context.SaveChanges();
            return ev.EventId;
        }
        
        public bool UpdateEvent(Event ev)
        {
            _context.Events.Update(ev);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteEvent(int id)
        {
            var ev = _context.Events.Find(id);
            if (ev == null) return false;

            _context.Events.Remove(ev);
            return _context.SaveChanges() > 0;
        }

        public List<Event> GetAllEvents()
        {
            return _context.Events.OrderByDescending(e => e.EventDate).ToList();
        }
        public List<Event> GetUpcomingEvents()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return _context.Events
                .Where(e => e.EventDate >= today)
                .OrderBy(e => e.EventDate)
                .ToList();
        }

        public Event? GetById(int id)
        {
            return _context.Events.FirstOrDefault(e => e.EventId == id);
        }

    }
}
