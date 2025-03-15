using DataAccess.Entities;
using Dtos.HotelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.HotelInterfaces
{
    public interface IHotel
    {
        public Task<List<ReceiveHotelDto>> GetAllHotels();
        public Task<ReceiveHotelDto> GetHotelById(Guid id);
        public Task<string> GetHotelImage(Guid id);
        public Task CreateHotel(CreateHotelDto hotel);
        public Task UpdateHotel(UpdateHotelDto hotel);
        public Task DeleteHotel(Guid id);
    }
}
