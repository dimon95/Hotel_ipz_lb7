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
    public class HistoryService : BookingHolderService, IHistoryService
    {
        private void Reshedule ( BookingHistory history, Booking oldBooking, BookingPeriod bp )
        {
            if ( oldBooking.BookedPlace.isFree( bp ) )
            {
                /*history.AddBooking( new Booking( Guid.NewGuid(), bp, oldBooking.BookedPlace,
                    oldBooking.Name, oldBooking.Surname, oldBooking.Middlename ) );

                history.DeleteBooking( oldBooking );*/
                oldBooking.BookedPlace.Bookings.Remove( oldBooking.BookingPeriod );
                oldBooking.BookingPeriod = bp;
                oldBooking.BookedPlace.Bookings.Add( oldBooking.BookingPeriod );
            }
            else
            {
                throw new ArgumentException( "No free places found" );
            }
        }

        public HistoryService ( IBookingHolderRepository bhRepo, IBookingRepository bRepo, 
            IRoomRepository rRepo, IHallRepository hRepo ) 
            : base( bhRepo, bRepo, rRepo, hRepo )
        {
        }

        public void RescheduleBooking ( Guid historyId, Guid bookingId, PeriodDto newPeriod )
        {
            BookingPeriod bp = ModelBuilder.BuildPeriod(newPeriod);

            if ( bp.Begin < Utils.BookingDate.GetToday() || bp.End < Utils.BookingDate.GetToday() 
                || bp.Begin > Utils.BookingDate.GetMax() || bp.End > Utils.BookingDate.GetMax() ||
                bp.Begin > bp.End )
                throw new InvalidDataPeriodException();
             
            BookingHistory bh = ServiceUtils.GetEntity<BookingHolder, BookingHistory>(HolderRepository, historyId);
            Booking oldBooking = HolderRepository.GetBooking(bh.Id, bookingId);

            if ( oldBooking.CheckStatus() != BookingStatus.Booked )
                throw new ArgumentException("Can't reshedule this booking");

            try
            {
                Reshedule( bh, oldBooking, ModelBuilder.BuildPeriod( newPeriod ) );
            }
            catch ( ArgumentException e )
            {
                throw e;
            }
        }

        /*public void RescheduleBooking ( Guid historyId, Guid bookingId, IList<PeriodDto> newPeriods )
        {
            BookingHistory bh = ServiceUtils.GetEntity<BookingHolder, BookingHistory>(HolderRepository, historyId);
            Booking oldBooking = HolderRepository.GetBooking(bh.Id, bookingId);

            if ( oldBooking.CheckStatus() != BookingStatus.Booked )
                throw new ArgumentException( "Can't reshedule this booking" );

            try
            {
                foreach ( PeriodDto p in newPeriods )
                {
                    Reshedule( bh, oldBooking, ModelBuilder.BuildPeriod( p ) );
                }
            }
            catch ( ArgumentException e )
            {
                throw e;
            }

        }*/

        public override IList<Guid> ViewAll ()
        {
            return HolderRepository.GetHistories().ToList();
        }
    }
}
