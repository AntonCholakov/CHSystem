using CHSystem.Attributes;
using CHSystem.Models;
using System.Collections.Generic;

namespace CHSystem.ViewModels.Halls
{
    public class HallsListVM : ListVM
    {
        [CollectionProperty]
        public List<Hall> Halls { get; set; }
    }
}