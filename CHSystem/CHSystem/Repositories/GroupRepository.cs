using CHSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Repositories
{
    public class GroupRepository:BaseRepository<Group>
    {
        public GroupRepository():base (){ }
        public GroupRepository(UnitOfWork unitOfWork):base(unitOfWork){ }
    }
}