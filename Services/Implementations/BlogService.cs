using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs.BlogDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repo;

        public BlogService(IBlogRepository repo)
        {
            _repo = repo;
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

            return await _repo.CreateAsync(blog);
        }

        public async Task<bool> UpdateBlogAsync(int blogId, BlogUpdateDTO dto)
        {
            var blog = await _repo.GetByIdAsync(blogId);
            if (blog == null) return false;

            blog.Title = dto.Title ?? blog.Title;
            blog.Image = dto.Image ?? blog.Image;
            blog.Link = dto.Link ?? blog.Link;
            blog.Description = dto.Description ?? blog.Description;

            return await _repo.UpdateAsync(blog);
        }

        public async Task<bool> DeleteBlogAsync(int blogId)
        {
            return await _repo.DeleteAsync(blogId);
        }

        public async Task<BlogDetailDTO?> GetBlogByIdAsync(int blogId)
        {
            var blog = await _repo.GetByIdAsync(blogId);
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
            var blogs = await _repo.GetAllAsync();
            return blogs.Select(b => new BlogDetailDTO
            {
                BlogId = b.BlogId,
                Title = b.Title,
                Image = b.Image,
                Link = b.Link,
                Description = b.Description
            });
        }

        public async Task<IEnumerable<BlogDetailDTO>> SearchBlogsByTitleOrDesAsync(string keyword)
        {
            var blogs = await _repo.SearchAsync(keyword);
            return blogs.Select(b => new BlogDetailDTO
            {
                BlogId = b.BlogId,
                Title = b.Title,
                Image = b.Image,
                Link = b.Link,
                Description = b.Description
            });
        }
    }

}
