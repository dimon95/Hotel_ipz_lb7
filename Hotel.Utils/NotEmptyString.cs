using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Utils
{
    public class NotEmptyString
    {
        private string _value;

        private void Check ( string value )
        {
            if ( value == null )
                throw new ArgumentNullException("value");
            if ( value.Length == 0 )
                throw new ArgumentException("string can't be empty");
        }

        public string Value
        {
            get { return _value; }
            set
            {
                Check(value); _value = value;
            }
        }
    }
}
