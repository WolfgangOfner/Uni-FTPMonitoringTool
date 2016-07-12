using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = null;
            int port = 0;
            ConsoleKeyInfo consoleKeyInfo;

            // Let the user input the IP-Address.
            do
            {
                Console.Write("Bitte geben Sie die IP Adresse an: ");
                try
                {
                    ipAddress = IPAddress.Parse(Console.ReadLine());
                }
                catch (Exception)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong ip entered! Repeat input.\n");
                    Console.ResetColor();
                }
            } while (ipAddress == null);

            // Let the user input the port number.
            do
            {
                Console.Write("Bitte geben Sie den Port an: ");
                try
                {
                    port = int.Parse(Console.ReadLine());

                    if (port < 1 || port > 65535)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong port entered! Port must be between 1 and 65535. Repeat input.\n");
                    port = 0;
                    Console.ResetColor();
                }
            } while (port == 0);
           
            Client client = new Client(ipAddress, port);
            client.Start();

            // run programm till its exited with e input
            do
            {
                if (Console.KeyAvailable)
                {
                    consoleKeyInfo = Console.ReadKey(true);

                    switch (consoleKeyInfo.KeyChar)
                    {                       
                        case 'e':
                            client.EndProgram();
                            break;
                    }
                }
                Thread.Sleep(10);

            } while (true);
        }
    }
}
