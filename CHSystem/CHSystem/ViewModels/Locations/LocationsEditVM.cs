using System.ComponentModel.DataAnnotations;

namespace CHSystem.ViewModels.Locations
{
    public class LocationsEditVM : EditVM
    {
        [Required, StringLength(60, MinimumLength = 3, ErrorMessage = "Name must be at least 3 symbols")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int RoomNumber { get; set; }
    }
}