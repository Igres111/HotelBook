using DataAccess.Context;
using DataAccess.Entities;
using Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.TokenInterfaces;
using Service.Interfaces.UserInterfaces;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations.UserRepository
{
    public class UserRepo: IUser
    {
        public readonly AppDbContext _context;
        public readonly IToken _tokenGenerator;

        public UserRepo(AppDbContext context, IToken tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }
        public async Task CreateUser(CreateUserDto user)
        {
            var userExists = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (userExists != null)
            {
                throw new Exception("User already exists");
            }
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Phone = user.Phone,
                Address = user.Address,
                AboutMe = user.AboutMe,
                Role = user.Role,
                CreatedAt = DateTime.UtcNow
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }
        public async Task<string> LogInUser(LoginUserDto user)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (userExists == null)
            {
                throw new Exception("User does not exist");
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Password, userExists.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid password");
            }
            var refreshToken = await _tokenGenerator.CreateRefreshTokenAsync(userExists);
            var accessToken = _tokenGenerator.CreateAccessToken(userExists);
            await _context.SaveChangesAsync();
            return accessToken;
        }
    }
}
