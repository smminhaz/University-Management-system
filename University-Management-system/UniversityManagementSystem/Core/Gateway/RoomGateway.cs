using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.Gateway
{
    public class RoomGateway:DBGateway
    {
        public IEnumerable<Room> GetAllRooms
        {
            get
            {
                try
                {
                    var roomList = new List<Room>();
                    CommandObj.CommandText = "SELECT * FROM t_Room";
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();

                    while (reader.Read())
                    {
                        var room = new Room
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Name = reader["Name"].ToString()
                        };
                        roomList.Add(room);
                    }
                    reader.Close();
                    return roomList;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect Rooms", exception);
                }
                finally
                {
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                }
            }
        } 
    }
}