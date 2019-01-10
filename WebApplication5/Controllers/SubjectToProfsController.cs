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
    public class SubjectToProfsController : Controller
    {
        private EnrollmentEntities db = new EnrollmentEntities();

        // GET: SubjectToProfs
        public ActionResult Index()
        {
            var subjectToProfs = db.SubjectToProfs.Include(s => s.Professor).Include(s => s.Subject1);
            return View(subjectToProfs.ToList());
        }

        // GET: SubjectToProfs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectToProf subjectToProf = db.SubjectToProfs.Find(id);
            if (subjectToProf == null)
            {
                return HttpNotFound();
            }
            return View(subjectToProf);
        }

        // GET: SubjectToProfs/Create
        public ActionResult Create()
        {
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "ProfessorName");
            ViewBag.SubjectID = new SelectList(db.Subject1, "SubjectID", "SubjectName");
            return View();
        }

        // POST: SubjectToProfs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,SubjectID,ProfessorID,rating")] SubjectToProf subjectToProf)
        {
            if (ModelState.IsValid)
            {
                db.SubjectToProfs.Add(subjectToProf);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "ProfessorName", subjectToProf.ProfessorID);
            ViewBag.SubjectID = new SelectList(db.Subject1, "SubjectID", "SubjectName", subjectToProf.SubjectID);
            return View(subjectToProf);
        }

        // GET: SubjectToProfs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectToProf subjectToProf = db.SubjectToProfs.Find(id);
            if (subjectToProf == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "ProfessorName", subjectToProf.ProfessorID);
            ViewBag.SubjectID = new SelectList(db.Subject1, "SubjectID", "SubjectName", subjectToProf.SubjectID);
            return View(subjectToProf);
        }

        // POST: SubjectToProfs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,SubjectID,ProfessorID,rating")] SubjectToProf subjectToProf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectToProf).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "ProfessorName", subjectToProf.ProfessorID);
            ViewBag.SubjectID = new SelectList(db.Subject1, "SubjectID", "SubjectName", subjectToProf.SubjectID);
            return View(subjectToProf);
        }

        // GET: SubjectToProfs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectToProf subjectToProf = db.SubjectToProfs.Find(id);
            if (subjectToProf == null)
            {
                return HttpNotFound();
            }
            return View(subjectToProf);
        }

        // POST: SubjectToProfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubjectToProf subjectToProf = db.SubjectToProfs.Find(id);
            db.SubjectToProfs.Remove(subjectToProf);
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
