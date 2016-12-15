using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Hotel.Utils;

namespace Hotel.Repository.Configurations
{
    public class DateConfiguration : ComplexTypeConfiguration<Date>
    {
        public DateConfiguration ()
        {
            Property( d => d.Day ).IsRequired();
            Property( d => d.Month ).IsRequired();
            Property( d => d.Year ).IsRequired();
        }
    }
}
