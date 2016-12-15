using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Utils;
using Hotel.Model.Entities.Abstract;

namespace Hotel.Model.Entities.Concrete
{
    public enum BookingStatus { Booked, Active, Ended }

    public class Booking : Entity
    {
        //private FullName _bookingOnName;

        private NotEmptyString _name = new NotEmptyString();
        private NotEmptyString _surname = new NotEmptyString();

        public virtual BookingPeriod BookingPeriod { get; set; }

        public virtual Place BookedPlace { get; set; }

        public string Name
        {
            get { return _name.Value; }
            set { _name.Value = value; }
        }

        public string Surname
        {
            get { return _surname.Value; }
            set { _surname.Value = value; }
        }

        public string Middlename { get; set; }

        protected Booking () { /*_bookingOnName = new FullName();*/ }

        public Booking ( Guid id, BookingPeriod period, Place bookedPlace, string name, string surname, string middlename )
            : base(id)
        {
            if ( !bookedPlace.isFree( period ) || bookedPlace.OnRestavretion)
            {
                throw new ArgumentException("This room is already booked on the period");
            }

            BookingPeriod = period;
            BookedPlace = bookedPlace;

            Name = name;
            Surname = surname;
            Middlename = middlename;
        }

        public BookingStatus CheckStatus ()
        {
            if ( Date.GetToday() < BookingPeriod.Begin )
                return BookingStatus.Booked;

            if ( Date.GetToday() > BookingPeriod.Begin && Date.GetToday() < BookingPeriod.End )
                return BookingStatus.Active;

            if ( Date.GetToday() > BookingPeriod.End )
                return BookingStatus.Ended;

            throw new Exception("Something went wrong");
        }

        public override string ToString ()
        {
            return "Room number " + BookedPlace.Number + " booked for period: " + BookingPeriod + "\r\n";
        }
    }
}
