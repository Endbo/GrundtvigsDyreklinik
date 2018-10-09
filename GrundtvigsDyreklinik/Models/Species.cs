using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrundtvigsDyreklinik.Models
{
    public class Species
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Du skal udfylde navenet på Dyreartet")]
        [Display(Name = "Dyreart")]
        public String Name { get; set; }
        public virtual ICollection<Race> Races { get; set; }
    }
}