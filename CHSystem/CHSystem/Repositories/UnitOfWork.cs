using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CHSystem.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private CHSystemContext context = new CHSystemContext();

        private DbContextTransaction trans = null;

        public DbContext Context { get; private set; }

        public UnitOfWork()
        {
            this.trans = context.Database.BeginTransaction();
            this.Context = context;
        }

        public void Commit()
        {
            if (this.trans != null)
            {
                this.trans.Commit();
                this.trans = null;
            }
        }

        public void RollBack()
        {
            if (this.trans != null)
            {
                this.trans.Rollback();
                this.trans = null;
            }
        }

        public void Dispose()
        {
            Commit();
            context.Dispose();
        }
    }
}