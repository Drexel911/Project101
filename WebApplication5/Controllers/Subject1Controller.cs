using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class Subject1Controller : Controller
    {
        private EnrollmentEntities db = new EnrollmentEntities();

        // GET: Subject1
        public ActionResult Index(string StudentName)
        {
            if (Session["studentname"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.studentname = StudentName;
                return View(db.Subject1.ToList());
            }
            
        }

        // GET: Subject1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject1 subject1 = db.Subject1.Find(id);
            if (subject1 == null)
            {
                return HttpNotFound();
            }


            var Result = from b in db.Professors
                         select new
                         {
                             b.ProfessorID,
                             b.ProfessorName,
                             Checked = ((from ab in db.SubjectToProfs
                                         where (ab.SubjectID == id) & (ab.ProfessorID == b.ProfessorID)
                                         select ab).Count() > 0)
                         };
            var MyViewModel = new ProfessorViewModel();
            MyViewModel.SubjectID = id.Value;
            MyViewModel.SubjectName = subject1.SubjectName;

            var MyCheckBoxList = new List<CheckBoxViewModel>();

            foreach (var item in Result)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { id = item.ProfessorID, Name = item.ProfessorName, Checked = item.Checked });
            }

            MyViewModel.Professor  = MyCheckBoxList;






            return View(MyViewModel);
        }

        // GET: Subject1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectID,SubjectName")] Subject1 subject1)
        {
            if (ModelState.IsValid)
            {
                db.Subject1.Add(subject1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subject1);
        }

        // GET: Subject1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject1 subject1 = db.Subject1.Find(id);
            if (subject1 == null)
            {
                return HttpNotFound();
            }

                var Result = from b in db.Professors
                             select new
                             {
                                 b.ProfessorID,
                                 b.ProfessorName,
                                 Checked = ((from ab in db.SubjectToProfs
                                            where (ab.SubjectID == id) & (ab.ProfessorID == b.ProfessorID)
                                            select ab).Count() > 0)
                             };
                var MyViewModel = new ProfessorViewModel();
                MyViewModel.SubjectID = id.Value;
                MyViewModel.SubjectName = subject1.SubjectName;

                var MyCheckBoxList = new List<CheckBoxViewModel>();

                foreach( var item in Result)
                {
                    MyCheckBoxList.Add(new CheckBoxViewModel{ id = item.ProfessorID, Name = item.ProfessorName, Checked = item.Checked });
                }

                MyViewModel.Professor = MyCheckBoxList; 






            return View(MyViewModel);
        }

        // POST: Subject1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfessorViewModel subject)
        {
            if(ModelState.IsValid)
            {
                var MySubject = db.Subject1.Find(subject.SubjectID);

                MySubject.SubjectName = subject.SubjectName;

                foreach (var item in db.SubjectToProfs)
                {
                    if (item.SubjectID == subject.SubjectID)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item in subject.Professor)
                {
                    if (item.Checked)
                    {
                        db.SubjectToProfs.Add(new SubjectToProf() { SubjectID = subject.SubjectID, ProfessorID = item.id });
                    }
                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: Subject1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject1 subject1 = db.Subject1.Find(id);
            if (subject1 == null)
            {
                return HttpNotFound();
            }
            return View(subject1);
        }

        // POST: Subject1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject1 subject1 = db.Subject1.Find(id);
            db.Subject1.Remove(subject1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
