using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Utils
{
    public interface IHashProvider<T>
    {
        string GetHashCode ( T str );
    }
}
