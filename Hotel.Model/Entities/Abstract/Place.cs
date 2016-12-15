using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Utils;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Model.Entities.Abstract
{
    public abstract class Place : Entity
    {
        private NotEmptyString _description = new NotEmptyString();

        public string Description
        {
            get { return _description.Value; }
            set { _description.Value = value; }
        }

        public int Number { get; set; }
        public int PersonsCount { get; set; }
        public decimal Price { get; set; }

        public bool OnRestavretion { get; set; }

        //public virtual IEnumerable<NotEmptyString>PhotosUrls;

        public virtual IList<BookingPeriod> Bookings { get; private set; }

        protected Place () { /*Bookings = new List<BookingPeriod>();*/  }

        public Place ( Guid id, int number, int personsCount, decimal price, string description )
            : base( id )
        {
            if ( number <= 0 || personsCount <= 0 || price <= 0)
                throw new ArgumentException("numbers must be higher then zero");

            Number = number;
            PersonsCount = personsCount;
            Description = description;
            Bookings = new List<BookingPeriod>();
            Price = price;

            ResetFromRestavretion();
        }

        public void SetOnRestavretion ()
        {
            OnRestavretion = true;
        }

        public void ResetFromRestavretion ()
        {
            OnRestavretion = false;
        }

        public bool isFree ( BookingPeriod period )
        {
            foreach ( var p in Bookings )
            {
                if ( period.Intersect( p ) )
                    return false;
            }

            return true;
        }

        public void AddBookingPeriod ( BookingPeriod bp )
        {
            Bookings.Add( bp );
        }

        public void DeleteBookingPeriod(BookingPeriod bp)
        {
            Bookings.Remove( bp );    
        }

        public override string ToString ()
        {
            string res;

            res = "Place number: " + Number + "\r\n" + "Person count: " + PersonsCount + "\r\n" +
                "Price: " + Price + "\r\n" +
                "Place is booked for dates: \r\n";

            foreach ( BookingPeriod bp in Bookings )
            {
                res += bp;
            }

            res += "\r\n";

            return res;
        }
    }
}
