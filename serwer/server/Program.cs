using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();
            DatabaseInitializer.Initialize(db);

            Server server = new Server(db);
            server.Run();
        }
    }
}
