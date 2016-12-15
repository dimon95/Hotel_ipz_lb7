using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dto
{
    public class BookingDto : DomainDto
    {
        public PeriodDto BookingPeriod { get; private set; }

        public Guid BookedPlaceId { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Middlename { get; private set; }
                
        public BookingDto ( Guid id, PeriodDto period, Guid bookedPlaceId, string name, string surname, string middlename )
            : base(id)
        {
            BookingPeriod = period;
            BookedPlaceId = bookedPlaceId;

            Name = name;
            Surname = surname;
            Middlename = middlename;
        }

        public override string ToString ()
        {
            return "Place " + BookedPlaceId + " is booked for period " + BookingPeriod.ToString();
        }
    }
}
