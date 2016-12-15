using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository.Impl
{
    public class BookingRepository : BasicRepository<Booking>, IBookingRepository
    {
        public BookingRepository ( HotelDbContext dbContext) : base( dbContext, dbContext.Bookings )
        {
        }
    }
}
