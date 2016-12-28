using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Repository;

using Hotel.Model.Entities.Concrete;
using Hotel.Utils;
using Hotel.Model.Entities.Abstract;
using NSubstitute;
using Hotel.Dto;

namespace Hotel.Services.Impl.Tests
{
    [TestClass()]
    public class AccountServiceTests
    {
       
        [TestMethod()]
        public void ChangeEmailTest ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            var aRepo = Substitute.For<IAccountRepository>();

            Account cl = new Client( Guid.NewGuid(), "Vasya", "Pupkin", "", 
                "pupkin@example.com", hashProvider.GetHashCode( "1111" ), new DateOfBirth( 01, 01, 1990 ));

            aRepo.Find( "pupkin@example.com" ).Returns(cl);
            aRepo.Load( cl.Id ).Returns( cl );

            AccountService ser = new AccountService(aRepo);

            Assert.AreEqual( cl.Email, "pupkin@example.com" );

            ser.ChangeEmail( cl.Id, "newmail@example.com" );

            Assert.AreEqual( cl.Email, "newmail@example.com" );
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeNameTest ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            var aRepo = Substitute.For<IAccountRepository>();

            Account cl = new Client( Guid.NewGuid(), "Vasya", "Pupkin", "",
                "pupkin@example.com", hashProvider.GetHashCode( "1111" ), new DateOfBirth( 01, 01, 1990 ));

            aRepo.Find( "pupkin@example.com" ).Returns( cl );
            aRepo.Load( cl.Id ).Returns( cl );

            AccountService ser = new AccountService(aRepo);

            ser.ChangeName( cl.Id, "Ivan", "Ivanov", "Ivanovich" );

            Assert.AreEqual( cl.Name, "Ivan" );
            Assert.AreEqual( cl.Middlename, "Ivanovich" );
            Assert.AreEqual(cl.Surname, "Ivanov");

            ser.ChangeName( cl.Id, "", "", "" );
        }

        [TestMethod()]
        [ExpectedException( typeof( ArgumentException ) )]
        public void ChangePasswordTest ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            var aRepo = Substitute.For<IAccountRepository>();

            Account cl = new Client( Guid.NewGuid(), "Vasya", "Pupkin", "",
                "pupkin@example.com", hashProvider.GetHashCode( "1111" ), new DateOfBirth( 01, 01, 1990 ));

            aRepo.Find( "pupkin@example.com" ).Returns( cl );
            aRepo.Load( cl.Id ).Returns( cl );

            AccountService ser = new AccountService(aRepo);

            ser.ChangePassword( cl.Id, hashProvider.GetHashCode( "1111" ), hashProvider.GetHashCode( "0000" ) );

            Assert.AreEqual(cl.PasswordHash, hashProvider.GetHashCode( "0000" ) );

