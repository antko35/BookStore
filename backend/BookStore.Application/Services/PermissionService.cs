using BookStore.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUserRepository _userRepository;
        public PermissionService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
        {
            return _userRepository.GetUserPermissions(userId);
        }
    }
}
