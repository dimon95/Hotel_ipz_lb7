using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;
using Hotel.Utils.Validators;
using Hotel.Services.Validators;

namespace Hotel.Services
{
    public interface IHallService : IPlaceService<HallDto>
    {
        Guid CreateHall (
            [PlaceNumberValidator] int number, 
            string description,
            [PositiveNumberValidator] int personsCount, 
            [PriceValidator] decimal price );

        IList<HallDto> GetFreeHalls ( PeriodDto period );

        IList<HallDto> GetHalls ();

        bool HasHallWithNumber ( [PlaceNumberValidator] int number );
    }
}
