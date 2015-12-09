using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Models
{
    public class Location : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }

        public virtual List<Hall> Halls { get; set; }
    }
}