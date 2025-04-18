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
        public string DestinationCountry { get; set; } = string.Empty;
        public string DestinationCity { get; set; } = string.Empty;
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
    }
}
