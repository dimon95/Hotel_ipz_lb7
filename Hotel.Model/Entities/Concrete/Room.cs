using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model;

namespace Hotel.Model.Entities.Concrete
{
    public class Room : Abstract.Place
    {
        public int SearchCriterias { get; set; }

        public int BedCount { get; set; }

        protected Room () { }

        public Room ( Guid id, int number, int personsCount, decimal price, string description, int bedCount )
            : base( id, number, personsCount, price, description )
        {
            BedCount = bedCount;

            SearchCriterias = 0x00;
        }

        public void SetCriteria ( SearchCriteria criteria )
        {
            int tmp = SearchCriterias;

            Utils.SetCriteria( ref tmp, criteria );

            SearchCriterias = tmp;
        }

        public void ResetCriteria ( SearchCriteria criteria )
        {
            int tmp = SearchCriterias;

            Utils.ResetCriteria( ref tmp, criteria );

            SearchCriterias = tmp;

        }

        public bool HasCriteria ( SearchCriteria sc )
        {
            return Utils.HasCriteria( SearchCriterias, sc );
        }

    }
}
