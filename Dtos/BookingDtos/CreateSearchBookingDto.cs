using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.BookingDtos
{
    public class CreateSearchBookingDto
    {
        public int NumberOfGuests { get; set; }
        public string Destination { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
