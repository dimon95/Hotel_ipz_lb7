using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Exceptions
{
    public class BedsOutOfRange : DomainLogicException
    {
        public BedsOutOfRange (  )

            : base( string.Format( "Single room can't hold two beds" ) )
        { }
    }
}
