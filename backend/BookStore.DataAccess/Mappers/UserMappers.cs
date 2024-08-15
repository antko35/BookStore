using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Mappers
{
    public static class UserMappers
    {
        public static User ToUserDto(UserEntity userEntity)
        {
            return User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email);
        }
    }
}
