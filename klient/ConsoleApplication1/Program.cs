using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    class Program
    {
        private static Thread tidListen;
        private static NetworkStream myStream;
        private static TcpClient myClient;
        private static byte[] myBuffer;
        private static bool running;
        private static MessageDTO msg;

        // Obsluga watku odpowiedzialnego za nasluch
        private static void ListenThread()
        {
            while (true)
            {
                // Oczekuje na komunikat z sieci i wczytuje go do bufora,
               
                int iLength = myStream.Read(myBuffer, 0, myClient.ReceiveBufferSize);
                msg.fromByteArray(myBuffer);
                // Konwersja danych na lancuch znakow do wyswietlenia
                //String myString = Encoding.ASCII.GetString(myBuffer);
                Console.WriteLine(msg.getMessage());

                // Wypisanie pobranych danych na ekranie
                //Console.WriteLine(myString.Substring(0, iLength));
            }
        }

        //Console close handler
        //Obsługa zamykania konsoli 
        static ConsoleEventDelegate handler;
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                running = false;
                //zakonczenie watku
                tidListen.Abort();
                //stop stream
                myClient.GetStream().Close();
                myClient.Close();
            }
            return false;
        }
        

        static void Main(string[] args)
        {
            msg = new MessageDTO();
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);
            //laczenie
            bool connected = false;
            Int32 port = 13000;
            while (!connected)
            {
                try
                {
                    myClient = new TcpClient("127.0.0.1", port);
                    myStream = myClient.GetStream();
                    connected = true;
                }
                catch (Exception connectionError)
                {
                    Console.WriteLine("\nNie udalo sie polaczyc z serwerem");
                    Console.WriteLine("Wcisnij dowolny klawisz aby kontynuowac");
                    Console.ReadKey();
                }
            }

            // Stworzenie bufora na dane w pamieci. 
            myBuffer = new byte[myClient.ReceiveBufferSize];

            // Tworzenie watka nasluchiwania 
            tidListen = new Thread(new ThreadStart(ListenThread));

            // Uruchomienie watku
            tidListen.Start();

            //stworzenie przykladowego uzytkownika
            User user = new User("piwosz123", "soczek321");
            UserDTO user1 = new UserDTO();
            MoneyDTO dto;
            user1.setUsername(user.Username);
            user1.setPassword(user.Password);
            Console.WriteLine("Connected");

            //Logowanie
            user1.setOperationType(1);
            byte[] data = user1.toByteArray();
            //wyslanie danych do serwera
            myStream.Write(data, 0, data.Length);

            string log, pas;

            int i = 0;
            int money;
            running = true;
            bool tryParse = true;

            //glowna petla programu
            while (running)
            {
                //czekamy na odpowiedz serwera
                System.Threading.Thread.Sleep(100);

                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("Wybierz operacje:");
                Console.WriteLine("1-Zaloguj");
                Console.WriteLine("2-Wyplata");
                Console.WriteLine("3-Wplac pieniedzy na konto");
                Console.WriteLine("4-Sprawdz dostepne srodki");
                Console.WriteLine("5-Wyloguj");
                Console.WriteLine("6-Zamknij program");
                Console.WriteLine("-----------------------------------------------------------");

                //zmiana operacji
                while (true)
                {
                    if (Int32.TryParse(Console.ReadLine(), out i))
                    {
                        tryParse = true;
                        break;
                    }
                    else
                    {
                        tryParse = false;
                        Console.Write("PROSZE WPROWADZIC LICZBE Z ZAKRESU 1-6");
                    }
                }
                if (tryParse)
                    switch (i)
                    {
                        case 1:
                            log = Console.ReadLine();
                            pas = Console.ReadLine();
                            Console.SetCursorPosition(0, Console.CursorTop - 2);
                            user = new User(log, pas);
                            user1.setUsername(user.Username);
                            user1.setPassword(user.Password);
                            user1.setOperationType(1);
                            data = user1.toByteArray();
                            break;
                        case 2:
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            money = 0;
                            dto = new MoneyDTO();
                            dto.setOperationType(2);
                            Console.WriteLine("-----------------------------------------------------------");
                            Console.WriteLine("Wprowadz kwote");
                            Console.WriteLine("-----------------------------------------------------------");
                            while (!Int32.TryParse(Console.ReadLine(), out money))
                            {
                                Console.Write("Bledna kwota ;)");
                            }
                            dto.setAount(Convert.ToInt32(money));
                            data = dto.toByteArray();
                            break;
                        case 3:
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            money = 0;
                            dto = new MoneyDTO();
                            dto.setOperationType(3);
                            Console.WriteLine("-----------------------------------------------------------");
                            Console.WriteLine("Wprowadz kwote");
                            Console.WriteLine("-----------------------------------------------------------");
                            while (!Int32.TryParse(Console.ReadLine(), out money))
                            {
                                Console.Write("Bledna kwota ;)");
                            }
                            dto.setAount(Convert.ToInt32(money));
                            data = dto.toByteArray();
                            break;

                        case 4:
                            dto = new MoneyDTO();
                            dto.setOperationType(4);
                            data = dto.toByteArray();
                            break;
                        case 5:
                            user1.setOperationType(5);
                            data = user1.toByteArray();
                            i = 5;
                            break;
                        case 6:
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            running = false;
                            //zakonczenie watku
                            tidListen.Abort();
                            //stop stream
                            myClient.GetStream().Close();
                            myClient.Close();
                            //zamykanie konsoli
                            Environment.Exit(0);
                            break;
                        default:
                            {
                                Console.WriteLine("-----------------------------------------------------------");
                                Console.WriteLine("BŁĘDNA OPERACJA");
                                Console.WriteLine("-----------------------------------------------------------");
                                break;
                            }
                    }

                if (running)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    myStream.Write(data, 0, data.Length);
                }
            }
        }
    }
}
