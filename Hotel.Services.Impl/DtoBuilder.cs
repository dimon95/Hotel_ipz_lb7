using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;
using Hotel.Model.Entities.Abstract;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Services.Impl
{
    static class DtoBuilder
    {
        public static AccountDto toDto ( this Account a )
        {
            Role r;

            if ( a is Client ) r = Role.Client;
            else if ( a is Admin ) r = Role.Admin;
            else throw new NotImplementedException(); 

            return new AccountDto( a.Id, a.Name, a.Surname, a.Middlename, a.Email, a.PasswordHash,
                a.DateOfBirth.Day, a.DateOfBirth.Month, a.DateOfBirth.Year, a.Cart.toDto(), a.History.toDto(), r );
        }
        
        public static BookingHolderDto toDto ( this BookingHolder c)
        {
            IList<BookingDto> tmp = new List<BookingDto>();

            foreach ( Booking b in c.Bookings )
                tmp.Add( b.toDto() );

            return new BookingHolderDto( c.Id, tmp);
        }

        public static BookingDto toDto ( this Booking booking)
        {
            return new BookingDto( booking.Id, booking.BookingPeriod.toDto(), booking.BookedPlace.Id,
                    booking.Name, booking.Surname, booking.Middlename );
        }

        public static PeriodDto toDto ( this BookingPeriod bp )
        {
            return new PeriodDto( bp.Id, bp.Begin.Day, bp.Begin.Month, bp.Begin.Year, bp.End.Day, bp.End.Month, bp.End.Year );
        }

        public static RoomDto toDto ( this Room r )
        {
            IList<PeriodDto> tmp  = new List<PeriodDto>();

            foreach ( BookingPeriod bp in r.Bookings )
                tmp.Add( bp.toDto() );

            return new RoomDto( r.Id, r.Number, r.Price, r.Description, r.PersonsCount, r.OnRestavretion, r.BedCount, 
                r.SearchCriterias, tmp );
        }

        public static HallDto toDto ( this Hall p )
        {
            IList<PeriodDto> tmp  = new List<PeriodDto>();

            foreach ( BookingPeriod bp in p.Bookings )
                tmp.Add( bp.toDto() );

            return new HallDto( p.Id, p.Number, p.Price, p.Description, p.PersonsCount, p.OnRestavretion, tmp );
        }
    }
}
