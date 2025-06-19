using AutoMapper;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly BloodRequestService _bloodRequestService;
        private readonly BlogService _blogService;
        private readonly EventService _eventService;
        private readonly IMapper _mapper;

        public StaffController(
            BloodRequestService bloodRequestService,
            BlogService blogService,
            EventService eventService,
            IMapper mapper)
        {
            _bloodRequestService = bloodRequestService;
            _blogService = blogService;
            _eventService = eventService;
            _mapper = mapper;
        }

        // Blood Requests
        [HttpGet("bloodrequests")]
        public async Task<ActionResult<IEnumerable<BloodRequestDto>>> GetAllBloodRequests()
        {
            var requests = await _bloodRequestService.GetAllBloodRequestsAsync();
            return Ok(requests);
        }

        [HttpGet("bloodrequests/emergency")]
        public async Task<ActionResult<IEnumerable<BloodRequestDto>>> GetEmergencyBloodRequests()
        {
            var requests = await _bloodRequestService.GetEmergencyRequestsAsync();
            return Ok(requests);
        }

        [HttpPost("bloodrequests")]
        public async Task<ActionResult<BloodRequestDto>> CreateBloodRequest(BloodRequestDto requestDto)
        {
            var request = _mapper.Map<BloodRequest>(requestDto);
            await _bloodRequestService.AddBloodRequestAsync(request);
            return CreatedAtAction(nameof(GetBloodRequestById), new { id = request.RequestId }, requestDto);
        }

        [HttpGet("bloodrequests/{id}")]
        public async Task<ActionResult<BloodRequestDto>> GetBloodRequestById(int id)
        {
            var request = await _bloodRequestService.GetBloodRequestByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        [HttpPut("bloodrequests/{id}")]
        public async Task<IActionResult> UpdateBloodRequest(int id, BloodRequestDto requestDto)
        {
            if (id != requestDto.RequestId)
            {
                return BadRequest();
            }

            var request = _mapper.Map<BloodRequest>(requestDto);
            await _bloodRequestService.UpdateBloodRequestAsync(request);
            return NoContent();
        }

        [HttpDelete("bloodrequests/{id}")]
        public async Task<IActionResult> DeleteBloodRequest(int id)
        {
            await _bloodRequestService.DeleteBloodRequestAsync(id);
            return NoContent();
        }

        // Blogs
        [HttpGet("blogs")]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }

        [HttpPost("blogs")]
        public async Task<ActionResult<BlogDto>> CreateBlog(BlogDto blogDto)
        {
            var blog = _mapper.Map<Blog>(blogDto);
            await _blogService.AddBlogAsync(blog);
            return CreatedAtAction(nameof(GetBlogById), new { id = blog.BlogId }, blogDto);
        }

        [HttpGet("blogs/{id}")]
        public async Task<ActionResult<BlogDto>> GetBlogById(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpPut("blogs/{id}")]
        public async Task<IActionResult> UpdateBlog(int id, BlogDto blogDto)
        {
            if (id != blogDto.BlogId)
            {
                return BadRequest();
            }

            var blog = _mapper.Map<Blog>(blogDto);
            await _blogService.UpdateBlogAsync(blog);
            return NoContent();
        }

        [HttpDelete("blogs/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _blogService.DeleteBlogAsync(id);
            return NoContent();
        }

        // Events
        [HttpGet("events")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("events/upcoming")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetUpcomingEvents()
        {
            var events = await _eventService.GetUpcomingEventsAsync();
            return Ok(events);
        }

        [HttpPost("events")]
        public async Task<ActionResult<EventDto>> CreateEvent(EventDto eventDto)
        {
            var eventItem = _mapper.Map<Event>(eventDto);
            await _eventService.AddEventAsync(eventItem);
            return CreatedAtAction(nameof(GetEventById), new { id = eventItem.EventId }, eventDto);
        }

        [HttpGet("events/{id}")]
        public async Task<ActionResult<EventDto>> GetEventById(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem);
        }

        [HttpPut("events/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDto eventDto)
        {
            if (id != eventDto.EventId)
            {
                return BadRequest();
            }

            var eventItem = _mapper.Map<Event>(eventDto);
            await _eventService.UpdateEventAsync(eventItem);
            return NoContent();
        }

        [HttpDelete("events/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}