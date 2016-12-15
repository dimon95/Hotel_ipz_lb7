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
    public interface IPlaceService<T> : IDomainEntityService<T>
        where T : PlaceDto
        
    {
        void DeletePlace ( Guid id );

        IList<PeriodDto> GetBookedPeriodsFor ( Guid placeId );

        void SetPlaceOnRestavration ( Guid placeId );

        void ResetPlaceFromRestavration ( Guid placeId );

        void ChangeDescription ( Guid placeId, string description );

        void ChangePrice ( 
            Guid placeId, 
            [PriceValidator] decimal price );

        void Update ( Guid id, string description, [PriceValidator] decimal price );
    }
}
