using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;
using System.Data.Entity;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository.Impl
{
    public class BookingHolderRepository : BasicRepository<BookingHolder>, IBookingHolderRepository
    {
        public BookingHolderRepository ( HotelDbContext dbContext ) : base( dbContext, dbContext.BookingHolders )
        {
        }

        public Booking GetBooking ( Guid bookingHolderId, Guid bookingId )
        {
            BookingHolder bHolder = Load(bookingHolderId);

            return bHolder.Bookings.FirstOrDefault( b => b.Id == bookingId );
        }

        public IEnumerable<Guid> GetBookings ( Guid bookingHolderId )
        {
            return DbSet.FirstOrDefault( bh => bh.Id == bookingHolderId ).Bookings.Select( b => b.Id );
        }

        public IQueryable<Guid> GetCarts ()
        {
            return DbSet.Where(c => c is Cart).Select(c => c.Id);
        }

        public IQueryable<Guid> GetHistories ()
        {
            return DbSet.Where( c => c is BookingHistory ).Select( c => c.Id );

        }
    }
}
