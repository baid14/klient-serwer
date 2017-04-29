using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace ConsoleApplication1
{
    class User
    {
        private String username;
        private String password;
        

        public User(String username , String pass) {
            this.username = username;
            this.password = GetCrypt(pass + "TO3");
        }

        private string GetCrypt(string text)
        {
            SHA512 alg = SHA512.Create();
            return Encoding.UTF8.GetString(alg.ComputeHash(Encoding.UTF8.GetBytes(text)));
        }


        public String Username {get {return username;}}
        public String Password { get { return password; } }
    }
}
