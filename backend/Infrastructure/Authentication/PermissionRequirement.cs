using BookStore.Core.Enum;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Authentication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public Permission[] Permissions { get; set; } = new Permission[0];
    }
}
