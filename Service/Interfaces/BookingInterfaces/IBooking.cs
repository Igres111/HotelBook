using Dtos.BookingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.BookingInterfaces
{
    public interface IBooking
    {
        public Task CreateBooking(CreateSearchBookingDto info);
        public Task<List<ReceiveBookingDto>> GetBookingByDest(string destination);
    }
}
