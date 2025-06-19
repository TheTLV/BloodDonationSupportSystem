using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs.Include(b => b.User).ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs.Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BlogId == id);
        }

        public async Task AddBlogAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(int id)
        {
            var blog = await GetBlogByIdAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync();
        Task<Blog> GetBlogByIdAsync(int id);
        Task AddBlogAsync(Blog blog);
        Task UpdateBlogAsync(Blog blog);
        Task DeleteBlogAsync(int id);
    }
}