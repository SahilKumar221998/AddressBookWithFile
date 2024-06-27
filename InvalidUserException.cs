using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookWithDictionary
{
   public class InvalidUserException:Exception
    {
        public InvalidUserException(string message) : base(message)
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
