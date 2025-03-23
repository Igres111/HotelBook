using Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.UserInterfaces;

namespace HotelBook.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController : ControllerBase
    {
        public readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        [HttpPost]
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
    }
}
