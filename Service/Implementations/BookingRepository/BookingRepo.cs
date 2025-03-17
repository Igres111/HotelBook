using DataAccess.Context;
using DataAccess.Entities;
using Dtos.BookingDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Service.Interfaces.BookingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations.BookingRepository
{   
    public class BookingRepo : IBooking
    {
        public readonly AppDbContext _context;
        public BookingRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateBooking(CreateSearchBookingDto info)
        {
            var newBooking = new Booking()
            {
                Id= Guid.NewGuid(),
                NumberOfGuests = info.NumberOfGuests,
                CheckIn = info.CheckIn,
                CheckOut = info.CheckOut,
                DestinationCity = info.DestinationCity,
                DestinationCountry = info.DestinationCountry,
                GuestName = string.Empty,
                GuestEmail = string.Empty,
                GuestPhone = string.Empty,
                CreatedAt = DateTime.Now,
            };
            _context.Bookings.Add(newBooking);
            await _context.SaveChangesAsync();
            return newBooking.Id;
        }
        public async Task<List<ReceiveBookingDto>> GetBookingByDest(string destination)
        {
            var bookingList = await _context.Bookings
                .Where(x => x.DestinationCity == destination)
                .Select(x => new ReceiveBookingDto()
                {
                    Destination = x.DestinationCity,
                    GuestName = x.GuestName,
                    GuestEmail = x.GuestEmail,
                    GuestPhone = x.GuestPhone,
                    NumberOfGuests = x.NumberOfGuests,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    CreatedAt = x.CreatedAt,
                    Delete = x.Delete,
                    UpdatedAt = x.UpdatedAt,
                })
                .ToListAsync();
            if(bookingList == null)
            {
               throw new Exception("No booking found");
            }
            return bookingList;
        }
    }
}
