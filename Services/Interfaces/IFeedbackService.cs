using BloodDonationSupportSystem.DTOs.FeedbackDTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IFeedbackService
    {
        void SendFeedback(int userId, FeedbackCreateDTO dto);
        List<FeedbackViewDTO> GetAll();
        List<Feedback> Search(string keyword);
    }

}
