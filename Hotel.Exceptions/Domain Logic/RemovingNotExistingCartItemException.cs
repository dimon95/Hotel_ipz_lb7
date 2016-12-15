/* (C) 2014-2016, Sergei Zaychenko, NURE, Kharkiv, Ukraine */

using System;

namespace Hotel.Exceptions
{
    public class RemovingNotExistingCartItemException : DomainLogicException
    {
        public RemovingNotExistingCartItemException ( Guid itemId, Guid cartId )
            : base( 
                  string.Format( 
                      "Attempt to remove item #{0} wich cart #{1} doesn't have.",
                      itemId, cartId
                  )
             )
        { }
    }
}
