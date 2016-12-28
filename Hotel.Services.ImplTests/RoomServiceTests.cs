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
using Hotel.Exceptions;

namespace Hotel.Services.Impl.Tests
{
    [TestClass()]
    public class RoomServiceTests
    {
        [TestMethod()]
        public void CreateRoomTest ()
        {
            List<Room> roomList = new List<Room>();
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);
            
            roomRep.Find( r1.Number ).Returns( (e)=> roomList.FirstOrDefault(r => r.Number == r1.Number));
            roomRep.Add( Arg.Do<Room>( r => roomList.Add( r ) ) );

            RoomService roomSer = new RoomService(roomRep, hallRep);

            Assert.IsTrue( roomRep.Find( r1.Number ) == null );

            roomSer.CreateRoom( r1.Number, r1.Description, r1.PersonsCount, r1.BedCount, r1.Price );

            Assert.IsTrue( roomRep.Find( r1.Number ) != null );
        }

        [TestMethod()]
        [ExpectedException(typeof( RemovingNotExistingRoomCriteriaException ) )]
        public void ResetCriteriaTest ()
        {
            
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);

            roomRep.Load( r1.Id ).Returns( r1 );

            RoomService roomSer = new RoomService(roomRep, hallRep);

            Assert.IsTrue( r1.SearchCriterias == 0x00 );

            roomSer.ResetCriteria( r1.Id, Model.SearchCriteria.TV );

            roomSer.SetCriteria( r1.Id, Model.SearchCriteria.Freedge );
            roomSer.SetCriteria( r1.Id, Model.SearchCriteria.TV );

            Assert.IsTrue( r1.HasCriteria( Model.SearchCriteria.Freedge )  && 
                r1.HasCriteria( Model.SearchCriteria.TV ) );

            roomSer.ResetCriteria( r1.Id, Model.SearchCriteria.Freedge );

