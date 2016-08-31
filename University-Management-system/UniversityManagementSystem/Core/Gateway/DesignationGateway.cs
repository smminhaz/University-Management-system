using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.Gateway
{
    public class DesignationGateway:DBGateway
    {
        public IEnumerable<Designation> GetAll
        {
            get
            {
                try
                {
                    List<Designation> desingantionlist = new List<Designation>();
                    string query = "SELECT * FROM t_Designation";
                    CommandObj.CommandText = query;
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();

                    while (reader.Read())
                    {
                        Designation designation = new Designation
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Title = reader["Title"].ToString()
                        };
                        desingantionlist.Add(designation);
                    }

                    reader.Close();

                    return desingantionlist;
                }
                catch (Exception exception)
                {
                    throw  new Exception("Could not collect Designations",exception);
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