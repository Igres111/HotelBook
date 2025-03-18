using DataAccess.Context;
using DataAccess.Entities;
using Dtos.RoomDtos;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.RoomInterFaces;

namespace Service.Implementations.RoomRepository
{
    public class RoomRepo : IRoom
    {
        public readonly AppDbContext _context;
        public RoomRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ReceiveRoomDto>> GetAllRooms()
        {
            var allRooms = await _context.Rooms
                .Where(el => el.Delete == null)
                .Select(el => new ReceiveRoomDto()
                {
                    Id = el.Id,
                    RoomNumber = el.RoomNumber,
                    RoomCapacity = el.RoomCapacity,
                    Price = el.Price,
                    Description = el.Description,
                    HotelId = el.HotelId,
                    IsBooked = el.IsBooked,
                    RoomType = el.RoomType,
                })
                .ToListAsync();
            return allRooms;
        }
        public async Task CreateRoom(CreateRoomDto room)
        {
            var hotelExists = await _context.Hotels.FirstOrDefaultAsync(el => el.Id == room.HotelId);
            if (hotelExists != null)
            {
                if (hotelExists.Delete != null)
                {
                    throw new Exception("Hotel doesn't exist");
                }
                var newRoom = new Room()
                {
                    Id = Guid.NewGuid(),
                    RoomNumber = room.RoomNumber,
                    RoomCapacity = room.RoomCapacity,
                    Price = room.Price,
                    Description = room.Description,
                    HotelId = room.HotelId,
                    IsBooked = room.IsBooked,
                    RoomType = room.RoomType,
                    CreatedAt = DateTime.UtcNow
                };
                hotelExists.Rooms.Add(newRoom);
                await _context.Rooms.AddAsync(newRoom);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteRoom(Guid id)
        {
            var roomToDelete = await _context.Rooms.FirstOrDefaultAsync(el => el.Id == id);
            if (roomToDelete != null)
            {
                if (roomToDelete.Delete != null)
                {
                    throw new Exception("Room doesn't exist");
                }
                roomToDelete.Delete = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateRoom(UpdateRoomDto room)
        {
            var roomToUpdate = await _context.Rooms.FirstOrDefaultAsync(el => el.Id == room.Id && el.Delete == null);
            if (roomToUpdate == null)
            {
                throw new Exception("Room not found");
            }
            roomToUpdate.RoomNumber = room.RoomNumber ?? roomToUpdate.RoomNumber;
            roomToUpdate.Price = room.Price ?? roomToUpdate.Price;
            roomToUpdate.Description = !string.IsNullOrEmpty(room.Description) ? room.Description : roomToUpdate.Description;
            roomToUpdate.IsBooked = room.IsBooked ?? roomToUpdate.IsBooked;
            roomToUpdate.RoomType = room.RoomType ?? roomToUpdate.RoomType;
            roomToUpdate.RoomCapacity = room.RoomCapacity ?? roomToUpdate.RoomCapacity;
            await _context.SaveChangesAsync();
        }
    }
}
