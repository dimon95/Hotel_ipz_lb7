using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Hotel.Model.Entities.Abstract;

namespace Hotel.Repository.Configurations
{
    public class AccountConfiguration : BasicConfiguration<Account>
    {
        public AccountConfiguration ()
        {
            Property( a => a.Name ).IsRequired();
            Property( a => a.Surname ).IsRequired();
            Property( a => a.Email ).IsRequired();
            Property( a => a.PasswordHash ).IsRequired();

            HasRequired( a => a.Cart ).WithMany().WillCascadeOnDelete();
            HasRequired( a => a.History ).WithMany().WillCascadeOnDelete();
        }
    }
}
