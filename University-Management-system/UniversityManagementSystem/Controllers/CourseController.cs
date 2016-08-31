using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UniversityManagementSystem.Core.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        DepartmentManager departmentManager=new DepartmentManager();
        SemesterManager semesterManager=new SemesterManager();
        CourseManager courseManager=new CourseManager();
        
        [HttpGet]
        public ActionResult Save()
        {


            List<Department> departments = departmentManager.GetAll().ToList();
             List<Semester> semesters = semesterManager.GetAll.ToList();
            ViewBag.Departments = departments;
            ViewBag.Semesters = semesters;
            return View();
        }

      
        // POST: /Course/Create
        [HttpPost]
        public ActionResult Save(Course aCourse)
        {
            try
            {

                
                    string message = courseManager.Save(aCourse);
                    ViewBag.Mgs = message;
                    List<Department> departments = departmentManager.GetAll().ToList();
                    List<Semester> semesters = semesterManager.GetAll.ToList();
                    ViewBag.Departments = departments;
                    ViewBag.Semesters = semesters;
                    return View(); 
                
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
    }
}
