using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;

namespace Hotel.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account Find ( string email );

        Account Find ( string email, string password );
    }
}
