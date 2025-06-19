using AutoMapper;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories;

namespace BloodDonationSupportSystem.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var eventItem = await _eventRepository.GetEventByIdAsync(id);
            return _mapper.Map<EventDto>(eventItem);
        }

        public async Task AddEventAsync(Event eventItem)
        {
            await _eventRepository.AddEventAsync(eventItem);
        }

        public async Task UpdateEventAsync(Event eventItem)
        {
            await _eventRepository.UpdateEventAsync(eventItem);
        }

        public async Task DeleteEventAsync(int id)
        {
            await _eventRepository.DeleteEventAsync(id);
        }

        public async Task<IEnumerable<EventDto>> GetUpcomingEventsAsync()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            var upcomingEvents = events.Where(e => e.EventDate > DateTime.Now && e.Status == "Upcoming");
            return _mapper.Map<IEnumerable<EventDto>>(upcomingEvents);
        }
    }
}