            Assert.IsFalse( r1.HasCriteria( Model.SearchCriteria.Freedge ) );
            Assert.IsTrue( r1.HasCriteria( Model.SearchCriteria.TV ) );
        }

        [TestMethod()]
        [ExpectedException( typeof( MultipleAddingRoomCriteriaException ) )]
        public void SetCriteriaTest ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);

            roomRep.Load( r1.Id ).Returns( r1 );

            RoomService roomSer = new RoomService(roomRep, hallRep);

            Assert.IsTrue( r1.SearchCriterias == 0x00 );

            roomSer.SetCriteria( r1.Id, Model.SearchCriteria.Freedge );
            roomSer.SetCriteria( r1.Id, Model.SearchCriteria.TV );

            Assert.IsTrue( r1.HasCriteria( Model.SearchCriteria.Freedge )  && 
                r1.HasCriteria( Model.SearchCriteria.TV ) );

            roomSer.SetCriteria( r1.Id, Model.SearchCriteria.TV );
        }

        [TestMethod()]
        public void DeletePlaceTest ()
        {
            List<Room> roomList = new List<Room>();
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);

            roomRep.Find( r1.Number ).Returns( ( e ) => roomList.FirstOrDefault( r => r.Number == r1.Number ) );
            roomRep.Add( Arg.Do<Room>( r => roomList.Add( r ) ) );
            roomRep.Delete( Arg.Do<Room>( r => roomList.Remove( r ) ) );

            RoomService roomSer = new RoomService(roomRep, hallRep);

            Assert.IsTrue( roomList.Count == 0 );

            r1.Id = roomSer.CreateRoom( r1.Number, r1.Description, r1.PersonsCount, r1.BedCount, r1.Price );
            roomRep.Load( r1.Id ).Returns( r1 );

            Assert.IsTrue( roomList.Count == 1 );

            roomSer.DeletePlace( r1.Id );

            Assert.IsTrue( roomList.Count == 0 );
        }

        [TestMethod()]
        public void ChangeDescriptionTest ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);

            roomRep.Load( r1.Id ).Returns( r1 );

            RoomService roomSer = new RoomService(roomRep, hallRep);

            Assert.IsTrue( r1.Description == "dfg" );

            roomSer.ChangeDescription( r1.Id, "new desc" );

            Assert.IsTrue( r1.Description == "new desc" );
        }

        [TestMethod()]
        public void ChangePriceTest ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);

            roomRep.Load( r1.Id ).Returns( r1 );

            RoomService roomSer = new RoomService(roomRep, hallRep);

            Assert.IsTrue( r1.Price == 2000 );

            roomSer.ChangePrice( r1.Id, 5500 );

            Assert.IsTrue( r1.Price == 5500 );
        }

        [TestMethod()]
        [ExpectedException( typeof( AttemptToMultipyRestavrationResetException ) )]
        public void ResetPlaceFromRestavrationTest ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);

            roomRep.Load( r1.Id ).Returns( r1 );

            RoomService roomSer = new RoomService(roomRep, hallRep);

            Assert.IsFalse( r1.OnRestavretion );

            roomSer.ResetPlaceFromRestavration(r1.Id);

            roomSer.SetPlaceOnRestavration( r1.Id );

            Assert.IsTrue( r1.OnRestavretion );

            roomSer.ResetPlaceFromRestavration( r1.Id );

            Assert.IsFalse( r1.OnRestavretion );
        }

        [TestMethod()]
        [ExpectedException( typeof( AttemptToMultipyRestavrationSetException ) )]
        public void SetPlaceOnRestavrationTest ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();
            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "dfg", 2);

            roomRep.Load( r1.Id ).Returns( r1 );

            RoomService roomSer = new RoomService(roomRep, hallRep);

            Assert.IsFalse( r1.OnRestavretion );

            roomSer.SetPlaceOnRestavration( r1.Id );

            Assert.IsTrue( r1.OnRestavretion );

            roomSer.SetPlaceOnRestavration( r1.Id );

            roomSer.ResetPlaceFromRestavration( r1.Id );

            Assert.IsFalse( r1.OnRestavretion );
        }

        [TestMethod]
        public void GetRooms ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "r1", 2);
            Room r2 = new Room(Guid.NewGuid(), 1, 2, 3000, "r2", 2);
            Room r3 = new Room(Guid.NewGuid(), 1, 2, 3500, "r3", 2);
            Room r4 = new Room(Guid.NewGuid(), 1, 2, 3200, "r4", 2);
            Room r5 = new Room(Guid.NewGuid(), 1, 2, 10000, "r5", 2);
            Room r6 = new Room(Guid.NewGuid(), 1, 2, 200, "r6", 2);

            IQueryable<Room> rList = (new List<Room> { r1, r2, r3, r4, r5, r6 }).AsQueryable();

            roomRep.LoadAll().Returns( rList  );

            RoomService rServ = new RoomService( roomRep, hallRep );

            Assert.IsTrue( rServ.GetRooms( 3000, 3500 ).Count == 3);
            Assert.IsTrue( rServ.GetRooms( 200, 10000 ).Count == 6 );
            Assert.IsTrue( rServ.GetRooms( 0, 150 ).Count == 0 );
            rServ.GetRooms( -500, 0 );
        }

        [TestMethod]
        public void GetRooms2 ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "r1", 2);
            r1.SetCriteria( Model.SearchCriteria.Freedge );

            Room r2 = new Room(Guid.NewGuid(), 1, 2, 3000, "r2", 2);
            r2.SetCriteria( Model.SearchCriteria.Freedge );

            Room r3 = new Room(Guid.NewGuid(), 1, 2, 3500, "r3", 2);
            r3.SetCriteria( Model.SearchCriteria.TV );

            Room r4 = new Room(Guid.NewGuid(), 1, 2, 3200, "r4", 2);
            r4.SetCriteria( Model.SearchCriteria.WiFi );
            r4.SetCriteria( Model.SearchCriteria.TV );

            Room r5 = new Room(Guid.NewGuid(), 1, 2, 10000, "r5", 2);
            r5.SetCriteria( Model.SearchCriteria.Freedge );
            r5.SetCriteria( Model.SearchCriteria.Vault );

            Room r6 = new Room(Guid.NewGuid(), 1, 2, 200, "r6", 2);

            IQueryable<Room> rList = (new List<Room> { r1, r2, r3, r4, r5, r6 }).AsQueryable();

            roomRep.LoadAll().Returns( rList );

            RoomService rServ = new RoomService( roomRep, hallRep );

            Assert.IsTrue( rServ.GetRooms(  Model.SearchCriteria.Freedge ).Count == 3 );
            Assert.IsTrue( rServ.GetRooms( Model.SearchCriteria.TV ).Count == 2 );
            Assert.IsTrue( rServ.GetRooms( Model.SearchCriteria.WiFi ).Count == 1 );
            Assert.IsTrue( rServ.GetRooms( Model.SearchCriteria.Vault ).Count == 1 );
        }

        [TestMethod]
        public void GetRooms3 ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "r1", 2);
            r1.SetCriteria( Model.SearchCriteria.Freedge );
            r1.SetCriteria( Model.SearchCriteria.TV );

            Room r2 = new Room(Guid.NewGuid(), 1, 2, 3000, "r2", 2);
            r2.SetCriteria( Model.SearchCriteria.Freedge );

            Room r3 = new Room(Guid.NewGuid(), 1, 2, 3500, "r3", 2);
            r3.SetCriteria( Model.SearchCriteria.TV );

            Room r4 = new Room(Guid.NewGuid(), 1, 2, 3200, "r4", 2);
            r4.SetCriteria( Model.SearchCriteria.WiFi );
            r4.SetCriteria( Model.SearchCriteria.TV );

            Room r5 = new Room(Guid.NewGuid(), 1, 2, 10000, "r5", 2);
            r5.SetCriteria( Model.SearchCriteria.Freedge );
            r5.SetCriteria( Model.SearchCriteria.Vault );
            r5.SetCriteria( Model.SearchCriteria.WiFi );

            Room r6 = new Room(Guid.NewGuid(), 1, 2, 200, "r6", 2);

            IQueryable<Room> rList = (new List<Room> { r1, r2, r3, r4, r5, r6 }).AsQueryable();

            roomRep.LoadAll().Returns( rList );

            RoomService rServ = new RoomService( roomRep, hallRep );

            Assert.IsTrue( rServ.GetRooms( r4.SearchCriterias ).Count == 5 );
            Assert.IsTrue( rServ.GetRooms( 0x00 ).Count == 1 );

            int sCrit = 0x00;
            Hotel.Model.Utils.SetCriteria( ref sCrit, Model.SearchCriteria.TV );

            Assert.IsTrue( rServ.GetRooms( sCrit ).Count == 3 );
        }

        [TestMethod]
        public void GetFreeRooms ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "r1", 2);
            r1.Bookings.Add( new BookingPeriod( Guid.NewGuid(), 
                new Utils.BookingDate( 1, 01, 2017 ), 
                new Utils.BookingDate( 5, 01, 2017 ) ) );
            r1.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
                new Utils.BookingDate( 6, 01, 2017 ),
                new Utils.BookingDate( 10, 01, 2017 ) ) );

            Room r2 = new Room(Guid.NewGuid(), 1, 2, 3000, "r2", 2);
            r2.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
               new Utils.BookingDate( 1, 01, 2017 ),
               new Utils.BookingDate( 5, 01, 2017 ) ) );

            Room r3 = new Room(Guid.NewGuid(), 1, 2, 3500, "r3", 2);
            r3.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
                new Utils.BookingDate( 11, 01, 2017 ),
                new Utils.BookingDate( 20, 01, 2017 ) ) );
            Room r4 = new Room(Guid.NewGuid(), 1, 2, 3200, "r4", 2);
            Room r5 = new Room(Guid.NewGuid(), 1, 2, 10000, "r5", 2);
            r5.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
                new Utils.BookingDate( 1, 01, 2017 ),
                new Utils.BookingDate( 3, 01, 2017 ) ) );
            r5.Bookings.Add( new BookingPeriod( Guid.NewGuid(),
                new Utils.BookingDate( 17, 01, 2017 ),
                new Utils.BookingDate( 22, 01, 2017 ) ) );
           

            IQueryable<Room> rList = (new List<Room> { r1, r2, r3, r4, r5 }).AsQueryable();

            roomRep.LoadAll().Returns( rList );

            RoomService rServ = new RoomService(roomRep, hallRep);

            Assert.IsTrue( rServ.GetFreeRooms( new Dto.PeriodDto( Guid.NewGuid(), 1, 1, 2017, 10, 1, 2017 ) ).Count == 2 );
            Assert.IsTrue( rServ.GetFreeRooms( new Dto.PeriodDto( Guid.NewGuid(), 15, 1, 2017, 20, 1, 2017 ) ).Count == 4 );

        }

        [TestMethod]
        public void HasRoomWithNumberTest ()
        {
            var roomRep =  Substitute.For<IRoomRepository>();
            var hallRep =  Substitute.For<IHallRepository>();

            Room r1 = new Room(Guid.NewGuid(), 1, 2, 2000, "r1", 2);
          
           // IQueryable<Room> rList = (new List<Room> { r1, r2, r3, r4, r5, r6 }).AsQueryable();

            roomRep.Find( r1.Number ).Returns( r1 );

            RoomService rServ = new RoomService( roomRep, hallRep );

            Assert.IsTrue( rServ.HasRoomWithNumber( r1.Number ) );
        }
    }
}