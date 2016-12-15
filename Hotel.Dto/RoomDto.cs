using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Dto
{
    public class RoomDto : PlaceDto
    {
        public int BedCount { get; private set; }

        //public IDictionary<string, bool> SearchCriterias { get; private set; }

        public int SearchCriterias { get; private set; }

        public RoomDto ( Guid id, int number, decimal price, string description, int personsCount, bool onRestavration,
            int bedCount, int searchCriterias, IList<PeriodDto> bookings)
            : base( id, number, price, description, personsCount, onRestavration, bookings )
        {
            BedCount = bedCount;

            SearchCriterias = searchCriterias;
        }

        public override string ToString ()
        {
            string res = base.ToString();

            foreach ( Model.SearchCriteria val in Enum.GetValues(typeof(Model.SearchCriteria)) )
            {
                res += Model.Utils.HasCriteria(SearchCriterias, val) ? "Has " : "Doesn't have ";
                res += val.ToString() + "\r\n"; 
            }

            res += "\r\n\r\n";

            return res;
        }
    }
}
