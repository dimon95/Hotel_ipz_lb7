using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Utils;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Model.Entities.Abstract
{
    public abstract class BookingHolder : Entity
    {
        public virtual IList<Booking> Bookings { get; private set; }

        protected BookingHolder ()
        {
            /*Bookings = new List<Booking>();*/
        }

        public BookingHolder ( Guid id )
            : base( id )
        {
            Bookings = new List<Booking>();
        }

        public void AddBooking ( Booking b )
        {
            if ( b == null )
                throw new ArgumentNullException( "b" );

            if ( Bookings.FirstOrDefault( booking => booking.Id == b.Id ) != null )
                throw new ArgumentException( "booking alredy hear" );

            Bookings.Add( b );
        }

        public void DeleteBooking ( Booking b )
        {
            if ( b == null )
                throw new ArgumentNullException( "b" );

            if ( Bookings.FirstOrDefault( booking => booking.Id == b.Id ) == null )
                throw new ArgumentException( "no such booking" );

            b.BookedPlace.DeleteBookingPeriod( b.BookingPeriod );

            Bookings.Remove( b );
        }

        public void Clear ()
        {
            foreach ( Booking b in Bookings )
            {

                b.BookedPlace.DeleteBookingPeriod( b.BookingPeriod );
            }

            Bookings.Clear();
        }

        // public abstract void Accept ( IBookingHolderVisitor visitor );
    }
}
