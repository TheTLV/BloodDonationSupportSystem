using AutoMapper;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories;

namespace BloodDonationSupportSystem.Services
{
    public class BlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDto>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllBlogsAsync();
            return _mapper.Map<IEnumerable<BlogDto>>(blogs);
        }

        public async Task<BlogDto> GetBlogByIdAsync(int id)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(id);
            return _mapper.Map<BlogDto>(blog);
        }

        public async Task AddBlogAsync(Blog blog)
        {
            await _blogRepository.AddBlogAsync(blog);
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            await _blogRepository.UpdateBlogAsync(blog);
        }

        public async Task DeleteBlogAsync(int id)
        {
            await _blogRepository.DeleteBlogAsync(id);
        }
    }
}