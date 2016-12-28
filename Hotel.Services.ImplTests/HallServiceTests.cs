using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.Model.Entities.Concrete;
using NSubstitute;
using Hotel.Repository;

namespace Hotel.Services.Impl.Tests
{
    [TestClass()]
    public class HallServiceTests
    {

        [TestMethod()]
        public void CreateHallTest ()
        {
            List<Hall> hallList = new List<Hall>();
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();
            Hall h1 = new Hall(Guid.NewGuid(), 1, 2, 2000, "dfg");

            hallRep.Find( h1.Number ).Returns( ( e ) => hallList.FirstOrDefault( r => r.Number == h1.Number ) );
            hallRep.Add( Arg.Do<Hall>( h => hallList.Add( h ) ) );

            HallService hallSer = new HallService(roomRep, hallRep);

            Assert.IsTrue( hallRep.Find( h1.Number ) == null );

            hallSer.CreateHall( h1.Number, h1.Description, h1.PersonsCount, h1.Price );

            Assert.IsTrue( hallRep.Find( h1.Number ) != null );
        }

        [TestMethod()]
        public void GetFreeHallsTest ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Hall h1 = new Hall(Guid.NewGuid(), 1, 2, 2000, "h1");
            h1.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
                new Utils.BookingDate( 1, 01, 2017 ),
                new Utils.BookingDate( 5, 01, 2017 ) ) );
            h1.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
                new Utils.BookingDate( 6, 01, 2017 ),
                new Utils.BookingDate( 10, 01, 2017 ) ) );

            Hall h2 = new Hall(Guid.NewGuid(), 1, 2, 3000, "h2");
            h2.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
               new Utils.BookingDate( 1, 01, 2017 ),
               new Utils.BookingDate( 5, 01, 2017 ) ) );

            Hall h3 = new Hall(Guid.NewGuid(), 1, 2, 3500, "h3");
            h3.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
                new Utils.BookingDate( 11, 01, 2017 ),
                new Utils.BookingDate( 20, 01, 2017 ) ) );

            Hall h4 = new Hall(Guid.NewGuid(), 1, 2, 3200, "h4");
            Hall h5 = new Hall(Guid.NewGuid(), 1, 2, 10000, "h5");
            h5.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
                new Utils.BookingDate( 1, 01, 2017 ),
                new Utils.BookingDate( 3, 01, 2017 ) ) );
            h5.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
                new Utils.BookingDate( 17, 01, 2017 ),
                new Utils.BookingDate( 22, 01, 2017 ) ) );


            IQueryable<Hall> hList = (new List<Hall> { h1, h2, h3, h4, h5 }).AsQueryable();

            hallRep.LoadAll().Returns( hList );

            HallService hServ = new HallService(roomRep, hallRep);

            Assert.IsTrue( hServ.GetFreeHalls( new Dto.PeriodDto( Guid.NewGuid(), 1, 1, 2017, 10, 1, 2017 ) ).Count == 2 );
            Assert.IsTrue( hServ.GetFreeHalls( new Dto.PeriodDto( Guid.NewGuid(), 15, 1, 2017, 20, 1, 2017 ) ).Count == 4 );

        }

        [TestMethod()]
        public void GetHallsTest ()
        {
            List<Hall> halls = new List<Hall>()
            {
                new Hall(Guid.NewGuid(), 1, 2, 2000, "r1"),
                new Hall(Guid.NewGuid(), 1, 2, 3000, "r2"),
                new Hall(Guid.NewGuid(), 1, 2, 3500, "r3"),
                new Hall(Guid.NewGuid(), 1, 2, 3200, "r4"),
                new Hall(Guid.NewGuid(), 1, 2, 10000, "r5"),
                new Hall(Guid.NewGuid(), 1, 2, 200, "r6")
            };

            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            hallRep.LoadAll().Returns( halls.AsQueryable() );

            HallService hServ = new HallService( roomRep, hallRep );

            Assert.IsTrue( hServ.GetHalls( ).Count == 6 );
        }

        [TestMethod()]
        public void HasHallWithNumberTest ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Hall r1 = new Hall(Guid.NewGuid(), 1, 2, 2000, "r1");

            hallRep.Find( r1.Number ).Returns( r1 );

            HallService rServ = new HallService( roomRep, hallRep );

            Assert.IsTrue( rServ.HasHallWithNumber( r1.Number ) );
        }
    }
}