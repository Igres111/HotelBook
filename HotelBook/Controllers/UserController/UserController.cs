using Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.TokenInterfaces;
using Service.Interfaces.UserInterfaces;

namespace HotelBook.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController : ControllerBase
    {
        public readonly IUser _user;
        public readonly IToken _tokenGenerator;
        public UserController(IUser user, IToken tokenGenerator)
        {
            _user = user;
            _tokenGenerator = tokenGenerator;
        }
        [HttpPost("Create-User")]
        public async Task<IActionResult> CreateUser(CreateUserDto userInfo)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }
            try
            {
                await _user.CreateUser(userInfo);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Log-In")]
        public async Task<IActionResult> LogInUser(LoginUserDto userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }
            try
            {
                await _user.LogInUser(userInfo);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("refresh-access-token")]
        public async Task<IActionResult> RefreshToken(string token)
        {
            try
            {
                var result = await _tokenGenerator.RefreshAccessTokenAsync(token);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Invalid Token");
            }
        }
    }
}
