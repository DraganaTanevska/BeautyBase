using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyBaseFinal.Models
{
    public class Available
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Датум")]
        public DateTime Date { get; set; }
        [Display(Name = "Време")]
        [Required]
        [RegularExpression("^[0-2][0-9]:[0-5][0-9]$",ErrorMessage ="Внесете време во правилен формат! (пример: 13:00)")]
        public string selectedTime { get; set; }
        [Display(Name = "Забелешка")]
        public string Description { get; set; }
    }
}