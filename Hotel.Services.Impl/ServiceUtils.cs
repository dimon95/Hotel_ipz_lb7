using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Repository;
using Hotel.Model.Entities.Concrete;
using Hotel.Utils;
using Hotel.Model.Entities.Abstract;

namespace Hotel.Services.Impl
{
    sealed class ServiceUtils
    {
        private ServiceUtils () { }

        public static TEntity GetEntity<TEntity> ( IRepository<TEntity> repo, Guid id )
             where TEntity : Entity
        {
            TEntity entity = repo.Load(id);

            if ( entity == null )
                throw new ArgumentException( "invalid id" );

            return entity;
        }

        public static TDerivedEntity GetEntity<TEntity, TDerivedEntity> ( IRepository<TEntity> repo, Guid id )
             where TEntity : Entity
             where TDerivedEntity : TEntity
        {
            TDerivedEntity entity = repo.Load(id) as TDerivedEntity;

            if ( entity == null )
                throw new ArgumentException( "invalid id" );

            return entity;
        }

        /*public static TBase GetEntity<TBase> ( IList<IRepository<TBase>> derivedRepos, Guid id )
            where TBase : Entity
        {
            foreach ( var entry in derivedRepos )
            {
                TBase entity = entry.Load(id);

                if ( entity != null )
                    return entity;
            }

            throw new ArgumentException("invalid id");
        }*/

        public static Place GetPlace ( IRoomRepository rRepo, IHallRepository hRepo, Guid id )
        {
            Place r = rRepo.Load(id);
            Place h = hRepo.Load(id);

            if ( r == null && h == null )
                throw new ArgumentException("invalid id");

            return r == null ? h : r;
        }
    }
}
