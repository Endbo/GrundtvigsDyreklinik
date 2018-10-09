using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrundtvigsDyreklinik.Models
{
    public class Treatment
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Du skal udfylde navnet på behandligen")]
        [Display(Name = "Navn")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Du skal udfylde navn Prisen")]
        [Display(Name = "Pris")]
        private int price { get; set; }

        [Display(Name = "Kommentar")]
        public string Comment { get; set; }


        [Display(Name = "Timer")]
        [NotMapped]
        public int Hour { get; set; }

        [Display(Name = "Minutter")]
        [NotMapped]
        public int Minute { get; set; }

        [Display(Name = "Varighed")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime Duration { get; set; }

    }
}