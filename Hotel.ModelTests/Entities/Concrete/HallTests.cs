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
    public class HallTests
    {
        [TestMethod()]
        public void HallTest ()
        {
            int number = 1;
            int personCount = 2;
            decimal price = 2000;
            string description = "dfgdfg";
            Hall r = new Hall(Guid.NewGuid(), number, personCount, price, description);

            Assert.AreEqual( number, r.Number );
            Assert.AreEqual( personCount, r.PersonsCount );
            Assert.AreEqual( price, r.Price );
            Assert.AreEqual( description, r.Description );
        }
    }
}