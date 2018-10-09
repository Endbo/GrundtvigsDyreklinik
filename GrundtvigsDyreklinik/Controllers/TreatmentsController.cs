using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GrundtvigsDyreklinik.Models;

namespace GrundtvigsDyreklinik.Controllers
{
    public class TreatmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Treatments
        public ActionResult Index()
        {
            return View(db.Treatments.ToList());
        }

        // GET: Treatments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // GET: Treatments/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Treatments/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Duration,Comment")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {                
                db.Treatments.Add(treatment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(treatment);
        }

        // GET: Treatments/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Duration,Comment")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(treatment);
        }

        // GET: Treatments/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            db.Treatments.Remove(treatment);
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
