/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class ServiceUnresolvedEntityException : ServiceValidationException
    {
        public ServiceUnresolvedEntityException ( Type entityType, Guid entityId )
            :   base( 
                    string.Format( 
                        "Unresolved entity #{1} of type {0}", 
                        entityType.ToString(), entityId 
                    )
                )
        {}
    }
}
