using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;
using Hotel.Utils.Validators;
using Hotel.Services.Validators;
using Hotel.Model.Entities.Concrete;
using Hotel.Model;


namespace Hotel.Services
{
    public interface IRoomService : IPlaceService<RoomDto>
    {
        Guid CreateRoom (
            [PlaceNumberValidator] int number,
            string description,
            [PersonsCountValidator] int personsCount,
            [BedCountValidator] int bedCount,
            [PriceValidator] decimal price);

        void SetCriteria ( Guid roomId, SearchCriteria criteria );

        void ResetCriteria ( Guid roomId, SearchCriteria criteria );

        IList<RoomDto> GetRooms ( SearchCriteria criteria );

        IList<RoomDto> GetRooms ();

        IList<RoomDto> GetFreeRooms ( PeriodDto period );

        IList<RoomDto> GetRooms ( [PriceValidator] decimal bPrice, [PriceValidator] decimal ePrice );

        IList<RoomDto> GetRooms ( int sCriteria );

        bool HasRoomWithNumber ( [PlaceNumberValidator] int number );
    }
}
