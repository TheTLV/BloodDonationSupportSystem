using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Model;

namespace BloodDonationSupportSystem.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public User? Register(string name, string email, string password, int? phone)
        {
            if (_context.Users.Any(u => u.Email == email))
                return null;

            var user = new User
            {
                Name = name,
                Email = email,
                Password = password,
                PhoneNumber = phone
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }


        public User? Login(string email, string password)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
