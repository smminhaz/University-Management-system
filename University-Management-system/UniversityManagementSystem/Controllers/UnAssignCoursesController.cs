using System.Web.Mvc;
using UniversityManagementSystem.Core.BLL;

namespace UniversityManagementSystem.Controllers
{
    public class UnAssignCoursesController : Controller
    {
        CourseManager courseManager=new CourseManager();
        //
        // GET: /UnAssignCourses/
        [HttpGet]
        public ActionResult UnAssign()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnAssign(int? id)
        {
            ViewBag.Message = courseManager.UnAssignCourses();
            return View();
        }
	}
}