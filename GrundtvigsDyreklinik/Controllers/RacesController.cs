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
    [Authorize(Roles = "Admin")]
    public class RacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Races
        public ActionResult Index()
        {
            var races = db.Races.Include(r => r.Species);
            return View(races.ToList());
        }

        // GET: Races/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            return View(race);
        }

        // GET: Races/Create
        public ActionResult Create()
        {
            ViewBag.SpeciesID = new SelectList(db.Species, "ID", "Name");
            return View();
        }

        // POST: Races/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,SpeciesID")] Race race)
        {
            if (ModelState.IsValid)
            {
                db.Races.Add(race);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpeciesID = new SelectList(db.Species, "ID", "Name", race.SpeciesID);
            return View(race);
        }

        // GET: Races/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpeciesID = new SelectList(db.Species, "ID", "Name", race.SpeciesID);
            return View(race);
        }

        // POST: Races/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,SpeciesID")] Race race)
        {
            if (ModelState.IsValid)
            {
                db.Entry(race).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpeciesID = new SelectList(db.Species, "ID", "Name", race.SpeciesID);
            return View(race);
        }

        // GET: Races/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            return View(race);
        }

        // POST: Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Race race = db.Races.Find(id);
            db.Races.Remove(race);
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
