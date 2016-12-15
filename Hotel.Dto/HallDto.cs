using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dto
{
    public class HallDto : PlaceDto
    {
        public HallDto ( Guid id, int number, decimal price, string description, int personsCount, bool onRestavration,
                IList<PeriodDto> bookings ) 
            : base( id, number, price, description, personsCount, onRestavration, bookings )
        {
        }

        public override string ToString ()
        {
            return base.ToString() + "\r\n\r\n";
        }
    }
}
