using BookStore.Core.Models;
using BookStore.DataAccess;
using BookStore.DataAccess.Entities;

public class UserRepository
{
    private readonly BookStoreDbContext _context;
    public UserRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Add(User user)
    {
        var userEntity = new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email
        };

        await _context.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        return userEntity.Id;
    }

    /*public async Task<User> GetById(Guid id)
    {
        var user = await _context.User
            .;
    }*/
}