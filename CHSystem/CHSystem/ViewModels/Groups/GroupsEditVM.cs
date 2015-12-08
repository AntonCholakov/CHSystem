using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CHSystem.ViewModels.Groups
{
    public class GroupsEditVM
    {
        public int ID { get; set; }
        
        [Required, StringLength(60, MinimumLength =6,ErrorMessage ="Name not in range")]
        public string Name { get; set; }
    }
}