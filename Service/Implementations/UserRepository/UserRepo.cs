using DataAccess.Context;
using DataAccess.Entities;
using Dtos.UserDtos;
using Service.Interfaces.UserInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations.UserRepository
{
    public class UserRepo: IUser
    {
        public readonly AppDbContext _context;
        public UserRepo(AppDbContext context)
        {
            _context = context;
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
    }
}
