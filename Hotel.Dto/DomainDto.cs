using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Dto
{
    public abstract class DomainDto
    {
        public Guid Id { get; private set; }

        protected DomainDto ( Guid id )
        {
            Id = id;  
        }
        
        //TO DO eqlity check; 
    }
}
