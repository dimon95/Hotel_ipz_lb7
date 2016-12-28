using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Utils
{
    public class Date:IComparable
    {
        //private static byte[] _daysInMonths = new byte[] { 29, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        private DateTime _date;


        public int Day
        {
            get { return _date.Day; }
        }

        public int Month
        {
            get { return _date.Month; }
        }

        public int Year
        {
            get { return _date.Year; }
        }


        protected Date () { }

        public Date ( int day, int month, int year )
        {
            _date = new DateTime( year, month, day );
        }

        public Date ( string day, string month, string year )
            :this(Convert.ToInt32( day), Convert.ToInt32( month), Convert.ToInt32(year))
        {
                        
        }

        public Date ( string date )
        {
            string[] tokens = date.Split(new char[] { '.' });

            if ( tokens.Count() != 3 )
                throw new ArgumentException();

            _date = new DateTime( Convert.ToInt32( tokens [ 0 ] ),
                                  Convert.ToInt32( tokens [ 1 ] ),
                                  Convert.ToInt32( tokens [ 2 ] ) );

            
        }

        public Date ( DateTime dt )
        {
            _date = dt;
        }


        public bool IsLeapYear ()
        {
            return DateTime.IsLeapYear(Year);
        }


        public Date AddDays ( int days )
        {
            return new Date( _date.AddDays( days ) );
        }

        public uint CountDays ( Date other )
        {
            return ( uint ) Math.Abs( ( _date - other._date ).Days );
        }

        public static bool operator == ( Date first, Date second )
        {
            if ( object.ReferenceEquals( first, null ) || object.ReferenceEquals( first, null ) )
                return false;

            return first.Day == second.Day && first.Month == second.Month && first.Year == second.Year;
        }

        public static bool operator != ( Date first, Date second )
        {
            return !( first == second );
        }

        public static bool operator > ( Date first, Date second )
        {
            if ( first.Year > second.Year )
                return true;
            if ( first.Year == second.Year && first.Month > second.Month )
                return true;
            if ( first.Year == second.Year && first.Month == second.Month && first.Day > second.Day )
                return true;

            return false;
        }

        public static bool operator >= ( Date first, Date second )
        {
            return first > second || first == second;
        }

        public static bool operator < ( Date first, Date second )
        {
            return !( first >= second );
        }

        public static bool operator <= ( Date first, Date second )
        {
            return !( first > second );
        }


        public override bool Equals ( object obj )
        {
            if ( obj == null || !( obj is Date ) )
                return false;

            return this == obj as Date;
        }

        public override int GetHashCode ()
        {
            return base.GetHashCode();
        }


        public override string ToString ()
        {
            return  ((Day < 10) ? "0": "") + Day.ToString() + "."
                + ((Month < 10) ? "0" : "") + Month.ToString() + "." + Year.ToString();
        }


        public static Date GetToday ()
        {
            return new Date( DateTime.Now );
        }


        public int CompareTo ( Date date )
        {
            if ( object.ReferenceEquals( date, null ) ) return 1;

            if ( this > date )
                return 1;
            if ( this < date )
                return -1;
            return 0;
        }

        public int CompareTo ( object obj )
        {
            if ( obj == null ) return 1;
            Date otherDate = obj as Date;
            if (! object.ReferenceEquals( otherDate, null ))
                return this.CompareTo( otherDate );
            throw new ArgumentException( "Object is not date" );
        }
    }

    public class BookingDate : Date
    {
        private static byte _minLimit = 0;
        private static byte _maxLimit = 200;

        private bool IsValid ( )
        {
            if ( GetToday().AddDays( _minLimit ) > this )
                return false;

            if ( GetToday().AddDays( _maxLimit ) < this )
                return false;

            return true;
        }

        protected BookingDate () { }

        public BookingDate ( int day, int month, int year )
            :base(day, month, year)
        {
            if ( !IsValid() )
                throw new ArgumentException( "Wrong range" );
        }

        public BookingDate ( string day, string month, string year )
            :base (day, month, year)
        {
            if ( !IsValid() )
                throw new ArgumentException("Wrong range");
        }

        public BookingDate ( string date )
            : base( date )
        {
            if ( !IsValid() )
                throw new ArgumentException( "Wrong range" );
        }

        public BookingDate ( Date d )
            : base( d.Day, d.Month, d.Year )
        {
            if ( !IsValid() )
                throw new ArgumentException("Wrong range");
        }

        public BookingDate ( DateTime dt )
            : base( dt )
        {
            if ( !IsValid() )
                throw new ArgumentException( "Wrong range" );
        }

        public static Date GetMax ()
        {
            return GetToday().AddDays( _maxLimit );
        }
    }

    public class DateOfBirth : Date
    {
        private static Date _minDateOfBirth = new Date(1,1,1900);

        private bool IsEighteeng ()
        {
            Date now = GetToday();

            if ( now.Year - Year > 18 )
                return true;
            else
            if ( now.Year - Year == 18 && now.Month > Month )
                return true;
            else
            if ( now.Year - Year == 18 && now.Month == Month && now.Day >= Day )
                return true;

            return false;
        }

        private bool IsValid ()
        {
            return IsEighteeng() && this >= _minDateOfBirth;
        }

        protected DateOfBirth () { }

        public DateOfBirth ( int date, int month, int year )
            : base( date, month, year )
        {
            if ( !IsValid() )
                throw new ArgumentException("Wrong range");
        }

        public DateOfBirth ( string day, string month, string year )
            :base (day, month, year)
        {
            if ( !IsValid() )
                throw new ArgumentException( "Wrong range" );
        }

        public DateOfBirth ( string date )
            : base( date )
        {
            if ( !IsValid() )
                throw new ArgumentException( "Wrong range" );
        }

        public DateOfBirth ( DateTime dt )
            : base( dt )
        {
            if ( !IsValid() )
                throw new ArgumentException( "Wrong range" );
        }

        public static Date GetMinDateOfBirth ()
        {
            return _minDateOfBirth;
        }

        public static Date GetMinEighteenDate ()
        {
            DateTime eighteenDate =  DateTime.Today.AddYears( -18 );
            return new Date((byte) eighteenDate.Day, ( byte ) eighteenDate.Month, eighteenDate.Year );
        }
    }
}
