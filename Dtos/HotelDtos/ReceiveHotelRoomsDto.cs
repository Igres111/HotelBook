using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.HotelDtos
{
    public class ReceiveHotelRoomsDto
    {
        public Guid HotelId { get; set; }
        public Guid BookingId { get; set; }
    }
}
