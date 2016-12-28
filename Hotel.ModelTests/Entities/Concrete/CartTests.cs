using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Utils;

namespace Hotel.Model.Entities.Concrete.Tests
{
    [TestClass()]
    public class CartTests
    {
        [TestMethod()]
        public void CalculateTest ()
        {
            Room r = new Room(Guid.NewGuid(), 1, 2, 2000, "dfgdfg", 2);

            BookingPeriod bp = new BookingPeriod(Guid.NewGuid(), new BookingDate(31,12,2016), new BookingDate(05,01,2017));

            string name = "Ivan";
            string mName ="Ivanov";
            string sName = "Ivanov";

            Booking b = new Booking(Guid.NewGuid(),bp,
                r,name ,mName , sName);

            Cart c = new Cart(Guid.NewGuid());

            Assert.IsTrue( c.Bookings.Count == 0 );

            c.AddBooking( b );

            Assert.IsTrue( c.Bookings.Contains( b ) );

            Assert.IsTrue( c.Calculate() == b.BookedPlace.Price * b.BookingPeriod.CountDays() );
        }

        [TestMethod()]
        public void MakeEmptyTest ()
        {
            Room r = new Room(Guid.NewGuid(), 1, 2, 2000, "dfgdfg", 2);
            Room r2 = new Room(Guid.NewGuid(), 2, 2, 2000, "dfgdfg", 2);


            BookingPeriod bp = new BookingPeriod(Guid.NewGuid(), new BookingDate(31,12,2016), new BookingDate(05,01,2017));

            string name = "Ivan";
            string mName ="Ivanov";
            string sName = "Ivanov";

            Booking b = new Booking(Guid.NewGuid(),bp,
                r,name ,mName , sName);
            Booking b2 = new Booking(Guid.NewGuid(),bp,
                r2,name ,mName , sName);

            Cart c = new Cart(Guid.NewGuid());

            c.AddBooking( b );
            c.AddBooking( b2 );

            Assert.IsTrue( c.Bookings.Count == 2 );

            c.MakeEmpty();

            Assert.IsTrue( c.Bookings.Count == 0 );
        }
    }
}