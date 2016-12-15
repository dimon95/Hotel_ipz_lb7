using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dto
{
    public class PeriodDto : DomainDto
    {
        public byte BeginDay { get; private set; }
        public byte BeginMonth { get; private set; }
        public int  BeginYear { get; private set; }

        public byte EndDay { get; private set; }
        public byte EndMonth { get; private set; }
        public int EndYaer { get; private set; }

        public PeriodDto ( Guid id, byte bDay, byte bMonth, int bYear, byte eDay, byte eMonth, int eYear )
            :base(id)
        {
            BeginDay = bDay;
            BeginMonth = bMonth;
            BeginYear = bYear;

            EndDay = eDay;
            EndMonth = eMonth;
            EndYaer = eYear;
        }

        public override string ToString ()
        {
            return BeginDay + "." + BeginMonth + "." + BeginYear + "-" + 
                   EndDay + "." + EndMonth + "." + EndYaer + "\r\n";
        }

    }
}