            ser.ChangePassword( cl.Id, hashProvider.GetHashCode( "1111" ), hashProvider.GetHashCode( "1111" ) );
        }

        [TestMethod()]
        public void CreateAdminTest ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            AccountService ser = new AccountService(new ImplTests.TestRepos.AccountRepo());

            Assert.IsTrue( ser.ViewAll().Count == 0 );

            Guid aId = ser.CreateAdmin( "Vasya", "Pupkin", "", new DateOfBirth( 20, 10, 1990 ),
                "pupkin@examplemail.com", hashProvider.GetHashCode( "1111" ) );

            Assert.IsTrue( ser.ViewAll().Count == 1 );

            AccountDto ad = ser.Indentify( hashProvider.GetHashCode( "1111" ), "pupkin@examplemail.com");

            Assert.IsTrue( ad.Role == Role.Admin );
            Assert.AreEqual( ad.Name, "Vasya" );
            Assert.AreEqual( ad.Surname, "Pupkin" );
            Assert.AreEqual( ad.Day, 20 );
            Assert.AreEqual( ad.Month, 10 );
            Assert.AreEqual( ad.Year, 1990 );
        }

        [TestMethod()]
        public void CreateClientTest ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            AccountService ser = new AccountService(new ImplTests.TestRepos.AccountRepo());

            Assert.IsTrue( ser.ViewAll().Count == 0 );

            Guid cId = ser.CreateClient( "Vasya", "Pupkin", "", new DateOfBirth( 20, 10, 1990 ),
                "pupkin@examplemail.com", hashProvider.GetHashCode( "1111" ) );

            Assert.IsTrue( ser.ViewAll().Count == 1 );

            AccountDto ad = ser.Indentify( hashProvider.GetHashCode( "1111" ), "pupkin@examplemail.com");

            Assert.IsTrue( ad.Role == Role.Client );
            Assert.AreEqual( ad.Name, "Vasya" );
            Assert.AreEqual( ad.Surname, "Pupkin" );
            Assert.AreEqual( ad.Day, 20 );
            Assert.AreEqual( ad.Month, 10 );
            Assert.AreEqual( ad.Year, 1990 );
        }

        [TestMethod()]
        public void GetCartContentTest ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            AccountService ser = new AccountService(new ImplTests.TestRepos.AccountRepo());

            Assert.IsTrue( ser.ViewAll().Count == 0 );

            Guid cId = ser.CreateClient( "Vasya", "Pupkin", "", new DateOfBirth( 20, 10, 1990 ),
                "pupkin@examplemail.com", hashProvider.GetHashCode( "1111" ) );

            BookingHolderDto cartDto = ser.GetCartContent( cId );

            Assert.IsTrue( cartDto.Bookings.Count == 0 );
        }

        [TestMethod()]
        public void GetHistoryContentTest ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            AccountService ser = new AccountService(new ImplTests.TestRepos.AccountRepo());

            Assert.IsTrue( ser.ViewAll().Count == 0 );

            Guid cId = ser.CreateClient( "Vasya", "Pupkin", "", new DateOfBirth( 20, 10, 1990 ),
                "pupkin@examplemail.com", hashProvider.GetHashCode( "1111" ) );

            BookingHolderDto histDto = ser.GetHistoryContent( cId );

            Assert.IsTrue( histDto.Bookings.Count == 0 );
        }

        [TestMethod]
        public void ViewTest ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            AccountService ser = new AccountService(new ImplTests.TestRepos.AccountRepo());

            Assert.IsTrue( ser.ViewAll().Count == 0 );

            Guid cId = ser.CreateClient( "Vasya", "Pupkin", "", new DateOfBirth( 20, 10, 1990 ),
                "pupkin@examplemail.com", hashProvider.GetHashCode( "1111" ) );

            Guid cId2 = ser.CreateClient( "Ivan", "Ivanov", "", new DateOfBirth( 20, 10, 1990 ),
                "ivanov@examplemail.com", hashProvider.GetHashCode( "1111" ) );

            Assert.AreEqual( ser.View( cId ).Id, cId );
            Assert.AreEqual( ser.View( cId2 ).Id, cId2 );
        }

        [TestMethod]
        public void ViewAllTest ()
        {
            DefaultStringHashProvider hashProvider = new DefaultStringHashProvider();

            AccountService ser = new AccountService(new ImplTests.TestRepos.AccountRepo());

            Assert.IsTrue( ser.ViewAll().Count == 0 );

            Guid cId = ser.CreateClient( "Vasya", "Pupkin", "", new DateOfBirth( 20, 10, 1990 ),
                "pupkin@examplemail.com", hashProvider.GetHashCode( "1111" ) );

            Guid cId2 = ser.CreateClient( "Ivan", "Ivanov", "", new DateOfBirth( 20, 10, 1990 ),
                "ivanov@examplemail.com", hashProvider.GetHashCode( "1111" ) );

            Assert.IsTrue( ser.ViewAll().Count == 2 );
        }
    }
}