using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyBaseFinal.Models
{
    public class Unavailable
    {

        [Key]
        public int Id { get; set; }
        [Display(Name = "Месец")]
        public DateTime Date { get; set; }
        [Display(Name = "Време")]
        public string selectedTime { get; set; }

        [Display(Name = "Корисник")]
        public string UserName { get; set; }

        [Display(Name = "Време на закажување")]
        public string ActionTime { get; set; }

        [Display(Name = "Забелешка")]
        public string Description { get; set; }
    }
}