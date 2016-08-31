using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.Gateway
{
    public class DepartmentGateway:DBGateway
    {

        public IEnumerable<Department> GetAll()
        {
            try
            {
                string query = "SELECT * FROM t_Departments";
                CommandObj.CommandText = query;
               List<Department> departments=new List<Department>();
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                while (reader.Read())
                {
                    Department department = new Department
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Name =  reader["Name"].ToString(),
                        Code = reader["Code"].ToString()
                    };
                  
                    departments.Add(department);
                }
                reader.Close();
                return departments;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect department",exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            }
        }


        public int Insert(Department aDepartment)
        {
            try
            {
                string query = "INSERT INTO t_Departments VALUES(@code,@name)";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@code", aDepartment.Code.ToUpper());
                CommandObj.Parameters.AddWithValue("@name", aDepartment.Name);
                ConnectionObj.Open();
                 return CommandObj.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                throw new Exception("Unable to save department", exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            }
        }

        public Department GetDepartmentByCode(string code)
        {
            try
            {
                string query = "SELECT * FROM t_Departments WHERE Code=@code";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@code", code);
                Department department = null;
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                if (reader.Read())
                {
                    department = new Department
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Code = reader["Code"].ToString()
                    };
                   
                }
                reader.Close();
                return department;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect department", exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            }
        }

        public Department GetDepartmentByName(string name)
        {
            try
            {
                string query = "SELECT * FROM t_Departments WHERE Name=@name";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@name", name);
                Department department = null;
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                if (reader.Read())
                {
                    department = new Department
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Code = reader["Code"].ToString()
                    };

                }
                reader.Close();
                return department;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect department", exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            }
        }
    }
}