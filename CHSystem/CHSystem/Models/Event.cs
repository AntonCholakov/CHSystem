using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Models
{
    public class Event : BaseModel
    {
        public string Name { get; set; }
        public int HallID { get; set; } 
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual List<User> Users { get; set; }
    }
}