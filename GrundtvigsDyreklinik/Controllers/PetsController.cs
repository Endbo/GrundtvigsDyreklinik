using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GrundtvigsDyreklinik.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace GrundtvigsDyreklinik.Controllers
{
    [Authorize]
    public class PetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pets
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var pets = db.Pets.Include(p => p.Species);
                pets = db.Pets.Include(p => p.Race);
                return View(pets.ToList());
            }

            else
            {
                var userID = User.Identity.GetUserId();
                var pets = db.Pets.Where(p => p.ApplicationUserID == userID);
                pets.Include(p => p.Species);
                pets.Include(p => p.Race);


                return View(pets.ToList());
            }
        }


        // GET: Pets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }


        // GET: Pets/Create
        public ActionResult Create()
        {
            ViewBag.SpeciesID = new SelectList(db.Species, "ID", "Name");
            return View();
        }

        public PartialViewResult FindRaces(int? speciesID)
        {
            ViewBag.RaceID = new SelectList(db.Races.Where(x => x.Species.ID == speciesID), "ID", "Name");

            return PartialView("FindRaces");
        }


        // POST: Pets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Pets.Add(pet);
                pet.ApplicationUserID = User.Identity.GetUserId();

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpeciesID = new SelectList(db.Species, "ID", "Name", pet.SpeciesID);
            ViewBag.RaceID = new SelectList(db.Races, "ID", "Name", pet.RaceID);

            return View(pet);
        }


        // GET: Pets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpeciesID = new SelectList(db.Species, "ID", "Name", pet.SpeciesID);

            return View(pet);
        }


        // POST: Pets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pet).State = EntityState.Modified;
                pet.ApplicationUserID = User.Identity.GetUserId();

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpeciesID = new SelectList(db.Species, "ID", "Name", pet.SpeciesID);
            ViewBag.RaceID = new SelectList(db.Races, "ID", "Name", pet.RaceID);

            return View(pet);
        }

        // GET: Pets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pet pet = db.Pets.Find(id);
            db.Pets.Remove(pet);
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
