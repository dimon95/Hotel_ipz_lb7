/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class ApplicationFatalException : Exception
    {
        public ApplicationFatalException ( string message )
            : base( message )
        { }
    }
}
