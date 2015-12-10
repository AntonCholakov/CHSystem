using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CHSystem.ViewModels.Halls
{
    public class HallsEditVM : EditVM
    {
        [Required, StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be at least 3 symbols")]
        public string Name { get; set; }

        [Required]
        public int LocationID { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }
    }
}