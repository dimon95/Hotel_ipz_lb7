using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;
using System.Data.Entity;
using Hotel.Model.Entities.Abstract;

namespace Hotel.Repository.Impl
{
    public class RoomRepository : PlaceRepository<Room>, IRoomRepository
    {
        public RoomRepository ( HotelDbContext dbContext ) : base( dbContext, dbContext.Rooms )
        {
            
        }
    }
}
