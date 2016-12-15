using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Hotel.Model.Entities.Abstract;
using Hotel.Model.Entities.Concrete;


namespace Hotel.Repository
{
    public interface IPlaceRepository<T> : IRepository<T>
        where T : Place
        
    {
        T Find ( int number );

        T Find ( BookingPeriod bp );

        //bool IsFree ( Room r, BookingPeriod bp );
    }
}
