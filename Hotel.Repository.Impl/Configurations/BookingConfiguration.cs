using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository.Configurations
{
    public class BookingConfiguration : BasicConfiguration<Booking>
    {
        public BookingConfiguration ()
        {
            HasRequired( b => b.BookedPlace );

            Property( b => b.Name ).IsRequired();
            Property( b => b.Surname ).IsRequired();

            HasRequired( b => b.BookingPeriod );
        }
    }
}
