using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Utils;

namespace Hotel.Model.Entities.Concrete
{
    public class BookingPeriod : Entity
    {
        public BookingDate Begin { get; set; }
        public BookingDate End { get; set; }

        protected BookingPeriod () { }

        public BookingPeriod ( Guid id, BookingDate begin, BookingDate end )
            :base(id)
        {
            if ( !( begin < end ) )
                throw new ArgumentException();

            Begin = begin;
            End = end;
        }

        public bool Intersect ( BookingPeriod other )
        {
            if ( other.Begin <= Begin && other.End >= Begin )
                return true;

            if ( Begin <= other.Begin && End >= other.End )
                return true;

            return false;
        }

        public uint CountDays ()
        {
            return Begin.CountDays( End );
        }

        public override string ToString ()
        {
            return Begin.ToString() + " - " + End.ToString() + "\r\n";
        }
    }
}
