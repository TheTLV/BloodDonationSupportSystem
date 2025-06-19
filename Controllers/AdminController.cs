using AutoMapper;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly BloodRequestService _bloodRequestService;
        private readonly BlogService _blogService;
        private readonly EventService _eventService;
        private readonly IMapper _mapper;

        public AdminController(
            UserService userService,
            BloodRequestService bloodRequestService,
            BlogService blogService,
            EventService eventService,
            IMapper mapper)
        {
            _userService = userService;
            _bloodRequestService = bloodRequestService;
            _blogService = blogService;
            _eventService = eventService;
            _mapper = mapper;
        }

        // Users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("users")]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, userDto);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userDto);
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        // Blood Requests (Admin can access all requests)
        [HttpGet("bloodrequests")]
        public async Task<ActionResult<IEnumerable<BloodRequestDto>>> GetAllBloodRequests()
        {
            var requests = await _bloodRequestService.GetAllBloodRequestsAsync();
            return Ok(requests);
        }

        // Blogs (Admin can access all blogs)
        [HttpGet("blogs")]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }

        // Events (Admin can access all events)
        [HttpGet("events")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // Dashboard Statistics
        [HttpGet("dashboard/stats")]
        public async Task<ActionResult<object>> GetDashboardStats()
        {
            var users = await _userService.GetAllUsersAsync();
            var requests = await _bloodRequestService.GetAllBloodRequestsAsync();
            var blogs = await _blogService.GetAllBlogsAsync();
            var events = await _eventService.GetAllEventsAsync();

            var stats = new
            {
                TotalUsers = users.Count(),
                TotalStaff = users.Count(u => u.Role == "Staff"),
                TotalMembers = users.Count(u => u.Role == "Member"),
                TotalBloodRequests = requests.Count(),
                PendingRequests = requests.Count(r => r.Status == "Pending"),
                CompletedRequests = requests.Count(r => r.Status == "Completed"),
                TotalBlogs = blogs.Count(),
                TotalEvents = events.Count(),
                UpcomingEvents = events.Count(e => e.Status == "Upcoming")
            };

            return Ok(stats);
        }
    }
}