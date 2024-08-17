using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();

    }
}
