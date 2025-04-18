using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.BookingDtos
{
    public class FulfillBookingDto
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public string GuestEmail { get; set; } = string.Empty;
        public string GuestPhone { get; set; } = string.Empty;
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
    }
}
