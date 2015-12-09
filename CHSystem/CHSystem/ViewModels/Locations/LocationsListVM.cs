using CHSystem.Attributes;
using CHSystem.Models;
using System.Collections.Generic;

namespace CHSystem.ViewModels.Locations
{
    public class LocationsListVM : ListVM
    {
        [CollectionProperty]
        public List<Location> Locations { get; set; }
    }
}