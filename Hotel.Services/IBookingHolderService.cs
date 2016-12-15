using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;

namespace Hotel.Services
{
    public interface IBookingHolderService : IDomainEntityService<BookingHolderDto>
    {
        IList<Guid> GetBookingsList ( Guid bookingHolderId );
    }
}
