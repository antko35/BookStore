using BookStore.Core.Enum;

namespace BookStore.Application.Services
{
    public interface IPermissionService
    {
        Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
    }
}