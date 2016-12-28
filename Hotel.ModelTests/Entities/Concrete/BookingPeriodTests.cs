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
    public class BookingPeriodTests
    {
        [TestMethod()]
        public void BookingPeriodTest ()
        {
            BookingDate begDate = new BookingDate(31,12,2016);
            BookingDate endDate = new BookingDate(05,01,2017);
            BookingPeriod bp =  new BookingPeriod(Guid.NewGuid(),begDate , endDate);

            Assert.AreEqual( begDate, bp.Begin );
            Assert.AreEqual( endDate, bp.End );
        }

        [TestMethod()]
        public void IntersectTest ()
        {
            BookingDate begDate = new BookingDate(31,12,2016);
            BookingDate endDate = new BookingDate(05,01,2017);
            BookingPeriod bp =  new BookingPeriod(Guid.NewGuid(),begDate , endDate);

            BookingDate begDate2 = new BookingDate(31,12,2016);
            BookingDate endDate2 = new BookingDate(05,01,2017);
            BookingPeriod bp2 =  new BookingPeriod(Guid.NewGuid(), begDate2, endDate2);

            BookingDate begDate3 = new BookingDate(31,12,2016);
            BookingDate endDate3 = new BookingDate(06,01,2017);
            BookingPeriod bp3 =  new BookingPeriod(Guid.NewGuid(), begDate3, endDate3);

            BookingDate begDate4 = new BookingDate(28,12,2016);
            BookingDate endDate4 = new BookingDate(04,01,2017);
            BookingPeriod bp4 =  new BookingPeriod(Guid.NewGuid(), begDate4, endDate4);

            BookingDate begDate5 = new BookingDate(08,01,2017);
            BookingDate endDate5 = new BookingDate(16,01,2017);
            BookingPeriod bp5 =  new BookingPeriod(Guid.NewGuid(), begDate5, endDate5);

            Assert.IsTrue( bp.Intersect( bp2 ) );
            Assert.IsTrue( bp.Intersect( bp3 ) );
            Assert.IsTrue( bp.Intersect( bp4 ) );
            Assert.IsFalse( bp.Intersect( bp5 ) );


        }

        [TestMethod()]
        public void CountDaysTest ()
        {
            BookingDate begDate = new BookingDate(31,12,2016);
            BookingDate endDate = new BookingDate(05,01,2017);
            BookingPeriod bp =  new BookingPeriod(Guid.NewGuid(),begDate , endDate);

            Assert.IsTrue( bp.CountDays() == Math.Abs(new DateTime(2016, 12,31).Subtract(new DateTime( 2017, 01, 05 ) ).Days));
        }
    }
}