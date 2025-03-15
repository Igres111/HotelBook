using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Enums.Enums;

namespace Dtos.RoomDtos
{
    public class ReceiveRoomDto
    {
        public Guid Id { get; set; }
        public int RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsBooked { get; set; }
        public Guid HotelId { get; set; }
        public RoomType RoomType { get; set; }
    }
}
