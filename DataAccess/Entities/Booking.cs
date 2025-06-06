﻿using DataAccess.Enums;
using static DataAccess.Enums.EnumBookingStatus;

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
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public Guid? RoomId { get; set; }
        public Room? Room { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
