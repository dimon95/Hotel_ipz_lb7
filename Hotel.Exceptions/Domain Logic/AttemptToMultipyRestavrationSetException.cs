/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class AttemptToMultipyRestavrationSetException : DomainLogicException
    {
        public AttemptToMultipyRestavrationSetException ( Guid placeId )
            :   base( string.Format( "Attempted to set on restavration place which is alredy restavrated #{0}", placeId ) )
        { }
    }
}
