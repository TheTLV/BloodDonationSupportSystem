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

        [HttpPost("sendToUser")]
        [Authorize(Roles = "1,2")] // 1 = Admin, 2 = Staff
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
    }

}
