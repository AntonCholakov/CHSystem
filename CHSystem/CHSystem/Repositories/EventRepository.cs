using CHSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Repositories
{
    public class EventRepository:BaseRepository<Event>
    {
        public EventRepository():base (){ }
        public EventRepository(UnitOfWork unitOfWork):base(unitOfWork){ }
    }
}