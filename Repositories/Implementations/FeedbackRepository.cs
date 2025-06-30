using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs.FeedbackDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Repositories.Implementations
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public List<FeedbackViewDTO> GetAllFeedback()
        {
            return _context.Feedbacks
                .Include(f => f.CreatedByNavigation)
                .OrderByDescending(f => f.FeedbackDate)
                .Select(f => new FeedbackViewDTO
                {
                    FeedbackId = f.FeedbackId,
                    Content = f.Content,
                    FeedbackDate = f.FeedbackDate,
                    CreatedByName = f.CreatedByNavigation != null ? f.CreatedByNavigation.Fullname : "N/A",
                    Email = f.CreatedByNavigation != null ? f.CreatedByNavigation.Email : "N/A"
                })
                .ToList();
        }


        public List<Feedback> SearchFeedback(string keyword)
        {
            return _context.Feedbacks
                .Where(f => f.Content.Contains(keyword))
                .OrderByDescending(f => f.FeedbackDate)
                .ToList();
        }
    }

}

