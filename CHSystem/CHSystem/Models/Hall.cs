using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Models
{
    public class Hall:BaseEntity
    {
        public string Name { get; set; }
        public int LocationID { get; set; }

        public virtual Location Location { get; set; }
    }
}