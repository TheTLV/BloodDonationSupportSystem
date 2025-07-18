﻿using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAndPassword(string email, string password);

        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneAsync(string phone);
        Task AddUserAsync(User user);

        Task<User?> GetUserWithProfileAsync(int userId);
        Task<User?> GetUserWithRoleAndProfileAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> UpdateUserStatusAsync(int userId,int newStatusId);
        Task<bool> UpdateUserRoleAsync(int userId, int newRoleId);

        Task<Bloodrequest?> GetRequestByIdAndUserAsync(int requestId, int userId);
        Task<Donation?> GetDonationByIdAndUserAsync(int donationId, int userId);
        Task<bool> SaveChangesAsync();
        void AddProfile(Profile profile);
        void RemoveDonation(Donation donation);
        void RemoveBloodRequest(Bloodrequest request);

    }

}
