using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository
{
    public interface IBookingHolderRepository : IRepository<BookingHolder>
    {
        IEnumerable<Guid> GetBookings ( Guid bookingHolderId );

        IQueryable<Guid> GetCarts ();

        IQueryable<Guid> GetHistories ();

        Booking GetBooking ( Guid bookingHolderId, Guid bookingId );
    }
}
