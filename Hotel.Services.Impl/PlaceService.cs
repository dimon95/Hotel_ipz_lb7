using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Dto;
using Hotel.Model.Entities.Concrete;
using Hotel.Model.Entities.Abstract;
using Hotel.Exceptions;
using Hotel.Repository;

namespace Hotel.Services.Impl
{
    public abstract class PlaceService 
    {
        protected IRoomRepository RoomRepository { get; private set; }
        protected IHallRepository HallRepository { get; private set; }

        protected PlaceService ( IRoomRepository rRepo, IHallRepository hRepo )
        {
            RoomRepository = rRepo;
            HallRepository = hRepo;
        }

        public virtual void ChangeDescription ( Guid placeId, string description )
        {            
            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            p.Description = description;
        }

        public virtual void ChangePrice ( Guid placeId, decimal price )
        {
            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            p.Price = price;
        }

        public IList<PeriodDto> GetBookedPeriodsFor ( Guid placeId )
        {
            IList<PeriodDto> res = new List<PeriodDto>();

            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            foreach ( BookingPeriod bp in p.Bookings )
            {
                res.Add( bp.toDto() );
            }

            return res;
        }

        public virtual void ResetPlaceFromRestavration ( Guid placeId )
        {
            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            if ( !p.OnRestavretion )
                throw new AttemptToMultipyRestavrationResetException( placeId );

            p.ResetFromRestavretion();
        }

        public virtual void SetPlaceOnRestavration ( Guid placeId )
        {
            Place p = ServiceUtils.GetPlace(RoomRepository, HallRepository, placeId);

            if ( p.OnRestavretion )
                throw new AttemptToMultipyRestavrationSetException(placeId);

            p.SetOnRestavretion();
        }

        public void Update ( Guid id, string description, decimal price )
        {
            ChangeDescription( id, description );
            ChangePrice( id, price );
        }

        /*public PlaceDto View ( Guid id )
        {
            return 
        }

        public IList<Guid> ViewAll ()
        {
            throw new NotImplementedException();
        }*/
    }
}
