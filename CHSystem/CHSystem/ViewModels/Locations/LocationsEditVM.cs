using System.ComponentModel.DataAnnotations;

namespace CHSystem.ViewModels.Locations
{
    public class LocationsEditVM : EditVM
    {
        [Required, StringLength(60, MinimumLength = 6, ErrorMessage = "Name not in range")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int RoomNumber { get; set; }
    }
}