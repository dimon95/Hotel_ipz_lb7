using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dto
{
    public class BookingHolderDto : DomainDto
    {
        public IList<BookingDto> Bookings { get; private set; }

        public BookingHolderDto ( Guid id, IList<BookingDto> bookings )
            : base( id )
        {
            Bookings = bookings;
        }

        public override string ToString ()
        {
            string res = "Booking holder has items:\r\n";

            foreach ( BookingDto b in Bookings )
            {
                res += b.ToString();
            }

            res += "\r\n";

            return res;
        }
    }
}
