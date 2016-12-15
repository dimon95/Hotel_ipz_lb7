using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Utils
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity () { }

        protected Entity ( Guid id )
        {
            if ( id == null )
                throw new ArgumentNullException( "id" );

            Id = id;
        }

        public override bool Equals ( object obj )
        {
            if ( this == obj )
                return true;

            if ( obj == null || GetType() != obj.GetType() )
                return false;

            var otherEntity = ( Entity ) obj ;
            return Id == otherEntity.Id;
        }

        public override int GetHashCode ()
        {
            return Id.GetHashCode();
        }
    }
}
