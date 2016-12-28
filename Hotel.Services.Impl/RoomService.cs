using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Dto;
using Hotel.Repository;
using Hotel.Model.Entities.Concrete;
using Hotel.Exceptions;
using Hotel.Model;

namespace Hotel.Services.Impl
{
    public class RoomService : PlaceService, IRoomService
    {
        public RoomService ( IRoomRepository rRepo, IHallRepository hRepo ) : 
            base( rRepo, hRepo )
        {
        }

        public Guid CreateRoom ( int number, string description, int personsCount, int bedCount, 
            decimal price )
        {
            if ( RoomRepository.Find(number) != null )
                throw new DuplicateNamedEntityException( typeof( Room ), number.ToString() );

            if ( personsCount == 1 && bedCount == 2 )
                throw new BedsOutOfRange();

            Room r = new Room(Guid.NewGuid(), number, personsCount, price, description, bedCount);

            RoomRepository.Add( r );

            return r.Id;
        }

        public void ResetCriteria ( Guid roomId, SearchCriteria criteria )
        {
            Room r = ServiceUtils.GetEntity(RoomRepository, roomId);

            if ( !r.HasCriteria( criteria ) )
                throw new RemovingNotExistingRoomCriteriaException( roomId );

            r.ResetCriteria( criteria );
        }

        public void SetCriteria ( Guid roomId, SearchCriteria criteria )
        {
            Room r = ServiceUtils.GetEntity(RoomRepository, roomId);

            if ( r.HasCriteria( criteria ) )
                throw new MultipleAddingRoomCriteriaException( roomId );

            r.SetCriteria( criteria );

        }

        public RoomDto View ( Guid id )
        {
            return ServiceUtils.GetEntity( RoomRepository, id ).toDto();
        }

        public IList<Guid> ViewAll ()
        {
            return RoomRepository.SelectAllDomainIds().ToList();
        }

        public void DeletePlace ( Guid id )
        {
            Room r = ServiceUtils.GetEntity(RoomRepository, id);

            RoomRepository.Delete( r );
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

        public IList<RoomDto> GetRooms ( SearchCriteria criteria )
        {
            List<RoomDto> res = new List<RoomDto>();

            foreach ( Room r in RoomRepository.LoadAll() )
            {
                if ( r.HasCriteria(criteria) )
                      res.Add( r.toDto() );
            }
            
            return res;
        }

        public IList<RoomDto> GetRooms ( int sCriteria )
        {
            List<RoomDto> res = new List<RoomDto>();

            foreach ( Room r in RoomRepository.LoadAll() )
            {
                if ( r.SearchCriterias == 0 && sCriteria == 0 )
                    res.Add( r.toDto() );

                foreach ( SearchCriteria sc in Enum.GetValues( typeof( SearchCriteria ) ) )
                {
                    if ( r.HasCriteria(sc) && Model.Utils.HasCriteria(sCriteria, sc) && !res.Contains(r.toDto()))
                    {
                        res.Add( r.toDto() );
                    }
                }
            }

            return res;
        }

        public IList<RoomDto> GetRooms ( decimal bPrice, decimal ePrice )
        {
            List<RoomDto> res = new List<RoomDto>();

            foreach ( Room r in RoomRepository.LoadAll() )
            {
                if ( r.Price >= bPrice && r.Price <= ePrice )
                    res.Add( r.toDto() );
            }

            return res;
        }

        public IList<RoomDto> GetFreeRooms ( PeriodDto period )
        {
            BookingPeriod bp = ModelBuilder.BuildPeriod(period);

            if ( bp.Begin < Utils.BookingDate.GetToday() || bp.End < Utils.BookingDate.GetToday()
                || bp.Begin > Utils.BookingDate.GetMax() || bp.End > Utils.BookingDate.GetMax() ||
                bp.Begin > bp.End )
                throw new InvalidDataPeriodException();

            IQueryable<Room> freeRooms = RoomRepository.LoadAll().Where( r => r.isFree( ModelBuilder.BuildPeriod( period ) ) );

            List<RoomDto> res = new List<RoomDto>();

            foreach ( Room r in freeRooms )
            {
                res.Add( r.toDto() );
            }

            return res;
        }

        public IList<RoomDto> GetRooms ()
        {
            List<RoomDto> res = new List<RoomDto>();

            foreach ( Room r in RoomRepository.LoadAll().ToList() )
            {
                res.Add( r.toDto() );
            }

            return res;
        }

        public bool HasRoomWithNumber ( int number )
        {
            return RoomRepository.Find( number ) != null;
        }
    }
}
