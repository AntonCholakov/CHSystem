using System.ComponentModel.DataAnnotations;

namespace CHSystem.ViewModels.Groups
{
    public class GroupsEditVM : EditVM
    {
        [Required, StringLength(60, MinimumLength = 6, ErrorMessage = "Name not in range")]
        public string Name { get; set; }
    }
}