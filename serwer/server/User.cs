using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class User
    {
        private string username;
        private string passhash;
        private string hardSalt=HardSalt.Salt;
        private string salt = "TO3";
        Account userAccount;
        /*
        private string GetCrypt(string text)
        {
            SHA512 alg = SHA512.Create();
            return Encoding.UTF8.GetString(alg.ComputeHash(Encoding.UTF8.GetBytes(text)));
        }
        */
        public User(string username, string password, Account acc)
        {
            this.username = username;
            this.passhash = Cryptographer.GetCrypt(Cryptographer.GetCrypt(password + salt) + hardSalt);
            userAccount = acc;
        }

        public User(string username, string password) : this(username, password, new Account()) { }

        public string Username
        {
            get { return username; }
            set { this.username = value; }
        }

        public string Passhash
        {
            get { return passhash; }
            set { this.passhash = value; }
        }
        
        public string Salt
        {
            get { return salt; }
            set { this.salt = value; }
        }

        public Account UserAccount
        {
            get { return userAccount; }
            set { this.userAccount = value; }
        }

    }
}
