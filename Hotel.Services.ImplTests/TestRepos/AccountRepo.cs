using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model.Entities.Abstract;

namespace Hotel.Services.ImplTests.TestRepos
{
    public class AccountRepo : Hotel.Repository.IAccountRepository
    {
        private readonly List<Account> _accounts = new List<Account>();

        public void Add ( Account t )
        {
            _accounts.Add(t);
        }

        public int Count ()
        {
            return _accounts.Count;
        }

        public void Delete ( Account t )
        {
            _accounts.Remove( t );
        }

        public Account Find ( string email )
        {
            return _accounts.FirstOrDefault( a => a.Email == email );
        }

        public Account Find ( string email, string password )
        {
            return _accounts.FirstOrDefault( a => a.Email == email && a.PasswordHash == password);
        }

        public Account Load ( Guid id )
        {
            return _accounts.FirstOrDefault( a => a.Id == id );
        }

        public IQueryable<Account> LoadAll ()
        {
            return _accounts.AsQueryable();
        }

        public IQueryable<Guid> SelectAllDomainIds ()
        {
            return _accounts.Select( a => a.Id ).AsQueryable();
        }
    }
}
