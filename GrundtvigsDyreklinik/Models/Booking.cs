using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace GrundtvigsDyreklinik.Models
{
    public class Booking
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Du skal udfylde start tidspunktet")]
        [Display(Name = "Starttidspunkt")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Slutttidspunkt")]
        public DateTime EndTime { get; set; }

        public string ThemColor { get; set; }

        [Display(Name = "Kæledyr")]
        public int PetID { get; set; }
        public virtual Pet pet { get; set; }

        [Display(Name = "Behandling")]
        public int TreatmentID { get; set; }
        public virtual Treatment Treatment { get; set; }

        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}