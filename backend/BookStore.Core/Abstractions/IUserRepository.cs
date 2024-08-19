using BookStore.Core.Enum;
using BookStore.Core.Models;

public interface IUserRepository
{
    Task<Guid> Add(User user);
    Task<User> GetByEmail(string email);
    Task<HashSet<Permission>> GetUserPermissions(Guid userId);
}