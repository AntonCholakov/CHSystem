using CHSystem.Attributes;
using CHSystem.Models;
using System.Collections.Generic;

namespace CHSystem.ViewModels.Users
{
    public class UsersListVM : ListVM
    {
        [CollectionProperty]
        public List<User> Users { get; set; }
    }
}