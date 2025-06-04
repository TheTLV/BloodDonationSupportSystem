using System;

public class User
{
    public int UId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public int PhoneNumber { get; set; }

    public User(int uId, string userName, string password, string email, int roleId, int phoneNumber)
    {
        UId = uId;
        UserName = userName;
        Password = password;
        Email = email;
        RoleId = roleId;
        PhoneNumber = phoneNumber;
    }
    public User() { }
}
