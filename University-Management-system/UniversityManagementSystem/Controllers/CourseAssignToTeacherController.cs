using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UniversityManagementSystem.Core.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class CourseAssignToTeacherController : Controller
    {
        CourseManager courseManger = new CourseManager();
        DepartmentManager departmentManager = new DepartmentManager();
        TeacherManager teacherManager = new TeacherManager();
        CourseAssignToTeacherManager courseAssignToTeacherManager=new CourseAssignToTeacherManager();
        public ActionResult AssignCourseToTeacher()
        {
            IEnumerable<Course> courses = courseManger.GetAll;
            IEnumerable<Department> departments = departmentManager.GetAll();
            IEnumerable<Teacher> teachers = teacherManager.GetAll;
            ViewBag.Courses = courses;
            ViewBag.Departments = departments;
            ViewBag.Teachers = teachers;
            return View();
        }
        [HttpPost]
        public ActionResult AssignCourseToTeacher(CourseAssignToTeacher courseAssign)
        {
            try
            {
                ViewBag.Message = courseAssignToTeacherManager.Save(courseAssign);
                IEnumerable<Course> courses = courseManger.GetAll;
                IEnumerable<Department> departments = departmentManager.GetAll();
                IEnumerable<Teacher> teachers = teacherManager.GetAll;
                ViewBag.Courses = courses;
                ViewBag.Departments = departments;
                ViewBag.Teachers = teachers;
                return View();
            }
            catch (Exception exception)
            {

                ViewBag.Message = exception.InnerException.Message;
                return View();
            }
           

        }

        public JsonResult GetTeachersByDepartmentId(int departmentId)
        {

            IEnumerable<Teacher> teachers = teacherManager.GetAll;
            var teacherList = teachers.ToList().FindAll(t => t.DepartmentId == departmentId);
            return Json(teacherList, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            IEnumerable<Course> courses = courseManger.GetAll;
            var courseList = courses.ToList().FindAll(c => c.DepartmentId == departmentId);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherById(int teacherId)
        {
            IEnumerable<Teacher> teachers = teacherManager.GetAll;
            Teacher aTeacher = teachers.ToList().Find(t => t.Id == teacherId);
            return Json(aTeacher, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseById(int courseId)
        {
            IEnumerable<Course> courses = courseManger.GetAll;
            Course aCourse = courses.ToList().Find(c => c.Id == courseId);
            return Json(aCourse, JsonRequestBehavior.AllowGet);
        }
	}
}