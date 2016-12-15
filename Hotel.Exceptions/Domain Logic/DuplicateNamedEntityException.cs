/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class DuplicateNamedEntityException : DomainLogicException
    {
        public DuplicateNamedEntityException ( Type t, string name )
            :   base( string.Format( "Duplicate {0} object called \"{1}\"", t.Name, name ) )
        {}
    }
}
