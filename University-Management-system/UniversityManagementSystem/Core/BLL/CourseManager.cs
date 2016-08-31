using System.Collections.Generic;
using UniversityManagementSystem.Core.Gateway;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.BLL
{
    public class CourseManager
    {
        CourseGateway courseGateway=new CourseGateway();
        public IEnumerable<Course> GetAll
        {
            get { return courseGateway.GetAll; }
        } 
        public string Save(Course aCourse)
        {
            if (!(IsCorseCodeValid(aCourse)))
            {
                return "Course code must be at least 5 character of length";
            }
            if (IsCourseCodeExits(aCourse.Code))
            {
                return "Course code Already Exists ! Code must be unique";
            }
            if (IsCourseNameExits(aCourse.Name))
            {
                return "Course Name Already Exists ! Name must be unique";
            }
            if (courseGateway.Insert(aCourse) > 0)
            {
                return "Saved Successfully";
            }
                return "Failed to save";
        }

        private bool IsCourseNameExits(string name)
        {

            Course course = courseGateway.GetCourseByName(name);
            if (course != null)
            {
                return true;
            }

            return false;  
        }

        private bool IsCourseCodeExits(string code)
        {
            Course course = courseGateway.GetCourseByCode(code.ToUpper());
            
            if (course != null)
            {
                return true;
            }
            
            return false;
        }

        private bool IsCorseCodeValid(Course aCourse)
        {
            if (aCourse.Code.Length > 5)
            {
                return true;
            }
            return false;

        }

        public IEnumerable<CourseViewModel> GetCourseViewModels
        {
            get { return courseGateway.GetCourseViewModels; }
        }

        public IEnumerable<Course> GetCoursesTakenByaStudentById(int id)
        {
            return courseGateway.GetCoursesTakeByaStudentByStudentId(id);
        } 
         public string UnAssignCourses()
        {
             if (courseGateway.UnAssignCourse() > 0)
             {
                 return "Unassign Courses Successfully!";
             }
             return "Failed to unassign";
        }

         public IEnumerable<Course> GetCourseByDepartmentId(int departmentId)
         {
             return courseGateway.GetCourseByDepartmentId(departmentId);
         }
    }
}