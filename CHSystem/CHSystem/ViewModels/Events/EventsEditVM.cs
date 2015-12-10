using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CHSystem.ViewModels.Events
{
    public class EventsEditVM : EditVM
    {
        [Required, StringLength(60, MinimumLength = 6, ErrorMessage = "Name not in range")]
        public string Name { get; set; }

        [Required]
        public int HallID { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public IEnumerable<SelectListItem> Halls { get; set; }

        public List<AssignedUsersVM> Users { get; set; }
    }

    public class AssignedUsersVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
    }
}