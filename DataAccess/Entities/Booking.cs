namespace DataAccess.Entities
{
    public class Booking : BaseEntity
    {
        public Guid Id { get; set; }
        public string DestinationCountry { get; set; } = string.Empty;
        public string DestinationCity { get; set; } = string.Empty;
        public string GuestName { get; set; } = string.Empty;
        public string GuestEmail { get; set; } = string.Empty;
        public string GuestPhone { get; set; } = string.Empty;
        public int NumberOfGuests { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Guid? RoomId { get; set; }
        public Room? Room { get; set; }

    }
}
