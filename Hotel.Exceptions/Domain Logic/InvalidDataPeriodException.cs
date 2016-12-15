using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Exceptions
{
    public class InvalidDataPeriodException : DomainLogicException
    {
        public InvalidDataPeriodException ()

            : base( string.Format( "Invalid date period." ) )
        { }
    }
}
