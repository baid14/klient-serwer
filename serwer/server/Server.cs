using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
    class Server
    {
        public static Database db = null;
        private TcpListener server = null;
 
        public Server(Database dba)
        {
            db = dba;
        }

        public void Run()
        {
            try
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();
                while (true)
                {
                    Console.WriteLine("Waiting for connection...");
                    ConnectionThread t = new ConnectionThread(server.AcceptTcpClient(), db);
                    Console.WriteLine("Connected!");
                    Thread oThread = new Thread(new ThreadStart(t.connectionHandling));
                    oThread.Start();
                    oThread.Join();
                }            
            
            }
            catch (SocketException e)
            {
                Console.WriteLine("Socket error: {0}", e);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
