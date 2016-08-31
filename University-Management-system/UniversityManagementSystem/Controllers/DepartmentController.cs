using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UniversityManagementSystem.Core.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager=new DepartmentManager();
        //
        // GET: /Department/
        public ActionResult Index()
        {
            List<Department> departments = departmentManager.GetAll().ToList();
            return View(departments);
        }

       
        // GET: /Department/Create
        public ActionResult Save()
        {
            return View();
        }

        //
        // POST: /Department/Create
        [HttpPost]
        public ActionResult Save(Department aDepartment)
        {
            try
            {
                string message=departmentManager.Save(aDepartment);
                ViewBag.Mgs = message;
                return View();

                //return RedirectToAction("Index");
            }
            catch(Exception exception)
            {
                ViewBag.Mgs = exception.InnerException.Message;
               
                return View();
            }
        }

        
    }
}
