using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Concrete;
using Hotel.Repository;
using Hotel.Model.Entities.Abstract;
using System.Data.Entity;
using Hotel.Model;

namespace Hotel.Repository.Impl
{
    public class AccountRepository : BasicRepository<Account>, IAccountRepository
    {
        public AccountRepository ( HotelDbContext dbContext ) : base( dbContext, dbContext.Accounts )
        {
        }

        public Account Find ( string email )
        {
            return DbSet.FirstOrDefault( a => a.Email == email );
        }

        public Account Find ( string email, string password )
        {
            return DbSet.FirstOrDefault( a => a.Email == email && a.PasswordHash == password );
        }

        public override void Delete ( Account t )
        {
            OnHistoryDeleteAccountVisitor visitor = new OnHistoryDeleteAccountVisitor();

            t.Accept( visitor );

            base.Delete( t );
        }
    }
}
