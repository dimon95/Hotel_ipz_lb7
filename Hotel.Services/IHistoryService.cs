using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;

namespace Hotel.Services
{
    public interface IHistoryService : IBookingHolderService
    {
        void RescheduleBooking ( Guid HistoryId, Guid bookingId, PeriodDto newPeriod );
    }
}
