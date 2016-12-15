using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Repository;
using System.Data.Entity;
using Hotel.Utils;

namespace Hotel.Repository.Impl
{
    public abstract class BasicRepository<T> where T : Entity
    {
        private HotelDbContext _dbContext;
        private DbSet<T> _set;

        protected BasicRepository ( HotelDbContext dbContext, DbSet<T> dbSet )
        {
            _dbContext = dbContext;
            _set = dbSet;
        }

        protected DbContext DbContext
        {
            get { return _dbContext; }
        }

        protected DbSet<T> DbSet
        {
            get { return _set; }
        }

        public virtual T Load ( Guid id )
        {
            /*Не работает с Room*/

            return _set.FirstOrDefault( b => b.Id == id );

            /*var tmp  = _set.Local;

            foreach ( var entry in tmp )
            {
                if ( entry.Id == id )
                    return entry;
            }

            return null;*/
        }

        public virtual IQueryable<T> LoadAll ()
        {
            return _set;
        }

        public virtual int Count ()
        {
            return _set.Count();
        }

        public virtual void Add ( T t )
        {
            _set.Add(t);
        }

        public virtual void Delete ( T t )
        {
            _set.Remove( t );
        }

        public virtual void StartTransaction ()
        {
            _dbContext.Database.BeginTransaction();
        }

        public virtual void Commit ()
        {
            _dbContext.ChangeTracker.DetectChanges();
            _dbContext.SaveChanges();
            _dbContext.Database.CurrentTransaction.Commit();
        }

        public virtual void Rollback ()
        {
            _dbContext.Database.CurrentTransaction.Rollback();
        }

        public virtual IQueryable<Guid> SelectAllDomainIds ()
        {
            return DbSet.Select( t => t.Id );
        }
    }
}
