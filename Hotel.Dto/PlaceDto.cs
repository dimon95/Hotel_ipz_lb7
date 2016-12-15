using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dto
{
    public abstract class PlaceDto : DomainDto
    {
        public int Number { get; private set; }
        public int PersonsCount { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool OnRestavration { get; private set; }

        public IList<PeriodDto> Bookings { get; private set; }

        public PlaceDto ( Guid id, int number, decimal price, string description, int personsCount, bool onRestavration, 
                IList<PeriodDto> bookings )
            : base( id )
        {
            Number = number;
            PersonsCount = personsCount;
            Description = description;
            Price = price;
            Bookings = bookings;
            OnRestavration = onRestavration;
        }

        public override string ToString ()
        {
            string res = "Number: " + Number + "\r\n" + "Persons count: " + PersonsCount + "\r\n" +
                          "Description: " + Description + "\r\n" + "Price: " + Price + "\r\n";

            res += "Place is booked for dates:\r\n";

            foreach ( PeriodDto p in Bookings )
            {
                res += p.ToString();
            }

            //res += "\r\n\r\n";

            return res;
        }
    }
}
