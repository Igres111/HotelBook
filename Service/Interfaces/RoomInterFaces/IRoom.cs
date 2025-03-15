using Dtos.RoomDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.RoomInterFaces
{
    public interface IRoom
    {
        public Task<List<ReceiveRoomDto>> GetAllRooms();
        public Task CreateRoom(CreateRoomDto room);
        public Task DeleteRoom(Guid id);
        public Task UpdateRoom(UpdateRoomDto room);
    }
}
