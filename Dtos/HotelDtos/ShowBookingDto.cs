using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Enums.Enums;

namespace Dtos.HotelDtos
{
    public class ShowBookingDto
    {
        public string DestinationCity { get; set; } = string.Empty;
        public int NumberOfGuests { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public int DaysLength { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int RoomCapacity { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public RoomType RoomType { get; set; }
    }
}
