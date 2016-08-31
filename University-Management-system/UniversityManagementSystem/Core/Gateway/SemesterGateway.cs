using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.Gateway
{
    public class SemesterGateway:DBGateway
    {
        public List<Semester> GetAll
        {
            get
            {
                try
                {
                    string query = "SELECT * FROM t_Semester";
                    CommandObj.CommandText = query;
                    List<Semester> semesters = new List<Semester>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        Semester semester = new Semester
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            
                        };
                        semesters.Add(semester);
                    }
                    reader.Close();
                    return semesters;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to connect semster", exception);
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