using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Cryptographer
    {
        public static string GetCrypt(string text)
        {
            SHA512 alg = SHA512.Create();
            return Encoding.UTF8.GetString(alg.ComputeHash(Encoding.UTF8.GetBytes(text)));
        }
    }
}
