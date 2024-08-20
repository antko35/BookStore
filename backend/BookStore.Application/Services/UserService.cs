using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class UserService : IUserService
    {
        private readonly PasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly JWTProvider _jwtProvider;
        public UserService(PasswordHasher passwordHasher,
            IUserRepository userRepository,
            JWTProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }
        public async Task Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email);

            await _userRepository.Add(user);
        }

        public async Task<string> Login(string password, string email)
        {
            var user = await _userRepository.GetByEmail(email);
            var result = _passwordHasher.Verify(password, user.PasswordHash);
            if (result == false)
            {
                throw new Exception("Invalid password");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
