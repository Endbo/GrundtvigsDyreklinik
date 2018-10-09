using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrundtvigsDyreklinik.Models
{
    public class Pet
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Du skal udfylde navn")]
        [Display(Name = "Navn")]
        public String Name { get; set; }
      
        //[Required(ErrorMessage = "You must fill in the date of birth of your pet")]
        //[Range(typeof(DateTime), "1/2/1980", "1/2/2050", ErrorMessage = "Det skal være mellem 1980 og 2050")]
        [Display(Name = "Fødselsdato")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "You must fill in a name.")]
        [Display(Name = "Køn")]
        public String Sex { get; set; }

        public virtual Species Species { get; set; }
        public int SpeciesID { get; set; }

        public virtual Race Race { get; set; }
        public int? RaceID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserID { get; set; }

    }
}