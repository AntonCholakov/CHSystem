using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CHSystem.ViewModels.Users
{
    public class UsersEditVM
    {
        public int ID { get; set; }

        [Required, StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be at least 3 symbols")]
        public string Username { get; set; }

        [Required, StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be at least 3 symbols")]
        public string Password { get; set; }
        
        [Required, StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be at least 3 symbols")]
        public string FirstName { get; set; }

        [Required, StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be at least 3 symbols")]
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