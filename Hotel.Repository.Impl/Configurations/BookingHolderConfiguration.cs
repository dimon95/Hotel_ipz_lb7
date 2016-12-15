using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Hotel.Model.Entities.Abstract;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository.Configurations
{
    public class BookingHolderConfiguration : BasicConfiguration<BookingHolder>
    {
        public BookingHolderConfiguration ()
        {
            HasMany( b => b.Bookings ).WithOptional();
            Map<BookingHistory>( h => { h.ToTable( "Histories" ); h.MapInheritedProperties(); } );
            Map<Cart>( c => { c.ToTable( "Carts" ); c.MapInheritedProperties(); } );
        }
    }
}
