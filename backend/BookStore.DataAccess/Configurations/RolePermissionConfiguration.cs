using BookStore.Core.Enum;
using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        private readonly AuthorizationOptions _authorization;
        public RolePermissionConfiguration(AuthorizationOptions authorization)
        {
            _authorization =  authorization;
        }
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(r => new { r.RoleId, r.PermissionId });

            builder.HasData(ParseRolePermissions());
        }

        private RolePermissionEntity[] ParseRolePermissions()
        {
            return _authorization.RolePermissions
                .SelectMany(rp => rp.Permissions
                    .Select(p => new RolePermissionEntity
                    {
                        RoleId = (int)Enum.Parse<Role>(rp.Role),
                        PermissionId = (int)Enum.Parse<Permission>(p)
                    }))
                    .ToArray();
        }
    }
}
