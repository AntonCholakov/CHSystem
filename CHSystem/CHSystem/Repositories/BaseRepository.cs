using CHSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CHSystem.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected DbContext Context { set; get; }
        private DbSet<T> DbSet { get; set; }

        protected UnitOfWork UnitOfWork { get; set; }

        public BaseRepository(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentException("An instance of the unitOfWork is null", "unitOfWork");

            this.Context = unitOfWork.Context;
            this.DbSet = this.Context.Set<T>();

            this.UnitOfWork = unitOfWork;
        }


        public BaseRepository()
        {
            this.Context = new CHSystemContext();
            this.DbSet = this.Context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);

            return query.ToList();
        }

        public virtual T GetByID(int id)
        {
            return DbSet.Find(id);
        }

        public virtual T First(Expression<Func<T, bool>> filter)
        {
            return DbSet.FirstOrDefault(filter);
        }

        private void Insert(T entity)
        {
            this.DbSet.Add(entity);
            this.Context.SaveChanges();
        }

        private void Update(T entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
        public virtual void Save(T entity)
        {
            if (entity.ID > 0)
                Update(entity);
            else
                Insert(entity);
        }

        public virtual void Delete(int id)
        {
            this.DbSet.Remove(GetByID(id));
            this.Context.SaveChanges();
        }

        public virtual void Dispose()
        {
            if (this.Context != null)
                this.Context.Dispose();
        }
    }
}