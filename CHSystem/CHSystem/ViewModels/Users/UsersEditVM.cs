using CHSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CHSystem.ViewModels.Users
{
    public class UsersEditVM
    {
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public List<AssignedGroupsVM> Groups { get; set; }
    }
    public class AssignedGroupsVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
    }
}