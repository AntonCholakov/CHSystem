using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHSystem.ViewModels.Halls
{
    public class HallsEditVM
    {
        public int ID { get; set; }

        [Required, StringLength(60, MinimumLength = 6, ErrorMessage = "Name not in range")]
        public string Name { get; set; }

        [Required]
        public int LocationID { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }
    }
}