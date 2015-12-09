using CHSystem.Attributes;
using CHSystem.Models;
using System.Collections.Generic;

namespace CHSystem.ViewModels.Events
{
    public class EventsListVM : ListVM
    {
        [CollectionProperty]
        public List<Event> Events { get; set; }
    }
}