using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;
using Hotel.Model.Entities.Concrete;
using Hotel.Exceptions;
using Hotel.Repository;

namespace Hotel.Services.Impl
{
    public class HallService : PlaceService, IHallService
    {
        public HallService ( IRoomRepository rRepo, IHallRepository hRepo ) 
            : base( rRepo, hRepo )
        {
        }

        public Guid CreateHall ( int number, string description, int personsCount, decimal price )
        {
            if ( HallRepository.Find( number ) != null )
                throw new DuplicateNamedEntityException( typeof( Hall ), number.ToString() );


            Hall h = new Hall(Guid.NewGuid(), number, personsCount, price, description);

            HallRepository.Add(h);


            return h.Id;
        }

        public void DeletePlace ( Guid id )
        {
            Hall h = ServiceUtils.GetEntity(HallRepository, id);


            HallRepository.Delete( h );

        }

        public HallDto View ( Guid id )
        {
            return ServiceUtils.GetEntity( HallRepository, id ).toDto();
        }

        public IList<Guid> ViewAll ()
        {
            return HallRepository.SelectAllDomainIds().ToList();
        }

        public override void ChangeDescription ( Guid placeId, string description )
        {

            base.ChangeDescription( placeId, description );

        }

        public override void ChangePrice ( Guid placeId, decimal price )
        {

            base.ChangePrice( placeId, price );

        }

        public override void ResetPlaceFromRestavration ( Guid placeId )
        {

            base.ResetPlaceFromRestavration( placeId );

        }

        public override void SetPlaceOnRestavration ( Guid placeId )
        {

            base.SetPlaceOnRestavration( placeId );

        }

        public IList<HallDto> GetFreeHalls ( PeriodDto period )
        {
            BookingPeriod bp = ModelBuilder.BuildPeriod(period);

            if ( bp.Begin < Utils.BookingDate.GetToday() || bp.End < Utils.BookingDate.GetToday()
                || bp.Begin > Utils.BookingDate.GetMax() || bp.End > Utils.BookingDate.GetMax() ||
                bp.Begin > bp.End )
                throw new InvalidDataPeriodException();

            IQueryable<Hall> freeRooms = HallRepository.LoadAll().Where( r => r.isFree( ModelBuilder.BuildPeriod( period ) ) );

            List<HallDto> res = new List<HallDto>();

            foreach ( Hall h in freeRooms )
            {
                res.Add( h.toDto() );
            }

            return res;
        }

        public IList<HallDto> GetHalls ()
        {
            List<HallDto> res = new List<HallDto>();

            foreach ( Hall h in HallRepository.LoadAll().ToList() )
            {
                res.Add( h.toDto() );
            }

            return res;
        }

        public bool HasHallWithNumber ( int number )
        {
            return HallRepository.Find( number ) != null;
        }
    }
}
