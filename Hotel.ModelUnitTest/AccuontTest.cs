using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Hotel.Model.Entities.Concrete;
using Hotel.Utils;

namespace Hotel.ModelUnitTest
{
    [TestClass]
    public class AccuontTest
    {
        [TestMethod]
        public void TestClientCreate ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            Guid g1 = Guid.NewGuid();
            string name1 = "Ivan";
            string sName1 = "Ivanov";
            string mName1 = "Ivanovich";
            string email1 = "ivanov@example.com";
            string passwordHash = hashProvider.GetHashCode("1111");
            DateOfBirth date1 = new DateOfBirth(15, 08, 1970);

            Client cl1 = new Client(g1, name1, sName1, mName1, email1, passwordHash, date1);

            Assert.AreEqual<Guid>( g1, cl1.Id );
            Assert.AreEqual<string>( name1, cl1.Name );
            Assert.AreEqual<string>( sName1, cl1.Surname );
            Assert.AreEqual<string>( mName1, cl1.Middlename );
            Assert.AreEqual<string>( email1, cl1.Email );
            Assert.AreEqual<string>( passwordHash, cl1.PasswordHash );
            Assert.AreEqual<DateOfBirth>( date1, cl1.DateOfBirth );

            Assert.IsTrue( cl1.History.Bookings.Count == 0 && cl1.Cart.Bookings.Count == 0 );
        }

        [TestMethod]
        public void TestAdminCreate ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            Guid g1 = Guid.NewGuid();
            string name1 = "Ivan";
            string sName1 = "Ivanov";
            string mName1 = "Ivanovich";
            string email1 = "ivanov@example.com";
            string passwordHash = hashProvider.GetHashCode("1111");
            DateOfBirth date1 = new DateOfBirth(15, 08, 1970);

            Admin cl1 = new Admin(g1, name1, sName1, mName1, email1, passwordHash, date1);

            Assert.AreEqual<Guid>( g1, cl1.Id );
            Assert.AreEqual<string>( name1, cl1.Name );
            Assert.AreEqual<string>( sName1, cl1.Surname );
            Assert.AreEqual<string>( mName1, cl1.Middlename );
            Assert.AreEqual<string>( email1, cl1.Email );
            Assert.AreEqual<string>( passwordHash, cl1.PasswordHash );
            Assert.AreEqual<DateOfBirth>( date1, cl1.DateOfBirth );

            Assert.IsTrue( cl1.History.Bookings.Count == 0 && cl1.Cart.Bookings.Count == 0 );
        }

        [TestMethod]
        public void TestAddDeleteToCart ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            Guid g1 = Guid.NewGuid();
            string name1 = "Ivan";
            string sName1 = "Ivanov";
            string mName1 = "Ivanovich";
            string email1 = "ivanov@example.com";
            string passwordHash = hashProvider.GetHashCode("1111");
            DateOfBirth date1 = new DateOfBirth(15, 08, 1970);

            Client cl1 = new Client(g1, name1, sName1, mName1, email1, passwordHash, date1);
            Room r = new Room(Guid.NewGuid(), 1, 2, 2000, "dfgdfg", 2);
            Booking b = new Booking(Guid.NewGuid(),
                new BookingPeriod(Guid.NewGuid(), new BookingDate(27,12,2016), new BookingDate(05,01,2017)),
                r, "Ivan", "Ivanov", "Ivanov");

            cl1.AddToCart( b );

            Assert.IsTrue( cl1.Cart.Bookings.Count == 1 );

            cl1.DeleteFromCart( b );

            Assert.IsTrue( cl1.Cart.Bookings.Count == 0 );

        }

        [TestMethod]
        public void TestPaymentMade ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            Guid g1 = Guid.NewGuid();
            string name1 = "Ivan";
            string sName1 = "Ivanov";
            string mName1 = "Ivanovich";
            string email1 = "ivanov@example.com";
            string passwordHash = hashProvider.GetHashCode("1111");
            DateOfBirth date1 = new DateOfBirth(15, 08, 1970);

            Client cl1 = new Client(g1, name1, sName1, mName1, email1, passwordHash, date1);
            Room r = new Room(Guid.NewGuid(), 1, 2, 2000, "dfgdfg", 2);
            Booking b = new Booking(Guid.NewGuid(),
                new BookingPeriod(Guid.NewGuid(), new BookingDate(27,12,2016), new BookingDate(05,01,2017)),
                r, "Ivan", "Ivanov", "Ivanov");

            cl1.AddToCart( b );
            cl1.PaymentMade();

            Assert.IsTrue( cl1.Cart.Bookings.Count == 0 && cl1.History.Bookings.Count == 1 );
        }

        [TestMethod]
        public void TestClear ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            Guid g1 = Guid.NewGuid();
            string name1 = "Ivan";
            string sName1 = "Ivanov";
            string mName1 = "Ivanovich";
            string email1 = "ivanov@example.com";
            string passwordHash = hashProvider.GetHashCode("1111");
            DateOfBirth date1 = new DateOfBirth(15, 08, 1970);

            Client cl1 = new Client(g1, name1, sName1, mName1, email1, passwordHash, date1);
            Room r = new Room(Guid.NewGuid(), 1, 2, 2000, "dfgdfg", 2);
            Room r2 = new Room(Guid.NewGuid(), 2, 2, 2500, "dfgdfg", 2);

            Booking b = new Booking(Guid.NewGuid(),
                new BookingPeriod(Guid.NewGuid(), new BookingDate(27,12,2016), new BookingDate(05,01,2017)),
                r, "Ivan", "Ivanov", "Ivanov");

            Booking b2 = new Booking(Guid.NewGuid(),
                new BookingPeriod(Guid.NewGuid(), new BookingDate(27,12,2016), new BookingDate(05,01,2017)),
                r2, "Ivan", "Ivanov", "Ivanov");

            cl1.AddToCart( b );

            Assert.IsTrue( cl1.Cart.Bookings.Count == 1 );

            cl1.PaymentMade();

            Assert.IsTrue( cl1.Cart.Bookings.Count == 0 && cl1.History.Bookings.Count == 1 );

            cl1.AddToCart( b2 );

            Assert.IsTrue( cl1.Cart.Bookings.Count == 1 );

            cl1.ClearCart();
            cl1.ClearHistory();

            Assert.IsTrue( cl1.Cart.Bookings.Count == 0 && cl1.History.Bookings.Count == 0 );
        }
    }
}
