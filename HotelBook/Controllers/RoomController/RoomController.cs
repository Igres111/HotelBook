using Dtos.RoomDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.RoomInterFaces;

namespace HotelBook.Controllers.RoomController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        public readonly IRoom _methods;
        public RoomController(IRoom methods)
        {
            _methods = methods;
        }

        [HttpGet("Get-All-Rooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            try
            {
                var result = await _methods.GetAllRooms();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("Create-Room")]
        public async Task<IActionResult> CreateRoom(CreateRoomDto room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _methods.CreateRoom(room);
                return Ok("Room Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete-Room")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            try
            {
                await _methods.DeleteRoom(id);
                return Ok("Room Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update-Room")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomDto room)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid data");
            }
            try
            {
                await _methods.UpdateRoom(room);
                return Ok("Room Updated");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        }
}
