using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GrundtvigsDyreklinik.Models;
using Microsoft.AspNet.Identity;

namespace GrundtvigsDyreklinik.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        //GET Json data for fullcalendar start...
        public JsonResult GetBookings()
        {
           var us = User.Identity.GetUserId(); 
           var events = db.Bookings.ToList();

            if(User.IsInRole("Admin"))
            {
                var eventListAd = from e in events
                                select new
                                {
                                    bookingID = e.ID,
                                    treatmentName = e.Treatment.Name,
                                    petName = e.pet.Name,
                                    start = e.StartTime.ToString("s"),
                                    end = e.EndTime.ToString("s"),
                                    color = e.ThemColor,
                                    userName = e.ApplicationUser.UserName
                                };                
                var rowsAD = eventListAd.ToArray();
                return Json(rowsAD, JsonRequestBehavior.AllowGet);

            }

           var eventList = from e in events                           
                            select new
                            {
                                bookingID = e.ID,
                                treatmentName = e.Treatment.Name,
                                petName = e.pet.Name,
                                start = e.StartTime.ToString("s"),
                                end = e.EndTime.ToString("s"),
                                color = e.ApplicationUserID==us? "green": "red",
                                userName = e.ApplicationUser.UserName
                            };


            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        //GET Json data for fullcalendar end...

        // Save And Update data start....
        [HttpPost]
        public JsonResult SaveAndEditBookings(Booking booking)
        {
            var status = false;
            var user = User.Identity.GetUserId();
            var ends= 0.0;
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            var bookingData = db.Bookings.Where(a => a.ID == booking.ID && a.ApplicationUserID == user).FirstOrDefault();
            var trets = db.Treatments.Where(a=> a.ID == booking.TreatmentID).FirstOrDefault();
            var booked = db.Bookings.ToList().Where(i => i.StartTime.Date == booking.StartTime.Date);
            bool boolBooked = true;

            if (trets.Duration != null)
            {
                ends = trets.Duration.Hour;
            }
            var endsTime = booking.StartTime.AddHours(ends);

            foreach (var i in booked)
            {

                if (booking.StartTime <= i.EndTime && endsTime >= i.StartTime)
                {
                    boolBooked = false;
                }
            }
       
            if(boolBooked)
            {
                if (bookingData != null)
                {
                    //bookingData = booking;
                    bookingData.ApplicationUserID = user;
                    bookingData.StartTime = booking.StartTime;
                    bookingData.EndTime = booking.EndTime.AddHours(ends);
                    bookingData.PetID = booking.PetID;
                    bookingData.TreatmentID = booking.TreatmentID;
                    db.Entry(bookingData).State = EntityState.Modified;
                    db.SaveChanges();
                    status = true;
                }
                //save data            
                else
                {
                    booking.EndTime = booking.EndTime.AddHours(ends);
                    booking.ApplicationUserID = user;
                    booking.ThemColor = color;
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    status = true;

                }
            }
            //update database           

            return new JsonResult { Data = new { status = status } };
        }
        //Save And Update data start....

        // deleteFunction 
        [HttpPost]
        public JsonResult DeleteBooking(int eventID)
        {
            var status = false;
           
                //deleteDatabase
                var booking = db.Bookings.Where(a => a.ID == eventID).FirstOrDefault();

                if (booking != null)
                {
                    db.Bookings.Remove(booking);
                    db.SaveChanges();
                    status = true;
                }
            
            return new JsonResult { Data = new { status = status } };
        }
      
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var profile = db.Users.Where(i => i.Id == userId).FirstOrDefault();
            Booking book = new Booking();

            if (profile!=null)
            {
                book.ApplicationUser = profile;

                if(book.ApplicationUser.ProfileImage ==null || string.IsNullOrEmpty(book.ApplicationUser.ProfileImage.ToString()))
                    {
                    book.ApplicationUser.ProfileImage = "";
                    }
            }

            
            ViewBag.PetID = new SelectList(db.Pets.Where(i=>i.ApplicationUserID == userId), "ID", "Name");
            ViewBag.TreatmentID = new SelectList(db.Treatments, "ID", "Name");

            return View(book);
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
