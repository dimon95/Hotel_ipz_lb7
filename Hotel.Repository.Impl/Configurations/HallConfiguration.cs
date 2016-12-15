using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;

namespace Hotel.Repository.Configurations
{
    public class HallConfiguration : BasicConfiguration<Hall>
    {
        public HallConfiguration ()
        {
            Property( p => p.Description ).IsRequired();
            HasMany( p => p.Bookings );
        }
    }
}
