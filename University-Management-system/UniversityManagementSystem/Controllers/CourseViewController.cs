using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UniversityManagementSystem.Core.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class CourseViewController : Controller
    {
        DepartmentManager departmentManager=new DepartmentManager();
        CourseManager courseManager=new CourseManager();
        //
        // GET: /CourseView/
        public ActionResult ShowCourseStatics()
        {
            IEnumerable<Department> departments = departmentManager.GetAll();
            ViewBag.Departments = departments;
            IEnumerable<CourseViewModel> courseViewModels = courseManager.GetCourseViewModels;
            return View(courseViewModels);
        }

        public JsonResult GetCourseInformationByDepartmentId(int departmentId)
        {
            IEnumerable<CourseViewModel> courseViewModels = courseManager.GetCourseViewModels.ToList().FindAll(deptId=>deptId.DepartmentId==departmentId);
            return Json(courseViewModels, JsonRequestBehavior.AllowGet);


        }

      
    }
}
