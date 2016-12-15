using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;

namespace Hotel.Services
{
    public interface IDomainEntityService<TDto> where TDto : DomainDto
    {
        IList<Guid> ViewAll ();

        TDto View ( Guid id );
    }
}
