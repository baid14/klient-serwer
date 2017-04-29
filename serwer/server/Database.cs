using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class Database
    {
        private List<User> userList;
        public Database() { userList = new List<User>(); }

        public void AddUser(User user)
        {
            userList.Add(user);
        }

        public User getUserByUsername(string username)
        {
            foreach(User u in userList)
            {
                if (u.Username.Equals(username))
                    return u;
            }
            return null;
        }


    }
}
