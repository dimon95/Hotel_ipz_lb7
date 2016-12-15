using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Hotel.Utils;

namespace Hotel.Repository.Configurations
{
    public class BasicConfiguration<T> : EntityTypeConfiguration<T>
        where T : Entity 
    {
        protected BasicConfiguration ()
        {
            HasKey( e => e.Id );
        }
    }
}
