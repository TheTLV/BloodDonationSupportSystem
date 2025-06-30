using BloodDonationSupportSystem.DTOs.EventDTOs;
using BloodDonationSupportSystem.DTOs.NotificationDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "2,3")]
        public IActionResult Create([FromBody] EventCreateDTO dto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null) return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);
            var id = _eventService.CreateEvent(dto, userId);
            return Ok(new { message = "Tạo sự kiện thành công", eventId = id });
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "2,3")]
        public IActionResult Update(int id, [FromBody] EventUpdateDTO dto)
        {
            var success = _eventService.UpdateEvent(id, dto);
            if (!success) return NotFound(new { message = "Không tìm thấy sự kiện" });
            return Ok(new { message = "Cập nhật sự kiện thành công" });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "2,3")]
        public IActionResult Delete(int id)
        {
            var success = _eventService.DeleteEvent(id);
            if (!success) return NotFound(new { message = "Không tìm thấy sự kiện" });
            return Ok(new { message = "Xóa sự kiện thành công" });
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var events = _eventService.GetAllEvents();
            return Ok(events);
        }

        [HttpGet("upcoming")]
        [AllowAnonymous]
        public IActionResult GetUpcoming()
        {
            var upcoming = _eventService.GetUpcomingEvents();
            return Ok(upcoming);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            var ev = _eventService.GetEventById(id);
            if (ev == null) return NotFound(new { message = "Không tìm thấy sự kiện" });
            return Ok(ev);
        }
    }
}
