using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrundtvigsDyreklinik.Models
{
    public class Race
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Du skal udfylde navn på racen")]
        [Display(Name = "Race")]
        public String Name { get; set; }

        [Display(Name = "Dyreart")]
        public int SpeciesID { get; set; }
        public virtual Species Species { get; set; }
    }
}
