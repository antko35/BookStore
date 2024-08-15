using BookStore.Core.Mappers;
using BookStore.Core.Models;
using BookStore.DataAccess;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
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

    public async Task<User> GetByEmail(string email)
    {
        var user = await _context.User
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("invalin email");

        return UserMappers.ToUserDto(user);
    }

}