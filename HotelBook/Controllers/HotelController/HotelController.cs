using Dtos.HotelDtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.HotelInterfaces;

namespace HotelBook.Controllers.HotelControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        public readonly IHotel _methods;
        public HotelController(IHotel methods)
        {
            _methods = methods;
        }
        [HttpGet("All-Hotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _methods.GetAllHotels();
            return Ok(hotels);
        }
        [HttpGet("Hotels-By-Id")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            try
            {
                var hotel = await _methods.GetHotelById(id);
                return Ok(hotel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet("Hotel-Image")]
        public async Task<IActionResult> GetHotelImage(Guid id)
        {
            try
            {
                var hotelImage = await _methods.GetHotelImage(id);
                return Ok(hotelImage);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateHotel(CreateHotelDto hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _methods.CreateHotel(hotel);
                return Ok("Hotel created successfully");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("Update-Hotel")]
        public async Task<IActionResult> UpdateHotel(UpdateHotelDto hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _methods.UpdateHotel(hotel);
                return Ok("Hotel updated successfully");
            }
            catch (Exception)
            {
                return BadRequest("Wrong Credentials");
            }
        }
        [HttpDelete("Delete-Hotel")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            try
            {
                await _methods.DeleteHotel(id);
                return Ok("Hotel deleted successfully");
            }
            catch (Exception)
            {
                return NotFound("Hotel not found");
            }
        }
    }
}
