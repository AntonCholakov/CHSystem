using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Models
{
    public class Group : BaseModel
    {
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}