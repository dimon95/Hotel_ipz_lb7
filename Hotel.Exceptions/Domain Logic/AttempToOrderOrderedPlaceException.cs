/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class AttempToOrderOrderedPlaceException : DomainLogicException
    {
        public AttempToOrderOrderedPlaceException ( Guid placeId )

            : base( string.Format( "Attempted to order already ordered place #{0}", placeId ) )
        { }
    }
}
