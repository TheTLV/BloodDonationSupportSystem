using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface IEventRepository
    {
        int CreateEvent(Event ev);
        bool UpdateEvent(Event ev);
        bool DeleteEvent(int id);
        List<Event> GetAllEvents();
        List<Event> GetUpcomingEvents();
        Event? GetById(int id);
    }
}
