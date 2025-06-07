using System;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext con;

    public UserRepository(AppDbContext context)
    {
        con = context;
    }

    public User GetById(int id) => con.Users.Find(id);

    public User GetByEmail(string email) =>
        con.Users.FirstOrDefault(u => u.Email == email);

    public void Create(User user)
    {
        con.Users.Add(user);
        con.SaveChanges();
    }

    public void Update(User user)
    {
        con.Users.Update(user);
        con.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = con.Users.Find(id);
        if (user != null)
        {
            con.Users.Remove(user);
            con.SaveChanges();
        }
    }

    public List<User> GetAll() => con.Users.ToList();
}
