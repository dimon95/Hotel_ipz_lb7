/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class ServiceValidationException : Exception
    {
        public ServiceValidationException ( string message )
            : base( message )
        { }
    }
}
