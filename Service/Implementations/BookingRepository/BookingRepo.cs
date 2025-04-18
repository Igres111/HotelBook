using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Enums;
using Dtos.BookingDtos;
using Dtos.HotelDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Service.Interfaces.BookingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Enums.EnumBookingStatus;

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
                BookingStatus = BookingStatus.Pending,
            };
            Console.WriteLine(newBooking.BookingStatus);
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
        public async Task FulfillBooking(FulfillBookingDto info)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(el => el.Id == info.BookingId);
            if (booking == null)
            {
                throw new Exception("Booking not found");
            }
            var room = await _context.Rooms.FirstOrDefaultAsync(el => el.Id == info.RoomId);
            if (room == null)
            {
                throw new Exception("Room not found");
            }
            booking.GuestName = info.GuestName;
            booking.GuestEmail = info.GuestEmail;
            booking.GuestPhone = info.GuestPhone;
            booking.CheckIn = info.CheckIn;
            booking.CheckOut = info.CheckOut;
            booking.BookingStatus = BookingStatus.Confirmed;
            if (room.Bookings != null)
            {
                room.Bookings.Add(booking);
            }
            else
            {
                room.Bookings = new List<Booking> { booking };
            }

                await _context.SaveChangesAsync();
        }
        public async Task<ShowBookingDto> ShowBooking(Guid bookingId)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(el => el.Id == bookingId);
            if (booking == null)
            {
                throw new Exception("Booking not found");
            }
            var room = await _context.Rooms.FirstOrDefaultAsync(el => el.Id == booking.RoomId);
            if (booking.RoomId == null || room == null)
            {
                throw new Exception("Room not found");
            }
            var result = new ShowBookingDto()
            {
                DestinationCity = booking.DestinationCity,
                NumberOfGuests = booking.NumberOfGuests,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                DaysLength = booking.CheckOut.DayNumber - booking.CheckIn.DayNumber,
                RoomNumber = room.RoomNumber,
                RoomCapacity = room.RoomCapacity,
                Price = (decimal)(room.DiscountPrice != null ? room.DiscountPrice : room.Price),
                Description = room.Description,
                RoomType = room.RoomType,
                DiscountPercent = room.DiscountPercent != 0 ? room.DiscountPercent : null ,
            };
            booking.BookingStatus = BookingStatus.Confirmed;
            return result;
        }
    }
}
