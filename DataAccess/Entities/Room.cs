using static DataAccess.Enums.Enums;

namespace DataAccess.Entities
{
    public class Room : BaseEntity
    {
        public Guid Id { get; set; }
        public int RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsBooked { get; set; }
        public Hotel Hotel { get; set; }
        public Guid HotelId { get; set; }
        public RoomType RoomType { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
