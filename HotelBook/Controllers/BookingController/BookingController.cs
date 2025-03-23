﻿using Dtos.BookingDtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.BookingInterfaces;


namespace HotelBook.Controllers.BookingController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public readonly IBooking _methods;
        public BookingController(IBooking methods)
        {
            _methods = methods;
        }
        [HttpPost("Create-Booking")]
        public async Task<IActionResult> CreateBooking(CreateSearchBookingDto info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var resultId = await _methods.CreateBooking(info);
                return Ok(resultId);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with booking");
            }
        }
        [HttpGet("Get-Booking-By-Destination")]
        public async Task<IActionResult> ReceiveBookingDto(string destination)
        {
            try
            {
                var bookingList = await _methods.GetBookingByDest(destination);
                return Ok(bookingList);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("Fulfill-Booking")]
        public async Task<IActionResult> FulfillBooking(FulfillBookingDto info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _methods.FulfillBooking(info);
                return Ok("Booking fulfilled");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("Show-Booking")]
        public async Task<IActionResult> ShowBooking(Guid roomId)
        {
            try
            {
                var booking = await _methods.ShowBooking(roomId);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
