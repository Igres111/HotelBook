using Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.UserInterfaces
{
    public interface IUser
    {
        public Task CreateUser(CreateUserDto userInfo);
        public Task<string> LogInUser(LoginUserDto userInfo);
        public Task ChangeUser(ChangeUserDto user);
    }
}
