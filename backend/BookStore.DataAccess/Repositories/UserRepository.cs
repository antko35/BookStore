using BookStore.Core.Enum;
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
        var roleEntity = await _context.Roles
            .SingleOrDefaultAsync(r => r.Id == (int)Role.User)
            ?? throw new InvalidOperationException();

        var userEntity = new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            Roles = new[] { roleEntity}
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

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var roles = await _context.User
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(u => u.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }

}