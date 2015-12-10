using System.ComponentModel.DataAnnotations;

namespace CHSystem.ViewModels.Groups
{
    public class GroupsEditVM : EditVM
    {
        [Required, StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be at least 3 symbols")]
        public string Name { get; set; }
    }
}