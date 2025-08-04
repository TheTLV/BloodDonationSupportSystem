using System;
using System.Threading.Tasks;
using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.BloodDTOs;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "4")]
    public class DoctorController : ControllerBase
    {
        private readonly IBloodService _bloodService;

        public DoctorController(IBloodService bloodService)
        {
            _bloodService = bloodService;
        }
        [HttpGet("pending-donations")]
        public async Task<IActionResult> GetPendingDonations()
        {
            try
            {
                var donations = await _bloodService.GetPendingDonationsForDoctor();
                return Ok(donations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        //[HttpPost("medical-screening/{donationId}")]
        //public async Task<IActionResult> ProcessMedicalScreening(int donationId, [FromBody] DonationEligibilityDTO dto)
        //{
        //    try
        //    {
        //        if (dto.BloodPressure == null || dto.HemoglobinLevel == null)
        //            return BadRequest(new { message = "Huyết áp và hemoglobin là bắt buộc." });

        //        var success = await _bloodService.ProcessMedicalScreeningAsync(donationId, dto);
        //        if (!success)
        //            return BadRequest(new { message = "Không thể xử lý khám sàng lọc y tế. Vui lòng kiểm tra donationId hoặc trạng thái đơn." });

        //        return Ok(new { message = "Khám sàng lọc y tế thành công" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
        //    }
        //}





        [HttpPost("pre-screening/{donationId}")]
        public async Task<IActionResult> ProcessPreScreening(int donationId, [FromBody] PreDonationScreeningDTO dto)
        {
            try
            {
                var (success, error) = await _bloodService.ProcessMedicalScreeningAsync(donationId, dto);
                if (!success)
                    return BadRequest(new { message = error ?? "Không thể xử lý sàng lọc y tế." });

                return Ok(new { message = "Sàng lọc y tế trước khi lấy máu thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        [HttpPost("post-analysis/{donationId}")]
        public async Task<IActionResult> ProcessPostAnalysis(int donationId, [FromBody] PostDonationAnalysisDTO dto)
        {
            try
            {
                var (success, error) = await _bloodService.ProcessBloodAnalysisAsync(donationId, dto);
                if (!success)
                    return BadRequest(new { message = error ?? "Không thể xử lý phân tích máu." });

                return Ok(new { message = "Phân tích máu sau khi lấy thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        [HttpPost("doctor-note/{donationId}")]
        public async Task<IActionResult> PostDoctorNote(int donationId, [FromBody] DoctorNoteDTO dto)
        {
            var result = await _bloodService.DoctorNoteAsync(donationId, dto);
            return Ok(result);
        }

        
        //[HttpGet("doctor-note/{donationId}")]
        //public async Task<IActionResult> GetDoctorNotes(int donationId)
        //{
        //    var notes = await _bloodService.GetDoctorNotesAsync(donationId);
        //    return Ok(notes);
        //}

        [HttpPost("blood-quality-check/{donationId}")]
        public async Task<IActionResult> ProcessBloodQualityCheck(int donationId, [FromBody] BloodQualityCheckDTO dto)
        {
            try
            {
                var success = await _bloodService.ProcessBloodQualityCheckAsync(donationId, dto.QualityPassed);
                if (!success)
                    return BadRequest(new { message = "Không thể xử lý kiểm tra chất lượng máu. Vui lòng kiểm tra donationId hoặc trạng thái đơn." });

                return Ok(new { message = "Kiểm tra chất lượng máu thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }
        
        [HttpPut("update-blood-group/{userId}")]
        public async Task<IActionResult> UpdateBloodGroup(int userId, [FromBody] UpdateBloodTypeDTO dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.BloodType))
                    return BadRequest(new { message = "Nhóm máu là bắt buộc." });

                var success = await _bloodService.UpdateUserBloodGroupAsync(userId, dto);
                if (!success)
                    return BadRequest(new { message = "Không thể cập nhật nhóm máu. Vui lòng kiểm tra userId." });

                return Ok(new { message = "Cập nhật nhóm máu thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }
        
        
        [HttpPost("donations/{donationId}/stock")]
        public async Task<IActionResult> AddDonationBloodToBank(int donationId)
        {
            var donation = await _bloodService.GetDonate(donationId);
            if (donation == null)
                return NotFound(new { message = "Donation không tồn tại" });

            if (string.IsNullOrWhiteSpace(donation.BloodGroup))
                return BadRequest(new { message = "Không có nhóm máu để đưa vào kho." });

            var quantity = donation.Quantity ?? 0;
            if (quantity <= 0)
                return BadRequest(new { message = "Số lượng hiến không hợp lệ." });

            await _bloodService.AddOrUpdateStockAsync(donation.BloodGroup, quantity);

            return Ok(new { message = "Đã cập nhật kho máu từ donation" });
        }



    }
}