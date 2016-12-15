using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Utils;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Model.Entities.Abstract
{
    public abstract class Account : Entity
    {
        //private FullName _fullName = new FullName();

        private NotEmptyString _name = new NotEmptyString();
        private NotEmptyString _surname = new NotEmptyString();

        private NotEmptyString _email = new NotEmptyString();
        private NotEmptyString _passwordHash = new NotEmptyString();

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

        public string Email
        {
            get { return _email.Value; }
            set { _email.Value = value; }
        }

        public string PasswordHash
        {
            get { return _passwordHash.Value; }
            set { _passwordHash.Value = value; }
        }

        public string Middlename { get; set; }

        public DateOfBirth DateOfBirth { get; set; }

        public virtual BookingHistory History { get; set; }

        public virtual Cart Cart { get; set; }

        protected Account ()
        {
        }
        
        protected Account ( Guid id, string name, string surname, string middlename,
            string email, string passwordHash, DateOfBirth dateOfBirth )
            : base( id )
        {
            //_fullName = new FullName( name, surname, middlename );

            Name = name;
            Surname = surname;
            Middlename = middlename;
            Email = email;
            PasswordHash = passwordHash;
            DateOfBirth = dateOfBirth;

            History = new BookingHistory( Guid.NewGuid() );
            Cart = new Cart( Guid.NewGuid() );
        }

        public void AddToCart ( Booking b )
        {
            Cart.AddBooking( b );
        }

        public void DeleteFromCart ( Booking b )
        {
            Cart.DeleteBooking( b );
        }

        public void PaymentMade ()
        {
            foreach ( Booking b in Cart.Bookings )
            {
                History.AddBooking( b );
                b.BookedPlace.AddBookingPeriod( b.BookingPeriod );
            }

            Cart.MakeEmpty();
        }

        public void Accept ( IAccountVisitor visitor )
        {
            visitor.Visit( this );
        }

        public override string ToString ()
        {
            string res;

            res = "Name: " + Name + "\r\n" + "Surname: " + Surname + "\r\n" + "Middlename: " + "\r\n" +
                "DateOfBirth: " + DateOfBirth + "\r\n" + "Email: " + Email + "\r\n";
            res += "Cart has items: \r\n";

            foreach ( Booking b in Cart.Bookings )
            {
                res += b;
            }

            res += "Total coast: " + Cart.Calculate() + "\r\n";

            res += "Booking history: \r\n";

            foreach ( Booking b in History.Bookings )
            {
                res += b;
            }

            res += "\r\n";

            return res;
        }
    }
}
