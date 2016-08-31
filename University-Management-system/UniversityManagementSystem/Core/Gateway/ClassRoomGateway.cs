using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.Gateway
{
    public class ClassRoomGateway:DBGateway
    {
        public int Insert(ClassRoom room)
        {
            try
            {
                string query =
                    "INSERT INTO t_AllocateClassRoom VALUES(@deptId,@courseId,@roomId,@dayId,@StartTime,@endTime,@allocationStatus)";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@deptId", room.DepartmentId);
                CommandObj.Parameters.AddWithValue("@courseId", room.CourseId);
                CommandObj.Parameters.AddWithValue("@roomId", room.RoomId);
                CommandObj.Parameters.AddWithValue("@dayId", room.DayId);
                CommandObj.Parameters.AddWithValue("@startTime", room.StartTime.ToShortTimeString());
                CommandObj.Parameters.AddWithValue("@endTime", room.Endtime.ToShortTimeString());
                CommandObj.Parameters.AddWithValue("@allocationStatus", 1);
                ConnectionObj.Open();
                int rowAffected = CommandObj.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Could not saved",exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }

        }

        public List<ClassSchedule> GetAll
        {
            get
            {
                try
                {
                    List<ClassSchedule> scheduleList = new List<ClassSchedule>();
                    CommandObj.CommandText = "SELECT * FROM classSchedule";
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        ClassSchedule schedule = new ClassSchedule
                        {
                            CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            CourseCode = reader["Code"].ToString(),
                            CourseName =reader["Name"].ToString(),                          
                            Schedule = reader["Schedule_Inforamtion"].ToString()
                        };
                        scheduleList.Add(schedule);
                    }
                    reader.Close();
                    return scheduleList;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect class schedule",exception);
                }
                finally
                {
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                }

            }
        }



        public List<TempClassSchedule> GetAllClassSchedules
        {
            get
            {
                try
                {
                    List<TempClassSchedule> scheduleList = new List<TempClassSchedule>();
                    CommandObj.CommandText = "SELECT * FROM ScheduleOfClass";
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        TempClassSchedule schedule = new TempClassSchedule
                        {
                          
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            CourseCode = reader["Code"].ToString(),
                            CourseName = reader["Name"].ToString(),
                            RoomName = reader["Room_Name"].ToString(),
                            DayName = reader["Day_Name"].ToString(),
                            StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                            EndTime =Convert.ToDateTime(reader["EndTime"].ToString())
                        };
                        scheduleList.Add(schedule);
                    }
                    reader.Close();
                    return scheduleList;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect class schedule", exception);
                }
                finally
                {
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                }

            }
        }



        public List<TempClassSchedule> GetClassSchedulByStartAndEngingTime(DateTime startTime, DateTime endTime)
        {
            CommandObj.CommandText = " Select * from ScheduleOfClass where  StartTime BETWEEN CAST('"+startTime+"' As Time) AND CAST('"+endTime+"' As Time)";
            return null;

        }

        public List<ClassRoom> GetClassSchedulByStartAndEndingTime(int roomId, int dayId, DateTime startTime, DateTime endTime)
        {
            try
            {
                //CommandObj.CommandText = "Select * from t_AllocateClassRoom where  StartTime BETWEEN CAST('" + startTime +"' As Time) AND CAST('" + endTime + "' As Time) AND RoomId ='" + roomId +
                //                         "' AND DayId='" + dayId + "'";
                CommandObj.CommandText = "Select * from t_AllocateClassRoom Where DayId=" + dayId + " AND RoomId="+roomId+" AND AllocationStatus=" + 1;
                List<ClassRoom> tempClassSchedules = new List<ClassRoom>();
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                while (reader.Read())
                {
                    ClassRoom temp = new ClassRoom
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                        CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                        RoomId = Convert.ToInt32(reader["RoomId"].ToString()),
                        DayId = Convert.ToInt32(reader["DayId"].ToString()),
                        StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                        Endtime = Convert.ToDateTime(reader["EndTime"].ToString())

                    };
                    tempClassSchedules.Add(temp);
                }
                reader.Close();
                return tempClassSchedules;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to collect class schedule", exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }

        }

        public IEnumerable<TempClassSchedule> GetAllClassSchedulesByDeparmentId(int departmentId,int courseId)
        {
            try
            {
                List<TempClassSchedule> scheduleList = new List<TempClassSchedule>();
                CommandObj.CommandText = "SELECT * FROM ScheduleOfClass WHERE DepartmentId='" + departmentId + "' AND CourseId='" + courseId + "' AND AllocationStatus='" + 1 + "'";
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                while (reader.Read())
                {
                    TempClassSchedule schedule = new TempClassSchedule
                    {

                        DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                        CourseCode = reader["Code"].ToString(),
                        CourseName = reader["Name"].ToString(),
                        RoomName = reader["Room_Name"].ToString(),
                        DayName = reader["Day_Name"].ToString(),
                        StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                        EndTime = Convert.ToDateTime(reader["EndTime"].ToString()),
                        Status = Convert.ToBoolean(reader["AllocationStatus"])
                    };

                    scheduleList.Add(schedule);
                }
                
                reader.Close();
                //ConnectionObj.Close();
                return scheduleList;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to collect class schedule", exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            }

        }

        public List<ClassRoom> GetAllClassroomInformation
        {
            get
            {
                try
                {
                    List<ClassRoom> scheduleList = new List<ClassRoom>();
                    CommandObj.CommandText = "SELECT * FROM t_AllocateClassRoom";
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        ClassRoom classRoom = new ClassRoom
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            DayId = Convert.ToInt32(reader["DayId"].ToString()),
                            RoomId = Convert.ToInt32(reader["RoomId"].ToString()),
                            StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                            Endtime = Convert.ToDateTime(reader["EndTime"].ToString()),

                            AlloctionStaus = Convert.ToBoolean(reader["AllocationStatus"].ToString())
                        };
                        scheduleList.Add(classRoom);
                    }
                    reader.Close();
                    return scheduleList;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect class schedule", exception);
                }
                finally
                {
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                }

            }
        }

        public int UnAllocateClassRoom()
        {
            ConnectionObj.Open();
            SqlTransaction sqlTransaction = ConnectionObj.BeginTransaction();
            try
            {
                CommandObj.CommandText = "UPDATE t_AllocateClassRoom SET AllocationStatus=0";
                CommandObj.Transaction = sqlTransaction;
               int i=CommandObj.ExecuteNonQuery();
               
                sqlTransaction.Commit();
                return i;
            }
            catch (Exception exception)
            {
                sqlTransaction.Rollback();
                throw new Exception("Failed to UnAllocate Class Room", exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }
        }
    }
}