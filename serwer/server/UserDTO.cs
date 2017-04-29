using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class UserDTO:DTO
    {
        private string username;
        private string password;

        public UserDTO(){ } 

        public void setUsername(string username)
        {
            this.username=username;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public string getUsername()
        {
            return this.username;
        }

        public string getPassword()
        {
            return this.password;
        }

    }
}
