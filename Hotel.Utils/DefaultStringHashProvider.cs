using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Utils
{
    public class DefaultStringHashProvider : IHashProvider<string>
    {
        public string GetHashCode ( string str )
        {
            return str.GetHashCode().ToString();
        }
    }
}
