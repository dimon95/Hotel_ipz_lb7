using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model.Entities.Abstract;

namespace Hotel.Model.Entities.Concrete
{
    public class BookingHistory : Abstract.BookingHolder
    {
        protected BookingHistory () { }

        public BookingHistory ( Guid id )
            :base(id)
        {
        }

        
    }
}
