using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UniversityManagementSystem.Core.BLL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private string message;
        TeacherManager teacherManager=new TeacherManager();
        DesignationManager designationManager=new DesignationManager();
        DepartmentManager departmentManager=new DepartmentManager();
        //
        // GET: /Teacher/
        public ActionResult Index()
        {
            List<Teacher> teachers = teacherManager.GetAll.ToList();   
            return View(teachers);
        }

        //
        // GET: /Teacher/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Teacher/Create
        public ActionResult Save()
        {
            IEnumerable<dynamic> desinationList = designationManager.GetAll;
            IEnumerable<Department> departments = departmentManager.GetAll();
            ViewBag.Designations = desinationList;
            ViewBag.Departments = departments;
            return View();
        }

        //
        // POST: /Teacher/Create
        [HttpPost]
        public ActionResult Save(Teacher teacher)
        {
            try
            {
                message = teacherManager.Save(teacher);
                IEnumerable<Designation> desinationList = designationManager.GetAll;
                IEnumerable<Department> departments = departmentManager.GetAll();
                ViewBag.Designations = desinationList;
                ViewBag.Departments = departments;
                ViewBag.Message = message;
                return View();
                //return RedirectToAction("Index");
            }
            catch(Exception exception)
            {
                message = exception.Message;
                if (exception.InnerException != null)
                {
                    message += "<br/>System Error:" + exception.InnerException.Message;

                }
                ViewBag.Message = message;
                return View();
            }
        }

        //
        // GET: /Teacher/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Teacher/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Teacher/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Teacher/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
