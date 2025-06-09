using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodDonationSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        // UC-12: Manage Member Profiles
        [HttpGet("members")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        {
            var members = await _staffService.GetAllMembersAsync();
            return Ok(members);
        }

        // UC-12: Manage Member Profiles
        [HttpGet("members/{id}")]
        public async Task<ActionResult<MemberDto>> GetMember(int id)
        {
            var member = await _staffService.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        // UC-12: Manage Member Profiles
        [HttpPut("members/{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberUpdateDto memberUpdateDto)
        {
            if (id != memberUpdateDto.Id)
            {
                return BadRequest();
            }

            var result = await _staffService.UpdateMemberAsync(memberUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-13: Manage Blood Requests
        [HttpGet("blood-requests")]
        public async Task<ActionResult<IEnumerable<BloodRequestDto>>> GetBloodRequests()
        {
            var requests = await _staffService.GetAllBloodRequestsAsync();
            return Ok(requests);
        }

        // UC-13: Manage Blood Requests
        [HttpGet("blood-requests/{id}")]
        public async Task<ActionResult<BloodRequestDto>> GetBloodRequest(int id)
        {
            var request = await _staffService.GetBloodRequestByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        // UC-26: Update Member Status (Donation Status)
        [HttpPut("blood-requests/{id}/status")]
        public async Task<IActionResult> UpdateBloodRequestStatus(int id, [FromBody] BloodRequestStatusUpdateDto statusUpdateDto)
        {
            if (id != statusUpdateDto.RequestId)
            {
                return BadRequest();
            }

            var result = await _staffService.UpdateBloodRequestStatusAsync(statusUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-14: Update Blogs
        [HttpPost("blogs")]
        public async Task<ActionResult<BlogDto>> CreateBlog([FromBody] BlogCreateDto blogCreateDto)
        {
            var createdBlog = await _staffService.CreateBlogAsync(blogCreateDto);
            return CreatedAtAction(nameof(GetBlog), new { id = createdBlog.Id }, createdBlog);
        }

        // UC-14: Update Blogs
        [HttpGet("blogs/{id}")]
        public async Task<ActionResult<BlogDto>> GetBlog(int id)
        {
            var blog = await _staffService.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        // UC-14: Update Blogs
        [HttpPut("blogs/{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogUpdateDto blogUpdateDto)
        {
            if (id != blogUpdateDto.Id)
            {
                return BadRequest();
            }

            var result = await _staffService.UpdateBlogAsync(blogUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-14: Update Blogs
        [HttpDelete("blogs/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var result = await _staffService.DeleteBlogAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-15: Search Donors/Patients
        [HttpGet("search/donors")]
        public async Task<ActionResult<IEnumerable<DonorDto>>> SearchDonors([FromQuery] DonorSearchCriteria criteria)
        {
            var donors = await _staffService.SearchDonorsAsync(criteria);
            return Ok(donors);
        }

        // UC-18: Send Notification
        [HttpPost("notifications")]
        public async Task<ActionResult<NotificationDto>> SendNotification([FromBody] NotificationCreateDto notificationDto)
        {
            var createdNotification = await _staffService.SendNotificationAsync(notificationDto);
            return CreatedAtAction(nameof(GetNotification), new { id = createdNotification.Id }, createdNotification);
        }

        // UC-18: Send Notification
        [HttpGet("notifications/{id}")]
        public async Task<ActionResult<NotificationDto>> GetNotification(int id)
        {
            var notification = await _staffService.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        // UC-19: View Events & Status
        [HttpGet("events")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
        {
            var events = await _staffService.GetAllEventsAsync();
            return Ok(events);
        }

        // UC-34: Create Event
        [HttpPost("events")]
        public async Task<ActionResult<EventDto>> CreateEvent([FromBody] EventCreateDto eventCreateDto)
        {
            var createdEvent = await _staffService.CreateEventAsync(eventCreateDto);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
        }

        // UC-35: Update Event
        [HttpPut("events/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventUpdateDto eventUpdateDto)
        {
            if (id != eventUpdateDto.Id)
            {
                return BadRequest();
            }

            var result = await _staffService.UpdateEventAsync(eventUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-36: Delete Event
        [HttpDelete("events/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _staffService.DeleteEventAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-16: View Feedback
        [HttpGet("feedbacks")]
        public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetFeedbacks()
        {
            var feedbacks = await _staffService.GetAllFeedbacksAsync();
            return Ok(feedbacks);
        }

        // UC-17: View Dashboard
        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            var dashboard = await _staffService.GetDashboardDataAsync();
            return Ok(dashboard);
        }
    }
}
