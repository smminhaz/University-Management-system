using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UniversityManagementSystem.Core.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class EnrollStudentInCourseController : Controller
    {
        StudentManager studentManager=new StudentManager();
        CourseManager courseManager=new CourseManager();
        
        [HttpGet]
        public ActionResult Enroll()
        {
            IEnumerable<Student> students = studentManager.GetAll;
            IEnumerable<Course> courses = courseManager.GetAll;
            ViewBag.Students = students;
            ViewBag.Courses = courses;
            return View();
        }
        [HttpPost]
        public ActionResult Enroll(EnrollStudentInCourse enrollStudent)
        {
            string message;
            try
            {
                message = studentManager.Save(enrollStudent);
                ViewBag.Message = message;
                IEnumerable<Student> students = studentManager.GetAll;
                IEnumerable<Course> courses = courseManager.GetAll;
                ViewBag.Students = students;
                ViewBag.Courses = courses;

                return View();
            }
            catch (Exception exception)
            {

               message= exception.InnerException.Message;
                ViewBag.Message = message;
                return View();
            }
        }

        public JsonResult GetStudentById(int studentId)
        {
            StudentViewModel student = studentManager.GetStudentInformationById(studentId);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByStudentId(int studentId)
        {
            Student aStudent = studentManager.GetAll.ToList().Find(st => st.Id == studentId);
            IEnumerable<Course> courses = courseManager.GetAll.ToList().FindAll(d => d.DepartmentId == aStudent.DepartmentId);
            return Json(courses, JsonRequestBehavior.AllowGet);

        }

	}
}