using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;

namespace Hotel.Model
{
    public interface IAccountVisitor
    {
        void Visit ( Account acc );
    }
}
