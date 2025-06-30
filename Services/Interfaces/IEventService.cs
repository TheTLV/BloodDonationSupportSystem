using BloodDonationSupportSystem.DTOs.EventDTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IEventService
    {
        int CreateEvent(EventCreateDTO dto, int createdByUserId);
        bool UpdateEvent(int id, EventUpdateDTO dto);
        bool DeleteEvent(int id);
        List<EventViewDTO> GetAllEvents();
        List<EventViewDTO> GetUpcomingEvents();
        EventDetailDTO? GetEventById(int id);
    }
}
