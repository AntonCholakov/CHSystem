﻿using CHSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Repositories
{
    public class LocationRepository:BaseRepository<Location>
    {
        public LocationRepository():base (){ }
        public LocationRepository(UnitOfWork unitOfWork):base(unitOfWork){ }
    }
}