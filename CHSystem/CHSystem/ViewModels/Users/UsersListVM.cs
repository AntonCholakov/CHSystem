﻿using CHSystem.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.ViewModels.Users
{
    public class UsersListVM
    {
        public List<User> Users { get; set; }
        public IPagedList UsersPagedList { get; set; }

        public string SortOrder { get; set; }

        public Dictionary<string, object> Props { get; set; }

        public string Search { get; set; }
    }
}