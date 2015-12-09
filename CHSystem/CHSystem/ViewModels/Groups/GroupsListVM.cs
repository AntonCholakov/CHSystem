using CHSystem.Attributes;
using CHSystem.Models;
using System.Collections.Generic;

namespace CHSystem.ViewModels.Groups
{
    public class GroupsListVM : ListVM
    {
        [CollectionProperty]
        public List<Group> Groups { get; set; }
    }
}