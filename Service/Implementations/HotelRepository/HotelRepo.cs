using DataAccess.Context;
using DataAccess.Entities;
using Dtos.HotelDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces.HotelInterfaces;


namespace Service.Implementations.HotelRepository
{
    public class HotelRepo : IHotel
    {
        public readonly AppDbContext _context;
        public HotelRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ReceiveHotelDto>> GetAllHotels()
        {
            var hotels = await _context.Hotels
                .Where(el => el.Delete == null)
                .Select(el => new ReceiveHotelDto()
            {
                Id = el.Id,
                Name = el.Name,
                Address = el.Address,
                City = el.City,
                Country = el.Country,
                PostalCode = el.PostalCode,
                Phone = el.Phone,
                Email = el.Email,
                HotelImage = el.HotelImage,
                Rating = el.Rating
            }).ToListAsync();
            return hotels;
        }
        public async Task<ReceiveHotelDto> GetHotelById(Guid id)
        {
            var hotel = await _context.Hotels
                .Where(el => el.Id == id && el.Delete == null)
                .Select(x => new ReceiveHotelDto()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                Country = x.Country,
                PostalCode = x.PostalCode,
                Phone = x.Phone,
                Email = x.Email,
                HotelImage = x.HotelImage,
                Rating = x.Rating
            }).FirstOrDefaultAsync();
            if (hotel == null)
            {
                throw new Exception("Hotel not found");
            }
            return hotel;
        }
        public async Task<string> GetHotelImage(Guid id)
        {
            var hotel = await _context.Hotels
                .Where(el => el.Id == id && el.Delete == null)
                .Select(id => id.HotelImage).FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(hotel))
            {
                throw new Exception("Hotel Image not found");
            }
            return hotel;
        }

        public async Task CreateHotel(CreateHotelDto hotel)
        {
            var newHotel = new Hotel()
            {
                Id = Guid.NewGuid(),
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country,
                PostalCode = hotel.PostalCode,
                Phone = hotel.Phone,
                Email = hotel.Email,
                HotelImage = hotel.HotelImage,
                CreatedAt = DateTime.UtcNow
            };
            await _context.Hotels.AddAsync(newHotel);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateHotel(UpdateHotelDto hotel)
        {
            var hotelToUpdate = await _context.Hotels.FirstOrDefaultAsync(el => el.Id == hotel.Id && el.Delete == null);
            if (hotelToUpdate == null)
            {
                throw new Exception("Hotel not found");
            }
            hotelToUpdate.Name = !string.IsNullOrEmpty(hotel.Name) ? hotel.Name : hotelToUpdate.Name;
            hotelToUpdate.Address = !string.IsNullOrEmpty(hotel.Address) ? hotel.Address : hotelToUpdate.Address;
            hotelToUpdate.City = !string.IsNullOrEmpty(hotel.City) ? hotel.City : hotelToUpdate.City; ;
            hotelToUpdate.Country = !string.IsNullOrEmpty(hotel.Country) ? hotel.Country : hotelToUpdate.Country;
            hotelToUpdate.PostalCode = !string.IsNullOrEmpty(hotel.PostalCode) ? hotel.PostalCode : hotelToUpdate.PostalCode;
            hotelToUpdate.Phone = !string.IsNullOrEmpty(hotel.Phone) ? hotel.Phone : hotelToUpdate.Phone;
            hotelToUpdate.Email = !string.IsNullOrEmpty(hotel.Email) ? hotel.Email : hotelToUpdate.Email;
            hotelToUpdate.HotelImage = !string.IsNullOrEmpty(hotel.HotelImage) ? hotel.HotelImage : hotelToUpdate.HotelImage;
            hotelToUpdate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteHotel(Guid id)
        {
            var hotelToDelete = await _context.Hotels.FirstOrDefaultAsync(el => el.Id == id);
            if (hotelToDelete.Delete == null)
            {
                hotelToDelete.Delete = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
        }
    }
}
