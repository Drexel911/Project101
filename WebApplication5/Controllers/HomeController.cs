using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string StudentName)
        {

            if (Session["studentname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.studentname = StudentName;
                return View();
            }
        }

        public ActionResult About(string StudentName)
        {
            if (Session["studentname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.studentname = StudentName;
                return View();
            }
        }

        public ActionResult Contact(string StudentName)
        {
            if (Session["studentname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.studentname = StudentName;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            Student userModel = new Student();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Register( Student userModel)
        {
            using (EnrollmentEntities db = new EnrollmentEntities())
            {
                db.Students.Add(userModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Regeistration sucessful";
            return View("Register", new Student());
        }

        public ActionResult Login()
        {
            if (Session["studentname"] != null)
            {
                return RedirectToAction("Index", "Home", new { studentname = Session["studentname"].ToString() });
            }
            else
            {
                return View();
            }
        
        }

        [HttpPost]

        public ActionResult Login( Student student)
        {
            EnrollmentEntities db = new EnrollmentEntities();

            var studentLogin = db.Students.FirstOrDefault(x => x.StudentName == student.StudentName && x.Password1 == student.Password1);

            if(studentLogin != null)
            {
                ViewBag.message = "loggedin";
                ViewBag.triedOnce = "yes";

                Session["studentname"] = student.StudentName;

                return RedirectToAction("index", "Home", new { StudentName = student.StudentName });
            }
            else
            {
                ViewBag.triedOnce = "yes";
                return View();
            }
        }



    }
}