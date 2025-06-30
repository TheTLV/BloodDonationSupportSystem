using BloodDonationSupportSystem.DTOs.FeedbackDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services.Interfaces;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repo;

        public FeedbackService(IFeedbackRepository repo)
        {
            _repo = repo;
        }

        public void SendFeedback(int userId, FeedbackCreateDTO dto)
        {
            var feedback = new Feedback
            {
                CreatedBy = userId,
                Content = dto.Content?.Trim(),
                FeedbackDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _repo.AddFeedback(feedback);
        }

        public List<FeedbackViewDTO> GetAll() => _repo.GetAllFeedback();

        public List<Feedback> Search(string keyword) => _repo.SearchFeedback(keyword);
    }

}
