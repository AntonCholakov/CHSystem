using CHSystem.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.ViewModels.Halls
{
    public class HallsListVM
    {
        public List<Hall> Halls { get; set; }

        public IPagedList HallsPagedList { get; set; }

        public string SortOrder { get; set; }

        public Dictionary<string, object> Props { get; set; }

        public string Search { get; set; }
    }
}