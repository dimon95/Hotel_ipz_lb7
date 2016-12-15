using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Utils
{
    public class Date:IComparable
    {
        private static byte[] _daysInMonths = new byte[] { 29, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public byte Day { get; set; }
        public byte Month { get; set; }
        public int Year { get; set; }

        private bool IsValid (  )
        {
            if ( Day > 31 || Month > 12 )
                return false;

            if ( ( Month == 4 || Month == 6 || Month == 9 || Month == 11 ) && Day > 30 )
                return false;

            if ( IsLeapYear() && Month == 2 && Day > 29 )
                return false;

            if ( !IsLeapYear() && Month == 2 && Day > 28 )
                return false;

            return true;
        }

        protected Date () { }

        public Date ( byte day, byte month, int year )
        {
            Day = day;
            Month = month;
            Year = year;

            if ( !IsValid() )
                throw new ArgumentException( "Invalid date" );
        }

        public Date ( string day, string month, string year )
            :this(Convert.ToByte(day), Convert.ToByte(month), Convert.ToInt32(year))
        {
                        
        }

        public Date ( string date )
        {
            string[] tokens = date.Split(new char[] { '.' });

            if ( tokens.Count() != 3 )
                throw new ArgumentException();

            Day = Convert.ToByte( tokens [ 0 ] );
            Month = Convert.ToByte( tokens [ 1 ] );
            Year = Convert.ToInt32( tokens [ 2 ] );

            if ( !IsValid() )
                throw new ArgumentException("Invalid date");
        }

        public bool IsLeapYear ()
        {
            return ( ( Year % 4 == 0 && Year % 100 != 0 ) || ( Year % 400 == 0 ) );
        }

        public int GetDaysInMonth ()
        {
            return _daysInMonths [ ( IsLeapYear() && Month == 2 ? 0 : Month ) ];
        }

        public Date AddDays ( int days )
        {
            int tmp = days;

            Date newDate = new Date(Day, Month, Year);

            while ( (tmp + Day) > newDate.GetDaysInMonth() )
            {
                tmp -= newDate.GetDaysInMonth() - newDate.Day + 1;

                if ( newDate.Month == 0 )
                    newDate.Month = 3;
                else
                    ++newDate.Month;

                newDate.Day = 1;

                if ( newDate.Month > 12 )
                {
                    ++newDate.Year;
                    newDate.Month = 1;
                }

                if ( newDate.Month == 2 && newDate.IsLeapYear() )
                    newDate.Month = 0;
            }

            newDate.Day += (byte)tmp;

            return newDate;
        }

        public uint CountDays ( Date other )
        {
            if ( this > other )
                throw new ArgumentException();

            if ( this == other )
                return 0;

            byte m = Month;
            int y = Year;

            uint res = 0;

            while ( y <= other.Year )
            {
                while ( m != other.Month )
                {
                    res += _daysInMonths [ m ];

                    if ( m == 0 )
                        m = 3;

                    if ( m == 1 && ( y % 4 == 0 && y % 100 != 0 ) || ( y % 400 == 0 ))
                        m = 0;

                    if ( m == 12 )
                        m = 1;

                    m++;
                }

                if ( y < other.Year )
                {
                    res += 365;

                    if ( ( y % 4 == 0 && y % 100 != 0 ) || ( y % 400 == 0 ) )
                        res += 1;
                }
                y++;
            }

            res += (uint)(_daysInMonths [ Month ] - Day + _daysInMonths [ other.Month ] - other.Day);

            return res;
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
            return new Date((byte)DateTime.Today.Day, (byte)DateTime.Today.Month, DateTime.Today.Year);
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

        protected BookingDate () { }

        private bool IsValid ( )
        {
            if ( GetToday().AddDays( _minLimit ) > this )
                return false;

            if ( GetToday().AddDays( _maxLimit ) < this )
                return false;

            return true;
        }

        public BookingDate ( byte day, byte month, int year )
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

        public DateOfBirth ( byte date, byte month, int year )
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
