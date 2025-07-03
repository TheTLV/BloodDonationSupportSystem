using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Repositories.Implementations
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog.BlogId;
        }

        public async Task<Blog?> GetByIdAsync(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<IEnumerable<Blog>> SearchAsync(string keyword)
        {
            return await _context.Blogs
                .Where(b => b.Title.Contains(keyword) || b.Description.Contains(keyword))
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return false;

            _context.Blogs.Remove(blog);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
