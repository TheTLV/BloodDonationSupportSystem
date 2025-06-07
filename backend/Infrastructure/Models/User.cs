using System;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public int RoleId { get; set; }
    public string Fullname { get; set; }

    public User(int userId, string username, string email, string password, string phoneNumber, int roleId, string fullname)
    {
        UserId = userId;
        Username = username;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        RoleId = roleId;
        Fullname = fullname;
    }

    public Role Role { get; set; }
    public Profile Profile { get; set; }
    public ICollection<Donation> Donations { get; set; }
    public ICollection<BloodRequest> BloodRequests { get; set; }

}
