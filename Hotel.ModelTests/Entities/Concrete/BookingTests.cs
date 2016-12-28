using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Model.Entities.Concrete;
using System;
using Hotel.Utils;

namespace Hotel.Model.Entities.Concrete.Tests
{
    [TestClass()]
    public class BookingTests
    {
        [TestMethod()]
        public void BookingTest ()
        {
            Room r = new Room(Guid.NewGuid(), 1, 2, 2000, "dfgdfg", 2);

            BookingPeriod bp = new BookingPeriod(Guid.NewGuid(), new BookingDate(31,12,2016), new BookingDate(05,01,2017));

            string name = "Ivan";
            string mName ="Ivanov";
            string sName = "Ivanov";

            Booking b = new Booking(Guid.NewGuid(),bp,
                r,name ,mName , sName);

            Assert.AreSame( bp, b.BookingPeriod );
            Assert.AreSame( b.BookedPlace, r );
            Assert.AreEqual( b.Name, name );
            Assert.AreEqual( b.Middlename, mName );
            Assert.AreEqual( b.Surname, sName );

            Assert.IsTrue( b.CheckStatus() == BookingStatus.Booked );
        }
    }
}