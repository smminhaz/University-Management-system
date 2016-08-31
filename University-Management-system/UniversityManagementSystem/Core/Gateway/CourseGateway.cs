using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.Gateway
{
    public class CourseGateway:DBGateway
    {
        TeacherGateway teacherGateway=new TeacherGateway();
        public IEnumerable<Course> GetAll
        {
            get
            {
                try
                {
                    string query = "SELECT * FROM t_Course";
                    CommandObj.CommandText = query;
                    List<Course> courses = new List<Course>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Course course = new Course
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            Code = reader["Code"].ToString(),
                            Credit = Convert.ToDouble(reader["Credit"].ToString()),
                            Description = reader["Descirption"].ToString(),
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())
                            
                        };
                        courses.Add(course);
                    }
                    reader.Close();
                    return courses;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect courses", exception);
                }
                finally
                {
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                } 
            }
         }

        public int Insert(Course aCourse)
        {
            try
            {
                CommandObj.CommandText = "spAddCourse";
                CommandObj.CommandType = CommandType.StoredProcedure;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@Code", aCourse.Code.ToUpper());
                CommandObj.Parameters.AddWithValue("@Name", aCourse.Name);
                CommandObj.Parameters.AddWithValue("@Credit", aCourse.Credit);
                CommandObj.Parameters.AddWithValue("@Description", aCourse.Description);
                CommandObj.Parameters.AddWithValue("@DepartmentId", aCourse.DepartmentId);
                CommandObj.Parameters.AddWithValue("@SemesterId", aCourse.SemesterId);
                ConnectionObj.Open();
                return CommandObj.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to Seve course", exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            } 

        }

        public Course GetCourseByName(string name)
        {
            try
            {
                string query = "SELECT * FROM t_Course WHERE Name=@name";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@name", name);
                ConnectionObj.Open();
                Course course = null;
                SqlDataReader reader = CommandObj.ExecuteReader();
                if(reader.Read())
                {
                  course = new Course
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Code = reader["Code"].ToString(),
                        Credit = Convert.ToDouble(reader["Credit"].ToString()),
                        Description = reader["Descirption"].ToString(),
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                        SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())

                    };
                    
                }
                reader.Close();
                return course;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to collect course", exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            } 
        }

        public Course GetCourseByCode(string code)
        {
            try
            {
                string query = "SELECT * FROM t_Course WHERE Code=@code";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@code", code);
                ConnectionObj.Open();
                Course course = null;
                SqlDataReader reader = CommandObj.ExecuteReader();
                if (reader.Read())
                {
                    course = new Course
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Code = reader["Code"].ToString(),
                        Credit = Convert.ToDouble(reader["Credit"].ToString()),
                        Description = reader["Descirption"].ToString(),
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                        SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())

                    };

                }
                reader.Close();
                return course;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to collect course", exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            } 
        }

        public IEnumerable<CourseViewModel> GetCourseViewModels
        {
            get
            {
                try
                {
                    string query ="spGetCourseInformation";
                    CommandObj.CommandType = CommandType.StoredProcedure;
                    CommandObj.CommandText = query;
                    List<CourseViewModel> courseViewModels = new List<CourseViewModel>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        CourseViewModel course = new CourseViewModel
                        {
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            Name = reader["Name"].ToString(),
                            Code = reader["Code"].ToString(),
                            Semester = reader["Semester"].ToString(),
                            Teacher = reader["Teacher"].ToString()
                           
                           

                        };
                        courseViewModels.Add(course);
                    }
                    reader.Close();
                    return courseViewModels;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect course statics", exception);
                }
                finally
                {
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                }
            }
        }

        public IEnumerable<Course> GetCoursesTakeByaStudentByStudentId(int id)
        {
            try
            {
                List<Course> courses = new List<Course>();
                CommandObj.CommandText = "spGetCoursesTakenByaStudent";
                CommandObj.CommandType = CommandType.StoredProcedure;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@StudentId", id);
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                while (reader.Read())
                {
                    Course aCourse = new Course
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Code = reader["Code"].ToString(),
                        Credit = Convert.ToDouble(reader["Credit"].ToString()),
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                        Description = reader["Descirption"].ToString(),
                        SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())
                    };
                    courses.Add(aCourse);
                }
                reader.Close();
                return courses;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to collect Courses",exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            }
        }

        public int UnAssignCourse()
        {
            ConnectionObj.Open();
            SqlTransaction sqlTransaction = ConnectionObj.BeginTransaction();
            try
            {
                CommandObj.CommandText = "UPDATE t_CourseAssignToTeacher SET IsActive=0";
                CommandObj.Transaction = sqlTransaction;
                CommandObj.ExecuteNonQuery();
                teacherGateway.UpdateTeacherInformation();
                int a = ResetStudentResult();
                int i = UnAssignStudentCourse();
                sqlTransaction.Commit();
                return i;
            }
            catch (Exception exception)
            {
                sqlTransaction.Rollback();
                throw new Exception("Failed to Unassign course",exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }
        }

        private int ResetStudentResult()
        {
            CommandObj.CommandText = "UPDATE t_StudentResult SET IsStudentActive=0";
            return CommandObj.ExecuteNonQuery();
        }

        

        private int UnAssignStudentCourse()
        {
            CommandObj.CommandText = "UPDATE t_StudentEnrollInCourse SET IsStudentActive=0";
            return CommandObj.ExecuteNonQuery();
        }

        public IEnumerable<Course> GetCourseByDepartmentId(int departmentId)
        {
           
                try
                {
                    string query = "SELECT * FROM t_Course WHERE DepartmentId='"+departmentId+"'";
                    CommandObj.CommandText = query;
                    List<Course> courses = new List<Course>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Course course = new Course
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            Code = reader["Code"].ToString(),
                            Credit = Convert.ToDouble(reader["Credit"].ToString()),
                            Description = reader["Descirption"].ToString(),
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())
                            
                        };
                        courses.Add(course);
                    }
                    reader.Close();
                    return courses;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect courses", exception);
                }
                finally
                {
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                } 
            }
        
    }
}