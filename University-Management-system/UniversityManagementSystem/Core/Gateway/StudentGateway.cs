using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.Gateway
{
    public class StudentGateway : DBGateway
    {
        public IEnumerable<Student> GetAll
        {
            get
            {
                try
                {
                    string query = "SELECT * FROM t_Student";
                    CommandObj.CommandText = query;
                    List<Student> students = new List<Student>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            RegNo = reader["RegNo"].ToString(),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            Contact = reader["ContactNo"].ToString(),
                            RegDate = Convert.ToDateTime(reader["RegisterationDate"].ToString()),
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString())
                        };
                        students.Add(student);
                    }
                    reader.Close();
                    return students;
                }
                catch (Exception exception)
                {
                    throw new Exception("Could not collect Studens", exception);
                }
                finally
                {
                    CommandObj.Dispose();
                    ConnectionObj.Close();
                }
            }


        }

        public string GetLastAddedStudentRegistration(string searchKey)
        {
            string query = "SELECT * FROM t_Student st WHERE RegNo LIKE '%" + searchKey + "%' and Id=(select Max(Id) FROM t_Student st WHERE RegNo LIKE '%" + searchKey + "%' )";
            CommandObj.CommandText = query;
            ConnectionObj.Open();
            Student aStudent = null;
            string regNo = null;
            SqlDataReader reader = CommandObj.ExecuteReader();
            if (reader.Read())
            {
                aStudent = new Student
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    RegNo = reader["RegNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    Contact = reader["ContactNo"].ToString(),

                };
                regNo = aStudent.RegNo;
            }

            ConnectionObj.Close();
            CommandObj.Dispose();
            reader.Close();
            return regNo;
        }

        public int Insert(Student aStudent)
        {
            try
            {

                string query = "INSERT INTO t_Student VALUES(@RegNo,@Name,@Email,@ContactNo,@RegisterationDate,@Address,@DepartmentId)";
                CommandObj.CommandText = query;

                CommandObj.Parameters.Clear();

                CommandObj.Parameters.AddWithValue("@RegNo", aStudent.RegNo);
                CommandObj.Parameters.AddWithValue("@Name", aStudent.Name);
                CommandObj.Parameters.AddWithValue("@Email", aStudent.Email.ToLower());
                CommandObj.Parameters.AddWithValue("@ContactNo", aStudent.Contact);
                CommandObj.Parameters.AddWithValue("@RegisterationDate", aStudent.RegDate.ToShortDateString());
                CommandObj.Parameters.AddWithValue("@Address", aStudent.Address);
                CommandObj.Parameters.AddWithValue("@DepartmentId", aStudent.DepartmentId);
                ConnectionObj.Open();
                int rowAffected = CommandObj.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Could Not save", exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }
        }

        public StudentViewModel GetStudentInformationById(int id)
        {

            try
            {
                CommandObj.CommandText = "spGetStudentInformationById";
                CommandObj.CommandType = CommandType.StoredProcedure;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@Id", id);
                StudentViewModel student = null;
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                if (reader.Read())
                {
                    student = new StudentViewModel
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        RegNo = reader["RegNo"].ToString(),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        ContactNo = reader["ContactNo"].ToString(),
                        RegisterationDate = Convert.ToDateTime(reader["RegisterationDate"].ToString()),
                        Address = reader["Address"].ToString(),
                        Department = reader["Department"].ToString()
                    };
                }

                reader.Close();
                return student;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to get student information", exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }
        }

        public int Insert(EnrollStudentInCourse enrollStudent)
        {
            try
            {
                CommandObj.CommandText = "INSERT INTO t_StudentEnrollInCourse VALUES(@stId,@courseId,@enrollDate,@status)";
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@stId", enrollStudent.StudentId);
                CommandObj.Parameters.AddWithValue("@courseId", enrollStudent.CourseId);
                CommandObj.Parameters.AddWithValue("@enrollDate", enrollStudent.EnrollDate.ToShortDateString());
                CommandObj.Parameters.AddWithValue("@status", 1);
                ConnectionObj.Open();
                int rowAffected = CommandObj.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Could not save", exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }

        }

        public IEnumerable<EnrollStudentInCourse> GetEnrollCourses
        {
            get
            {
                try
                {
                    CommandObj.CommandText = "SELECT * FROM t_StudentEnrollInCourse";
                    List<EnrollStudentInCourse> enrollStudentInCourses = new List<EnrollStudentInCourse>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        EnrollStudentInCourse enrollStudentInCourse = new EnrollStudentInCourse
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            StudentId = Convert.ToInt32(reader["StudentId"].ToString()),
                            CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                            EnrollDate = Convert.ToDateTime(reader["EnrollDate"].ToString()),
                            Status = Convert.ToBoolean(reader["IsStudentActive"].ToString())

                        };
                        enrollStudentInCourses.Add(enrollStudentInCourse);
                    }
                    reader.Close();
                    return enrollStudentInCourses;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect enrolled courses", exception);
                }
                finally
                {
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                }
            }
        }

        public int Insert(StudentResult studentResult)
        {
            try
            {
                CommandObj.CommandText = "INSERT INTO t_StudentResult VALUES(@stId,@courseId,@grade,@isStudentActive)";
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@stId", studentResult.StudentId);
                CommandObj.Parameters.AddWithValue("@courseId", studentResult.CourseId);
                CommandObj.Parameters.AddWithValue("@grade", studentResult.Grade);
                CommandObj.Parameters.AddWithValue("@isStudentActive", 1);
                ConnectionObj.Open();
                int rowAffected = CommandObj.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Could not save", exception);
            }
            finally
            {
                ConnectionObj.Close();
                CommandObj.Dispose();
            }
        }

        public IEnumerable<StudentResult> GetAllResult
        {
            get
            {
                try
                {
                    CommandObj.CommandText = "SELECT * FROM t_StudentResult";
                    List<StudentResult> studentResults = new List<StudentResult>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        StudentResult studentResult = new StudentResult
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                            StudentId = Convert.ToInt32(reader["StudentId"].ToString()),
                            Grade = reader["Grade"].ToString(),
                            Status = (bool)reader["IsStudentActive"]
                        };
                        studentResults.Add(studentResult);
                    }
                    reader.Close();
                    return studentResults;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect student result", exception);
                }
                finally
                {
                    CommandObj.Dispose();
                    ConnectionObj.Close();
                }
            }
        }

        public IEnumerable<StudentResultViewModel> GetStudentResultByStudentId(int id)
        {
            try
            {
                List<StudentResultViewModel> studentResults = new List<StudentResultViewModel>();
                CommandObj.CommandText = "spGetStudentResult";
                CommandObj.CommandType = CommandType.StoredProcedure;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@studentId", id);
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                while (reader.Read())
                {
                    StudentResultViewModel studentResult = new StudentResultViewModel
                    {
                        StudentId = Convert.ToInt32(reader["StudentId"].ToString()),
                        Code = reader["Code"].ToString(),
                        Name = reader["Name"].ToString(),
                        Grade = reader["Grade"].ToString()
                    };
                    studentResults.Add(studentResult);
                }
                reader.Close();
                return studentResults;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to collect sudent result", exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }
        }

        public int Update(EnrollStudentInCourse enrollStudentInCourse)
        {
            ConnectionObj.Open();
            SqlTransaction sqlTransaction = ConnectionObj.BeginTransaction();
            try
            {


                CommandObj.Transaction = sqlTransaction;
                CommandObj.CommandText = "UPDATE t_StudentEnrollInCourse SET IsStudentActive=1 WHERE StudentId='" + enrollStudentInCourse.StudentId + "' AND CourseId='" + enrollStudentInCourse.CourseId + "'";
                int updateResult = CommandObj.ExecuteNonQuery();

                // int updateResult = UpdateStudentEnrolledCourses(enrollStudentInCourse);
                sqlTransaction.Commit();
                return updateResult;
            }
            catch (Exception exception)
            {
                sqlTransaction.Rollback();
                throw new Exception("Could not save", exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }
        }



        public int UpdateStudentResult(StudentResult studentResult)
        {
            CommandObj.CommandText = "UPDATE t_StudentResult SET IsStudentActive=1,Grade='" + studentResult.Grade + "' WHERE StudentId='" +
                                     studentResult.StudentId + "' AND CourseId='" + studentResult.CourseId + "'";
            ConnectionObj.Open();


            int i = CommandObj.ExecuteNonQuery();
            ConnectionObj.Close();
            return i;
        }
    }
}