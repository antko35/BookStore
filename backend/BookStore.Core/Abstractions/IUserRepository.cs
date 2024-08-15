using BookStore.Core.Models;

public interface IUserRepository
{
    Task<Guid> Add(User user);
    Task<User> GetByEmail(string email);
}