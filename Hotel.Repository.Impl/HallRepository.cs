using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository.Impl
{
    public class HallRepository : PlaceRepository<Hall>, IHallRepository
    {
        public HallRepository ( HotelDbContext dbContext ) : base( dbContext, dbContext.Halls )
        {
        }
    }
}
