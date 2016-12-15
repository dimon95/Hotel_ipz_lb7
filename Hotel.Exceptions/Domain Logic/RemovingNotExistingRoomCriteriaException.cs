/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class RemovingNotExistingRoomCriteriaException : DomainLogicException
    {
        public RemovingNotExistingRoomCriteriaException ( Guid roomId )
            : base( string.Format(
                    "Room #{0} doesn't have such criteria. It can not be removed.",
                    roomId
                ) )
        { }
    }
}
