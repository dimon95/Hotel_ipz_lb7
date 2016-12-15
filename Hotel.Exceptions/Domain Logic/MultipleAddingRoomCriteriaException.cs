/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class MultipleAddingRoomCriteriaException : DomainLogicException
    {
        public MultipleAddingRoomCriteriaException ( Guid roomId )
            : base( string.Format( "Attempted to add criteria room already has#{0}", roomId ) )
        { }
    }
}
