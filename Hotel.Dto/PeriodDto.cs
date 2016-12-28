using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dto
{
    public class PeriodDto : DomainDto
    {
        public int BeginDay { get; private set; }
        public int BeginMonth { get; private set; }
        public int  BeginYear { get; private set; }

        public int EndDay { get; private set; }
        public int EndMonth { get; private set; }
        public int EndYaer { get; private set; }

        public PeriodDto ( Guid id, int bDay, int bMonth, int bYear, int eDay, int eMonth, int eYear )
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
