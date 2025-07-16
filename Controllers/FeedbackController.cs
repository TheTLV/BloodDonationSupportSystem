namespace BloodDonationSupportSystem.Controllers
{
    using BloodDonationSupportSystem.DTOs.FeedbackDTOs;
    using BloodDonationSupportSystem.Models;
    using BloodDonationSupportSystem.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "1,2,3")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private int UId => GetUserIdFromToken();
        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                throw new Exception("Không tìm thấy UserId trong token");

            return int.Parse(userIdClaim.Value);
        }
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult PostFeedback([FromBody] FeedbackCreateDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Content))
                    return BadRequest("Nội dung không được để trống.");

                _feedbackService.SendFeedback(UId, dto); 
                return Ok(new { message = "Gửi feedback thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpGet]
        [Authorize(Roles = "2,3")]
        public IActionResult GetAll()
        {
            var list = _feedbackService.GetAll();
            return Ok(list);
        }


        [HttpGet("search")]
        [Authorize(Roles = "2,3")]
        public IActionResult Search([FromQuery] string keyword)
        {
            var result = _feedbackService.Search(keyword ?? "");
            return Ok(result);
        }
    }

}
