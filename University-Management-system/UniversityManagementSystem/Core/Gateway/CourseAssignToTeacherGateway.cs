using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using UniversityManagementSystem.Core.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.Gateway
{
    public class CourseAssignToTeacherGateway:DBGateway
    {
        TeacherManager teacherManager=new TeacherManager();
        public int Insert(CourseAssignToTeacher courseAssign)
        {
            ConnectionObj.Open();
            SqlTransaction sqlTransaction = ConnectionObj.BeginTransaction();
            try
            {

                
                CommandObj.Transaction = sqlTransaction;
                CommandObj.CommandText = "INSERT INTO t_CourseAssignToTeacher VALUES(@deptId,@teacherId,@courseId,@status)";
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@deptId", courseAssign.DepartmentId);
                CommandObj.Parameters.AddWithValue("@teacherId", courseAssign.TeacherId);
                CommandObj.Parameters.AddWithValue("@courseId", courseAssign.CourseId);
                CommandObj.Parameters.AddWithValue("@status", 1);
                int rowAffected = CommandObj.ExecuteNonQuery();

                int updateResult = UpdateTeacher(courseAssign);
                sqlTransaction.Commit();
                return rowAffected;
            }
            catch (Exception exception)
            {
                sqlTransaction.Rollback();
                throw new Exception("Could not save",exception);
            }
            finally
            {
                CommandObj.Dispose();
                ConnectionObj.Close();
            }

        }

        

        private int UpdateTeacher(CourseAssignToTeacher courseAssign)
        {
            Teacher teacher = teacherManager.GetAll.ToList().Find(t => t.Id == courseAssign.TeacherId);

            double creditTakenbyTeacher =Convert.ToDouble(teacher.CreditTaken)+Convert.ToDouble(courseAssign.Credit);
            CommandObj.CommandText = "Update t_Teacher Set CreditTaken='" + creditTakenbyTeacher + "' WHERE Id='" +
                                     courseAssign.TeacherId + "'";
            return CommandObj.ExecuteNonQuery();
        }

        public IEnumerable<CourseAssignToTeacher> GetAll 
        {
            get
            {
                try
                {
                    CommandObj.CommandText = "SELECT * FROM t_CourseAssignToTeacher";
                    List<CourseAssignToTeacher> courseAssignToTeachers = new List<CourseAssignToTeacher>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        CourseAssignToTeacher assignToTeacher = new CourseAssignToTeacher
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            TeacherId = Convert.ToInt32(reader["TeacherId"].ToString()),
                            CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                            Status = Convert.ToBoolean(reader["IsActive"].ToString())

                        };
                        courseAssignToTeachers.Add(assignToTeacher);
                    }
                    reader.Close();
                    return courseAssignToTeachers;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to collect",exception);
                }
                finally
                {
                    CommandObj.Dispose();
                    ConnectionObj.Close();
                }
            } 
        }

        public int Update(CourseAssignToTeacher courseAssign)
        {
            ConnectionObj.Open();
            SqlTransaction sqlTransaction = ConnectionObj.BeginTransaction();
            try
            {


                CommandObj.Transaction = sqlTransaction;
                CommandObj.CommandText = "UPDATE t_CourseAssignToTeacher SET IsActive=1 WHERE TeacherId='" + courseAssign.TeacherId + "' AND CourseId='"+courseAssign.CourseId+"'";
                CommandObj.ExecuteNonQuery();

                int updateResult = UpdateTeacher(courseAssign);
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
    }
}