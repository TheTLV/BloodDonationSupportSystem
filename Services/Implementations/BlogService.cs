using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs.BlogDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBlogAsync(BlogCreateDTO dto)
        {
            var blog = new Blog
            {
                Title = dto.Title,
                Image = dto.Image,
                Link = dto.Link,
                Description = dto.Description
            };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog.BlogId;
        }

        public async Task<bool> UpdateBlogAsync(int blogId, BlogUpdateDTO dto)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) return false;

            blog.Title = dto.Title ?? blog.Title;
            blog.Image = dto.Image ?? blog.Image;
            blog.Link = dto.Link ?? blog.Link;
            blog.Description = dto.Description ?? blog.Description;

            _context.Blogs.Update(blog);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBlogAsync(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) return false;

            _context.Blogs.Remove(blog);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<BlogDetailDTO> GetBlogByIdAsync(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null) return null;

            return new BlogDetailDTO
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Image = blog.Image,
                Link = blog.Link,
                Description = blog.Description
            };
        }

        public async Task<IEnumerable<BlogDetailDTO>> GetAllBlogsAsync()
        {
            return await _context.Blogs
                .Select(b => new BlogDetailDTO
                {
                    BlogId = b.BlogId,
                    Title = b.Title,
                    Image = b.Image,
                    Link = b.Link,
                    Description = b.Description
                }).ToListAsync();
        }

        public async Task<IEnumerable<BlogDetailDTO>> SearchBlogsByTitleOrDesAsync(string keyword)
        {
            return await _context.Blogs
                .Where(b =>
                    b.Title.Contains(keyword) ||
                    b.Description.Contains(keyword))
                .Select(b => new BlogDetailDTO
                {
                    BlogId = b.BlogId,
                    Title = b.Title,
                    Image = b.Image,
                    Link = b.Link,
                    Description = b.Description
                }).ToListAsync();
        }
    }
}
