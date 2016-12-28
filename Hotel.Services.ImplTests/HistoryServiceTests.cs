using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Hotel.Repository;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Services.Impl.Tests
{
    [TestClass()]
    public class HistoryServiceTests
    {
        [TestMethod()]
        public void RescheduleBookingTest ()
        {
            var bookHolderRepMock  = Substitute.For<IBookingHolderRepository>();
            var bookRepMock = Substitute.For<IBookingRepository>();
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            BookingHistory bh = new BookingHistory(Guid.NewGuid());
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);
            BookingPeriod p = new BookingPeriod(Guid.NewGuid(), new Utils.BookingDate(1,01,2017), new Utils.BookingDate(06, 01, 2017));
            BookingPeriod newP = new BookingPeriod(Guid.NewGuid(), new Utils.BookingDate(3,01,2017), new Utils.BookingDate(10, 01, 2017));
        
            Booking b = new Booking(Guid.NewGuid(), p, r1, "Vasya", "Pupkin", "Ivanov");

            bh.AddBooking( b );

            HistoryService hServ = new HistoryService(bookHolderRepMock, bookRepMock, roomRep, hallRep);

            bookHolderRepMock.Load( bh.Id ).Returns( bh);
            bookHolderRepMock.GetBooking( bh.Id, b.Id ).Returns( b );

            hServ.RescheduleBooking( bh.Id, b.Id, newP.toDto() );

            var v = hServ.View( bh.Id );

            Dto.BookingDto bDto =  v.Bookings.FirstOrDefault( x => x.Id == b.Id );

            var bookPeriod = r1.Bookings[0];

            Assert.IsTrue( bDto.BookingPeriod.BeginDay == 3 && bookPeriod.Begin.Day == 3);
            Assert.IsTrue( bDto.BookingPeriod.BeginMonth == 1 && bookPeriod.Begin.Month == 1 );
            Assert.IsTrue( bDto.BookingPeriod.BeginYear == 2017 && bookPeriod.Begin.Year == 2017 );

            Assert.IsTrue( bDto.BookingPeriod.EndDay == 10 && bookPeriod.End.Day == 10 );
            Assert.IsTrue( bDto.BookingPeriod.EndMonth == 1 && bookPeriod.End.Month == 1 );
            Assert.IsTrue( bDto.BookingPeriod.EndYaer == 2017 && bookPeriod.End.Year == 2017 );
        }
    }
}