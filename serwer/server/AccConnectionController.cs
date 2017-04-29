using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class AccConnectionController
    {
        Database db;
        public AccConnectionController(Database db)
        {
            this.db = db;
        }

        public Account LogIn(DTO dto)
        {
            UserDTO userDTO = dto as UserDTO;
            if(userDTO!=null)
            {
                string username = userDTO.getUsername();
                string password = userDTO.getPassword();

                User user = db.getUserByUsername(username);
                
                if (user != null)
                {
                    string salt = user.Salt;
                    string passhash = Cryptographer.GetCrypt(password + HardSalt.Salt);
                    if (user.Passhash.Equals(passhash))
                    {
                        return user.UserAccount;
                    }
                    else
                    {
                        throw new Exception("Invalid password!");
                    }
                }
                throw new Exception("Invalid username!");
            }
            return null;
        }
    }
}
