using System;

public interface IUserRepository
{
    User GetById(int id);
    User GetByEmail(string email);
    void Create(User user);
    void Update(User user);
    void Delete(int id);
    List<User> GetAll();
}
