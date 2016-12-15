using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model.Entities.Concrete
{
    public class Hall : Abstract.Place
    {
        protected Hall () { }

        public Hall ( Guid id, int number, int personsCount, decimal price, string description )
            : base( id, number, personsCount, price, description )
        { }
    }
}
