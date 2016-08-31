using System.Collections.Generic;
using UniversityManagementSystem.Core.Gateway;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.BLL
{
    public class RoomManager
    {
        RoomGateway roomGateway=new RoomGateway();
        public IEnumerable<Room> GetAllRooms
        {
            get { return roomGateway.GetAllRooms; }
        }
    }
}