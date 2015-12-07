using CHSystem.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.ViewModels.Locations
{
    public class LocationsListVM
    {
        public List<Location> Locations { get; set; }
        public IPagedList LocationsPagedList { get; set; }
        public string SortOrder { get; set; }
        public Dictionary<string, object> Props { get; set; }
        public string  Search { get; set; }
    }
}