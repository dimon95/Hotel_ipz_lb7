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
using Hotel.Dto;

namespace Hotel.Services.Impl.Tests
{
    [TestClass()]
    public class CartServiceTests
    {
        [TestMethod]
        public void GetBookingsListTest ()
        {
            var bookHolderRepMock  = Substitute.For<IBookingHolderRepository>();
            var bookRepMock = Substitute.For<IBookingRepository>();
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Cart cart1 = new Cart(Guid.NewGuid());

            bookHolderRepMock.GetBookings( cart1.Id ).
                Returns(new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() });

            CartService cServ = new CartService(bookHolderRepMock, bookRepMock, roomRep, hallRep);

            Assert.IsTrue( cServ.GetBookingsList( cart1.Id ).Count == 3 );
        }

        [TestMethod()]
        public void AddItemTest ()
        {
            var bookHolderRepMock  = Substitute.For<IBookingHolderRepository>();
            var bookRepMock = Substitute.For<IBookingRepository>();
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Cart cart1 = new Cart(Guid.NewGuid());
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);
            PeriodDto pDto = new PeriodDto(Guid.NewGuid(), 1, 02, 2017, 5, 02, 2017);
            BookingDto bDto = new BookingDto(Guid.NewGuid(), pDto, r1.Id, "Vasya", "Pupkin", "Ivanov");
            
            bookHolderRepMock.Load( cart1.Id ).Returns( cart1 );
            roomRep.Load( r1.Id ).Returns( r1 );

            Assert.IsTrue( cart1.Bookings.Count == 0 );

            CartService cServ = new CartService(bookHolderRepMock, bookRepMock, roomRep, hallRep);

            cServ.AddItem( cart1.Id, bDto );

            Assert.IsTrue( cart1.Bookings.Count == 1 );
        }

        [TestMethod()]
        public void ClearTest ()
        {
            var bookHolderRepMock  = Substitute.For<IBookingHolderRepository>();
            var bookRepMock = Substitute.For<IBookingRepository>();
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Cart cart1 = new Cart(Guid.NewGuid());
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);
            PeriodDto pDto = new PeriodDto(Guid.NewGuid(), 1, 02, 2017, 5, 02, 2017);
            BookingDto bDto = new BookingDto(Guid.NewGuid(), pDto, r1.Id, "Vasya", "Pupkin", "Ivanov");

            bookHolderRepMock.Load( cart1.Id ).Returns( cart1 );
            roomRep.Load( r1.Id ).Returns( r1 );

            Assert.IsTrue( cart1.Bookings.Count == 0 );

            CartService cServ = new CartService(bookHolderRepMock, bookRepMock, roomRep, hallRep);

            cServ.AddItem( cart1.Id, bDto );

            Assert.IsTrue( cart1.Bookings.Count == 1 );

            cServ.Clear(cart1.Id);

            Assert.IsTrue( cart1.Bookings.Count == 0 );
        }

        [TestMethod()]
        public void DeleteItemTest ()
        {
            var bookHolderRepMock  = Substitute.For<IBookingHolderRepository>();
            var bookRepMock = Substitute.For<IBookingRepository>();
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Cart cart1 = new Cart(Guid.NewGuid());
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);
            PeriodDto pDto = new PeriodDto(Guid.NewGuid(), 1, 02, 2017, 5, 02, 2017);
            BookingDto bDto = new BookingDto(Guid.NewGuid(), pDto, r1.Id, "Vasya", "Pupkin", "Ivanov");

            bookHolderRepMock.Load( cart1.Id ).Returns( cart1 );
            bookRepMock.Load( bDto.Id ).Returns( new Booking( bDto.Id, ModelBuilder.BuildPeriod( pDto ), r1, bDto.Name, bDto.Surname, bDto.Middlename ) );
            roomRep.Load( r1.Id ).Returns( r1 );

            Assert.IsTrue( cart1.Bookings.Count == 0 );

            CartService cServ = new CartService(bookHolderRepMock, bookRepMock, roomRep, hallRep);

            cServ.AddItem( cart1.Id, bDto );

            Assert.IsTrue( cart1.Bookings.Count == 1 );

            cServ.DeleteItem( cart1.Id, bDto.Id );

            Assert.IsTrue( cart1.Bookings.Count == 0 );
        }

        [TestMethod()]
        public void GetTotalCoastTest ()
        {
            var bookHolderRepMock  = Substitute.For<IBookingHolderRepository>();
            var bookRepMock = Substitute.For<IBookingRepository>();
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Cart cart1 = new Cart(Guid.NewGuid());
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);
            Room r2 = new Room(Guid.NewGuid(), 2, 2, 2000, "dfg", 2);

            PeriodDto pDto = new PeriodDto(Guid.NewGuid(), 1, 02, 2017, 5, 02, 2017);
            BookingDto bDto = new BookingDto(Guid.NewGuid(), pDto, r1.Id, "Vasya", "Pupkin", "Ivanov");
            BookingDto bDto2 = new BookingDto(Guid.NewGuid(), pDto, r2.Id, "Vasya", "Pupkin", "Ivanov");

            bookHolderRepMock.Load( cart1.Id ).Returns( cart1 );
            roomRep.Load( r1.Id ).Returns( r1 );
            roomRep.Load( r2.Id ).Returns( r2 );

            Assert.IsTrue( cart1.Bookings.Count == 0 );

            CartService cServ = new CartService(bookHolderRepMock, bookRepMock, roomRep, hallRep);

            cServ.AddItem( cart1.Id, bDto );
            cServ.AddItem( cart1.Id, bDto2 );

            Assert.IsTrue( cServ.GetTotalCoast( cart1.Id ) == cart1.Calculate() );
        }
    }
}