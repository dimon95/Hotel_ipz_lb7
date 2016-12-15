using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Utils;

namespace Hotel.Repository
{
    public interface IRepository<T> where T : Entity
    {
        T Load ( Guid id );

        IQueryable<T> LoadAll ();

        int Count ();

        void Add ( T t);

        void Delete ( T t );

        IQueryable<Guid> SelectAllDomainIds ();
    }
}
