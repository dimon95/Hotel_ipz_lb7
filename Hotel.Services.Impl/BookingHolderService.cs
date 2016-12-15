using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;
using Hotel.Model.Entities.Concrete;
using Hotel.Dto;
using Hotel.Repository;

namespace Hotel.Services.Impl
{
    public abstract class BookingHolderService : IBookingHolderService
    {

        protected IBookingHolderRepository HolderRepository { get; private set; }
        protected IBookingRepository BookingRepository { get; private set; }
        //protected IBookingPeriodRepository PeriodRepository { get; private set; } 
        protected IRoomRepository RoomRepository { get; private set; }
        protected IHallRepository HallRepository { get; private set; }

        protected BookingHolderService ( IBookingHolderRepository bhRepo, IBookingRepository bRepo, 
            IRoomRepository roomRepo, IHallRepository hallRepo )
        {
            HolderRepository = bhRepo;
            BookingRepository = bRepo;
            RoomRepository = roomRepo;
            HallRepository = hallRepo;
           // PeriodRepository = bpRepo;
        }

        public IList<Guid> GetBookingsList ( Guid bookingHolderId )
        {            
            return HolderRepository.GetBookings( bookingHolderId ).ToList();
        }

        public BookingHolderDto View ( Guid id )
        {
            return ServiceUtils.GetEntity( HolderRepository, id ).toDto();
        }

        public abstract IList<Guid> ViewAll ();
    }
}
