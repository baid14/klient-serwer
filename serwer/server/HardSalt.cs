using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class HardSalt
    {
        private static string salt = "TO4";

        public static string Salt
        {
            get;
            set;
        }
    }
}
