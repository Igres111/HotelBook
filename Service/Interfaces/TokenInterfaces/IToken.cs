using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.TokenInterfaces
{
    public interface IToken
    {
        public string CreateAccessToken(User user);
        public Task<RefreshToken> CreateRefreshTokenAsync(User user);
        public Task<string> RefreshAccessTokenAsync(string refreshTokenInput);
    }
}
