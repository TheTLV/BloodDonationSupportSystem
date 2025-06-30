using BloodDonationSupportSystem.DTOs.NotificationDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "2,3")]
        public IActionResult Create([FromBody] NotificationCreateDTO dto)
        {
            int? userId = null;
            if (User.HasClaim(c => c.Type == "UserId"))
                userId = int.Parse(User.FindFirst("UserId")!.Value);

            _notificationService.CreateNotification(dto, userId);
            return Ok(new { message = "Tạo thông báo thành công" });
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "1,2,3")]
        public IActionResult GetAll()
        {
            var list = _notificationService.GetAllNotifications();
            return Ok(list);
        }

        [HttpGet("getByUser")]
        [Authorize(Roles = "1")]
        public IActionResult GetByUser()
        {
            int userId = int.Parse(User.FindFirst("UserId")!.Value);
            var list = _notificationService.GetNotificationsByUser(userId);
            return Ok(list);
        }
    }

}
