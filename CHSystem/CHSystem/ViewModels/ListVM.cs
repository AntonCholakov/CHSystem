using CHSystem.ViewModels.Pager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.ViewModels
{
    public class ListVM
    {
        public PagingVM Pager { get; set; }

        public string SortOrder { get; set; }

        public Dictionary<string, object> Props { get; set; }

        public string Search { get; set; }
    }
}