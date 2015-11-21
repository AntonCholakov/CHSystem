﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Models
{
    public class Event:BaseEntity
    {
        public string Name { get; set; }
        public int HallID { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual List<User> Users { get; set; }
    }
}