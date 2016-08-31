using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UniversityManagementSystem.Core.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class ClassRoomController : Controller
    {
        DepartmentManager departmentManager=new DepartmentManager();
        CourseManager courseManager=new CourseManager();
        RoomManager roomManager=new RoomManager();
        DayManager dayManager=new DayManager();
        ClassRoomManager classRoomManager=new ClassRoomManager();
        
        private List<TempClassSchedule> classRooms;
        //
        // GET: /ClassRoom/
        public ActionResult Index()
        {
            ViewBag.Departments = departmentManager.GetAll();
            classRooms = classRoomManager.GetAllClassSchedules;
            ViewBag.ClassSchedule = classRooms;
            return View();
        }

        
        // GET: /ClassRoom/Create
        public ActionResult Save()
        {
            List<Day> days = dayManager.GetAllDays.ToList();
            ViewBag.Days = days;
            List<Room> rooms = roomManager.GetAllRooms.ToList();
            ViewBag.Rooms = rooms;
            ViewBag.Departments = departmentManager.GetAll();
            ViewBag.Courses = courseManager.GetAll;
            return View();
        }

        //
        // POST: /ClassRoom/Create
        [HttpPost]
        public ActionResult Save(ClassRoom classRoom)
        {
            try
            {
 
                string message = classRoomManager.Save(classRoom);
                
                ViewBag.Message = message;
                List<Day> days = dayManager.GetAllDays.ToList();
                ViewBag.Days = days;
                List<Room> rooms = roomManager.GetAllRooms.ToList();
                ViewBag.Rooms = rooms;
                var rr= departmentManager.GetAll();
                ViewBag.Departments = rr;
                ViewBag.Courses = courseManager.GetAll;
                return View();
            }
            catch
            {
                return View();
            }
        }
        public JsonResult GetClassScheduleByDepartment(int departmentId)
        {
            var courses = courseManager.GetCourseByDepartmentId(departmentId);

            List<object> clsSches = new List<object>();

            foreach (var course in courses)
            {
                var scheduleInfo = classRoomManager.GetAllClassSchedulesByDeparmentId(departmentId,course.Id);
                if (scheduleInfo=="")
                {
                    scheduleInfo = "Not sheduled yet";
                }
               

                var clsSch = new
                {
                    CourseCode = course.Code,
                    CourseName = course.Name,
                    ScheduleInfo = scheduleInfo
                };
                clsSches.Add(clsSch);
            }
            return Json(clsSches, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            IEnumerable<Course> courses = courseManager.GetAll;
            var courseList = courses.ToList().FindAll(c => c.DepartmentId == departmentId);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UnAllocateClassRoom()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnAllocateClassRoom(int? id)
        {
            ViewBag.Message = classRoomManager.UnAllocateClassRoom();
            return View();
        }
    }
}
