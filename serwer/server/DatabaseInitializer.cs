using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class DatabaseInitializer
    {
        public static void Initialize(Database db)
        {
            db.AddUser(new User("piwosz123", "soczek321", new Account(100)));
            db.AddUser(new User("janek204", "stonoga1", new Account(1000)));
            db.AddUser(new User("rudy102", "kuleczka", new Account(102)));
            db.AddUser(new User("zdolny3.0", "poprawka2017", new Account(0)));
        }
    }
}
