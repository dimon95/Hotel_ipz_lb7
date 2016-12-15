using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model.Entities.Concrete;
using Hotel.Dto;
using Hotel.Model.Entities.Abstract;
using Hotel.Utils;
using Hotel.Exceptions;
using Hotel.Repository;

namespace Hotel.Services.Impl
{
    public class CartService : BookingHolderService, ICartService
    {   
        /*private IBookingHolderRepository HolderRepository {get; set;}
        private IBookingRepository BookingRepository { get; set;}
        private IRoomRepository RoomRepository { get; set; }
        private IHallRepository HallRepository { get; set; }*/

        public CartService ( IBookingHolderRepository bhRepo, IBookingRepository bRepo,
                IRoomRepository rRepo, IHallRepository hRepo ) 
            : base( bhRepo, bRepo, rRepo, hRepo )
        {
        }

        public void AddItem ( Guid cartId, BookingDto booking )
        {
            Cart c = ServiceUtils.GetEntity<BookingHolder, Cart>(HolderRepository, cartId);

            Place p = ServiceUtils.GetEntity(RoomRepository, booking.BookedPlaceId);

            BookingPeriod bp = ModelBuilder.BuildPeriod(booking.BookingPeriod);

            if ( bp.Begin < Utils.BookingDate.GetToday() || bp.End < Utils.BookingDate.GetToday()
                || bp.Begin > Utils.BookingDate.GetMax() || bp.End > Utils.BookingDate.GetMax() ||
                bp.Begin > bp.End )
                throw new InvalidDataPeriodException();

            if ( c.Bookings.FirstOrDefault( bk => bk.BookedPlace == p && bk.BookingPeriod.Intersect( bp ) ) != null )
                throw new AttempToOrderOrderedPlaceException( p.Id );

           

            Booking b = new Booking(Guid.NewGuid(), bp, 
                ServiceUtils.GetPlace(RoomRepository, HallRepository, booking.BookedPlaceId),
                booking.Name, booking.Surname, booking.Middlename);

            c.AddBooking( b );

           
        }

        public void Clear ( Guid cartId )
        {
           

            Cart c = ServiceUtils.GetEntity<BookingHolder, Cart>(HolderRepository, cartId);

            c.Clear();

           
        }

        public void DeleteItem ( Guid cartId, Guid bookingId )
        {
            Cart c = ServiceUtils.GetEntity<BookingHolder, Cart>(HolderRepository, cartId);

            if ( c.Bookings.FirstOrDefault( bk => bk.Id == bookingId ) == null )
                throw new RemovingNotExistingCartItemException( bookingId, cartId );

            

            Booking b = ServiceUtils.GetEntity(BookingRepository, bookingId);

            c.DeleteBooking( b );

           
        }

        /*public IList<Guid> GetBookingsList ( Guid bookingHolderId )
        {
            return HolderRepository.GetBookings( bookingHolderId ).ToList();
        }*/

        public decimal GetTotalCoast ( Guid cartId )
        {
            Cart c = ServiceUtils.GetEntity<BookingHolder, Cart>(HolderRepository, cartId);

            return c.Calculate();
        }

        public override IList<Guid> ViewAll ()
        {
            return HolderRepository.GetCarts().ToList();
        }
    }
}
