using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;
using Hotel.Model.Entities.Abstract;


namespace Hotel.Repository.Impl
{
    public abstract class PlaceRepository<T> : BasicRepository<T>
        where T : Place
    {
        public PlaceRepository ( HotelDbContext dbContext, DbSet<T> dbSet ) 
            : base( dbContext, dbSet )
        {
        }

        //public abstract void Add ( Place t );
        //public abstract void Delete ( Place t );

        public T Find ( int number )
        {
            return DbSet.FirstOrDefault( p => p.Number == number );
        }

        public T Find (BookingPeriod bp)
        {
            return DbSet.FirstOrDefault( p => p.isFree( bp ) );
        }
    }
}
