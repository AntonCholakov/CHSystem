using CHSystem.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.ViewModels.Groups
{
    public class GroupsListVM
    {
        public List<Group> Groups { get; set; }

        public IPagedList GroupsPagedList { get; set; }

        public string SortOrder { get; set; }

        public Dictionary<string, object> Props { get; set; }

        public string Search { get; set; }
    }
}