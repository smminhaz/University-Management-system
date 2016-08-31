using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.Gateway
{
    public class TeacherGateway:DBGateway
    {
        public IEnumerable<Teacher> GetAll
        {
            get
            {
                try
                {
                    string query = "SELECT * FROM t_Teacher";
                    CommandObj.CommandText = query;
                    List<Teacher> teachers = new List<Teacher>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        Teacher teacher = new Teacher
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString(),
                            Email = reader["Email"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            DesignationId = Convert.ToInt32(reader["DesignationId"].ToString()),
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            CreditTobeTaken = Convert.ToDouble(reader["CreditToBeTaken"].ToString()),
                            CreditTaken = Convert.ToDouble(reader["CreditTaken"].ToString())
                            

                        };
                        teachers.Add(teacher);
                    }
                    reader.Close();
                    return teachers;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect Teachers", exception);
                }
                finally
                {
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                } 
            }
        }

        public Teacher GetTeacherByEmailAddress(string email)
        {
            try
            {
                string query = "SELECT * FROM t_Teacher WHERE Email=@email";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@email", email);
                Teacher teacher = null;
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                if(reader.Read())
                {
                     teacher = new Teacher
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        Email = reader["Email"].ToString(),
                        Contact = reader["Contact"].ToString(),
                        DesignationId = Convert.ToInt32(reader["DesignationId"].ToString()),
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                        CreditTobeTaken = Convert.ToDouble(reader["CreditToBeTaken"].ToString())

                    };
                    
                }
                reader.Close();
                return teacher;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to collect Teachers", exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            } 
        }

        public int Insert(Teacher teacher)
        {
            try
            {
                CommandObj.CommandText = "spAddTeacher";
                CommandObj.CommandType = CommandType.StoredProcedure;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@Name", teacher.Name);
                CommandObj.Parameters.AddWithValue("@Address", teacher.Address);
                CommandObj.Parameters.AddWithValue("@Email", teacher.Email.ToLower());
                CommandObj.Parameters.AddWithValue("@Contact", teacher.Contact);
                CommandObj.Parameters.AddWithValue("@DesignationId", teacher.DesignationId);
                CommandObj.Parameters.AddWithValue("@DepartmentId", teacher.DepartmentId);
                CommandObj.Parameters.AddWithValue("@CreditTobeTaken", teacher.CreditTobeTaken);
                CommandObj.Parameters.AddWithValue("@RemainingCredit", 0);
                ConnectionObj.Open();
                return CommandObj.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                throw new Exception("Could Not save teacher",exception);
            }

            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            }
        }



        public int UpdateTeacherInformation()
        {
            ConnectionObj.Open();
            CommandObj.CommandText = "UPDATE t_Teacher SET CreditTaken=0";
            int i= CommandObj.ExecuteNonQuery();
            ConnectionObj.Close();
            return i;
        }
    }
}