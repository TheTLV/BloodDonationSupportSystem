using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.DTOs.EventDTOs;
using BloodDonationSupportSystem.DTOs.NotificationDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services.Implementations;
using BloodDonationSupportSystem.Services.Interfaces;

namespace BloodDonationSupportSystem.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepo;
        private readonly INotificationService _notificationService;

        public EventService(IEventRepository eventRepo, INotificationService notificationService)
        {
            _eventRepo = eventRepo;
            _notificationService = notificationService;
        }

        public int CreateEvent(EventCreateDTO dto, int createdByUserId)
        {
            var newEvent = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                EventDate = dto.EventDate,
                CreatedBy = createdByUserId
            };

            var id = _eventRepo.CreateEvent(newEvent);

            _notificationService.CreateNotification(new NotificationCreateDTO
            {
                Message = $"Sự kiện mới: {dto.Title} sẽ diễn ra vào ngày {dto.EventDate:dd/MM/yyyy}",
                EventId = id
            });

            return id;
        }
        public bool UpdateEvent(int id, EventUpdateDTO dto)
        {
            var exist = _eventRepo.GetById(id);
            if (exist == null) return false;

            exist.Title = dto.Title;
            exist.Description = dto.Description;
            exist.EventDate = dto.EventDate;

            var success = _eventRepo.UpdateEvent(exist);
            if (success)
            {
                _notificationService.CreateNotification(new NotificationCreateDTO
                {
                    Message = $"Sự kiện {dto.Title} đã được cập nhật (ngày {dto.EventDate:dd/MM/yyyy})",
                    EventId = id
                });
            }

            return success;
        }

        public bool DeleteEvent(int id)
        {
            var ev = _eventRepo.GetById(id);
            if (ev == null) return false;

            var success = _eventRepo.DeleteEvent(id);
            if (success)
            {
                _notificationService.CreateNotification(new NotificationCreateDTO
                {
                    Message = $"Sự kiện {ev.Title} đã bị xóa"
                });
            }

            return success;
        }

        public EventDetailDTO? GetEventById(int id)
        {
            var e = _eventRepo.GetById(id);
            if (e == null) return null;

            return new EventDetailDTO
            {
                EventId = e.EventId,
                Title = e.Title,
                Description = e.Description,
                EventDate = e.EventDate
            };
        }

        public List<EventViewDTO> GetUpcomingEvents()
        {
            return _eventRepo.GetUpcomingEvents()
                .Select(e => new EventViewDTO
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    EventDate = e.EventDate
                }).ToList();
        }

        public List<EventViewDTO> GetAllEvents()
        {
            return _eventRepo.GetAllEvents()
                .Select(e => new EventViewDTO
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    EventDate = e.EventDate
                }).ToList();
        }
    }
}


