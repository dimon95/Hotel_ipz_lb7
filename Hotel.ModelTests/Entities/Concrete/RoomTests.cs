using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model.Entities.Concrete.Tests
{
    [TestClass()]
    public class RoomTests
    {
        [TestMethod()]
        public void RoomTest ()
        {
            int number = 1;
            int personCount = 2;
            decimal price = 2000;
            int bedCount = 2;
            string description = "dfgdfg";
            Room r = new Room(Guid.NewGuid(), number, personCount, price, description, bedCount);

            Assert.AreEqual( number, r.Number );
            Assert.AreEqual( personCount, r.PersonsCount );
            Assert.AreEqual( price, r.Price );
            Assert.AreEqual( bedCount, r.BedCount );
            Assert.AreEqual( description, r.Description );

            Assert.AreEqual( r.SearchCriterias, 0x00 );
        }

        [TestMethod()]
        public void SetCriteriaTest ()
        {
            int number = 1;
            int personCount = 2;
            decimal price = 2000;
            int bedCount = 2;
            string description = "dfgdfg";
            Room r = new Room(Guid.NewGuid(), number, personCount, price, description, bedCount);

            r.SetCriteria( SearchCriteria.TV );
            r.SetCriteria( SearchCriteria.Vault );

            Assert.IsTrue( r.HasCriteria( SearchCriteria.TV ) && r.HasCriteria( SearchCriteria.Vault ) );
            Assert.IsFalse( r.HasCriteria( SearchCriteria.Freedge ) || r.HasCriteria( SearchCriteria.WiFi ) );
        }

        [TestMethod()]
        public void ResetCriteriaTest ()
        {
            int number = 1;
            int personCount = 2;
            decimal price = 2000;
            int bedCount = 2;
            string description = "dfgdfg";
            Room r = new Room(Guid.NewGuid(), number, personCount, price, description, bedCount);
            r.SetCriteria( SearchCriteria.TV );
            Assert.IsTrue( r.HasCriteria( SearchCriteria.TV ) );
            r.ResetCriteria( SearchCriteria.TV );
            Assert.IsFalse( r.HasCriteria( SearchCriteria.TV ) );
        }
    }
}