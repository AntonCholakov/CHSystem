﻿using CHSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHSystem.Repositories
{
    public class HallRepository:BaseRepository<Hall>
    { 
        public HallRepository():base (){ }
        public HallRepository(UnitOfWork unitOfWork):base(unitOfWork){ }
    }
}