using System;
using System.Collections.Generic;
using System.Linq;
using UniversityManagementSystem.Core.Gateway;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Core.BLL
{
    public class StudentManager
    {
        StudentGateway studentGateway = new StudentGateway();
        DepartmentGateway departmentGateway = new DepartmentGateway();
        public IEnumerable<Student> GetAll
        {
            get { return studentGateway.GetAll; }
        }

        public string GetLastAddedStudentRegistration(string searchKey)
        {
            return studentGateway.GetLastAddedStudentRegistration(searchKey);

        }

        public string Save(Student aStudent)
        {
            int counter;
            Department department = departmentGateway.GetAll().Single(depid => depid.Id == aStudent.DepartmentId);
            string searchKey = department.Code + "-" + aStudent.RegDate.Year + "-";
            string lastAddedRegistrationNo = GetLastAddedStudentRegistration(searchKey);
            if (lastAddedRegistrationNo == null)
            {
                aStudent.RegNo = searchKey + "001";

            }

            if (lastAddedRegistrationNo != null)
            {
                string tempId = lastAddedRegistrationNo.Substring((lastAddedRegistrationNo.Length - 3), 3);
                counter = Convert.ToInt32(tempId);
                string studentSl = (counter + 1).ToString();


                if (studentSl.Length == 1)
                {

                    aStudent.RegNo = searchKey + "00" + studentSl;

                }
                else if (studentSl.Count() == 2)
                {

                    aStudent.RegNo = searchKey + "0" + studentSl;
                }
                else
                {

                    aStudent.RegNo = searchKey + studentSl;
                }

            }
            var listOfEmailAddress = from student in GetAll
                                     select student.Email;
            string tempEmail = listOfEmailAddress.ToList().Find(email => email.Contains(aStudent.Email));

            if (tempEmail != null)
            {
                return "Email address must be unique";
            }
            if (IsEmailAddressValid(aStudent.Email))
            {
                if (studentGateway.Insert(aStudent) > 0)
                {
                    return "Saved Successfully!;Registration No:" + aStudent.RegNo + ";Name:" + aStudent.Name + ";Email:" + aStudent.Email + ";Contact Number:" + aStudent.Contact;
                }

                return "Failed to save";
            }
            return "Please! enter a valid email address";
        }

        private bool IsEmailAddressValid(string email)
        {
            if (email.Contains(".com") && ((email.Contains("@gmail")) || (email.Contains("@yahoo")) || (email.Contains("@live")) || (email.Contains("@outlook"))))
            {
                return true;
            }
            return false;

        }

        public StudentViewModel GetStudentInformationById(int id)
        {
            return studentGateway.GetStudentInformationById(id);
        }

        public string Save(EnrollStudentInCourse enrollStudentInCourse)
        {
            EnrollStudentInCourse enrollStudent =
                GetEnrollCourses.ToList()
                    .Find(
                        st =>
                            (st.StudentId == enrollStudentInCourse.StudentId &&
                            st.CourseId == enrollStudentInCourse.CourseId) && (st.Status));
            if (enrollStudent == null)
            {
                if (studentGateway.Insert(enrollStudentInCourse) > 0)
                {
                    return "Saved Successfully!";
                }
                return "Failed to save";
            }

            return "This course already taken by the student";
        }

        public IEnumerable<EnrollStudentInCourse> GetEnrollCourses
        {
            get { return studentGateway.GetEnrollCourses; }
        }

        public string Save(StudentResult studentResult)
        {
            StudentResult result =
                GetAllResult.ToList()
                    .Find(st => st.StudentId == studentResult.StudentId && st.CourseId == studentResult.CourseId);
            if (result == null)
            {
                if (studentGateway.Insert(studentResult) > 0)
                {
                    return "Saved sucessfull!";
                }
                return "Failed to save";

            }
            if (result.Status)
            {
                return "This course result already saved";
            }
            if (studentGateway.UpdateStudentResult(studentResult) > 0)
            {
                return "Saved sucessfull!";
            }

            return "This course result already saved";
        }

        //private bool IsResulExits(StudentResult studentResult)
        //{
        //    StudentResult result =
        //        GetAllResult.ToList()
        //            .Find(st => st.StudentId == studentResult.StudentId && st.CourseId == studentResult.CourseId);
        //    if (result != null)
        //    {
        //        bool st = result.Status;
        //        if (st)
        //        {
        //            return true;  
        //        }

        //    }
        //    return false;
        //}

        public IEnumerable<StudentResult> GetAllResult
        {
            get { return studentGateway.GetAllResult; }
        }

        public IEnumerable<StudentResultViewModel> GetStudentResultByStudentId(int id)
        {
            return studentGateway.GetStudentResultByStudentId(id);
        }


    }
}