using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Enums.Enums;

namespace Dtos.RoomDtos
{
    public class UpdateRoomDto
    {
        public Guid Id { get; set; }
        public string? RoomNumber { get; set; } = string.Empty;
        public int? RoomCapacity { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public bool? IsBooked { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int DiscountPercent { get; set; }
        public RoomType? RoomType { get; set; }
    }
}
