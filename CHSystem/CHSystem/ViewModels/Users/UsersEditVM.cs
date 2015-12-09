using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CHSystem.ViewModels.Users
{
    public class UsersEditVM
    {
        public int ID { get; set; }

        [Required, StringLength(60, MinimumLength = 6, ErrorMessage = "Name not in range")]
        public string Username { get; set; }

        [Required, StringLength(60, MinimumLength = 6, ErrorMessage = "Name not in range")]
        public string FirstName { get; set; }

        [Required, StringLength(60, MinimumLength = 6, ErrorMessage = "Name not in range")]
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