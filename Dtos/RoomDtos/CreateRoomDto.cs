using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Enums.Enums;

namespace Dtos.RoomDtos
{
    public class CreateRoomDto
    {
        public string RoomNumber { get; set; } = string.Empty;
        public int RoomCapacity { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsBooked { get; set; }
        public Guid HotelId { get; set; }
        public RoomType RoomType { get; set; }
    }
}
