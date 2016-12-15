/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class AttemptToMultipyRestavrationResetException : DomainLogicException
    {
        public AttemptToMultipyRestavrationResetException ( Guid placeId )
            : base( string.Format( "Attempted to reset restavration place which is not restavrated #{0}", placeId ) )
        { }
    }
}
