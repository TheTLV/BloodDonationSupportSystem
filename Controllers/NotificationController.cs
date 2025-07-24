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
        private int userId => GetUserIdFromToken();
        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                throw new Exception("Không tìm thấy UserId trong token");

            return int.Parse(userIdClaim.Value);
        }

        [HttpPost("sendToUser")]
        [Authorize(Roles = "2,3")] 
        public IActionResult SendToUser([FromBody] AdminNotificationCreateDTO dto)
        {
            try
            {
                _notificationService.AdimnCreateNotification(dto);
                return Ok(new { message = "Notification sent!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
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

        [HttpGet("unread-count")]
        [Authorize(Roles ="1,2,3")]
        public IActionResult GetUnreadCount()
        {
            var result = _notificationService.GetUnreadCount(userId);
            return Ok(result);
        }

        [HttpPut("mark-as-read/{id}")]
        [Authorize(Roles = "1,2,3")]
        public IActionResult MarkAsRead(int id)
        {
            _notificationService.MarkAsRead(id);
            return NoContent();
        }

        [HttpPut("mark-all-as-read")]
        [Authorize(Roles = "1,2,3")]
        public IActionResult MarkAllAsRead()
        {
            _notificationService.MarkAllAsRead(userId);
            return Ok(new { message = "Tất cả đã đọc" }); 
        }
    }
}
