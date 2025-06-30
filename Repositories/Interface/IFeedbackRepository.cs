using BloodDonationSupportSystem.DTOs.FeedbackDTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface IFeedbackRepository
    {
        List<FeedbackViewDTO> GetAllFeedback();
        void AddFeedback(Feedback feedback);   
        List<Feedback> SearchFeedback(string keyword); 
    }

}
