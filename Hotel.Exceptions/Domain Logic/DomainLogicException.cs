/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class DomainLogicException : Exception
    {
        public DomainLogicException ( string message )
            :   base( message )
        {}
    }
}
