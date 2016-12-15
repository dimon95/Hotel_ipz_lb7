using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;

namespace Hotel.Services
{
    public interface ICartService : IBookingHolderService
    {
        void AddItem ( Guid cartId, BookingDto booking );

        void DeleteItem ( Guid cartId, Guid bookingId );

        void Clear ( Guid cartId );

        decimal GetTotalCoast ( Guid cartId );
    }
}
