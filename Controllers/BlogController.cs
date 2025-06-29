using BloodDonationSupportSystem.DTOs.BlogDTOs;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "2,3")]
    public class BlogController: ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BlogCreateDTO dto)
        {
            var id = await _blogService.CreateBlogAsync(dto);
            return Ok(new { message = "Tạo blog thành công"});
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BlogUpdateDTO dto)
        {
            var success = await _blogService.UpdateBlogAsync(id, dto);
            if (!success) return NotFound(new { message = "Không tìm thấy blog" });
            return Ok(new { message = "Cập nhật blog thành công" });
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _blogService.DeleteBlogAsync(id);
            if (!success) return NotFound(new { message = "Không tìm thấy blog" });
            return Ok(new { message = "Xóa blog thành công" });
        }

        
        [HttpGet("getAllBlog")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }


        [HttpGet("searchByTitleOrDecription")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var blogs = await _blogService.SearchBlogsByTitleOrDesAsync(keyword);
            return Ok(blogs);
        }


        [HttpGet("getBlog/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null) return NotFound(new { message = "Không tìm thấy blog" });
            return Ok(blog);
        }
    }
}
