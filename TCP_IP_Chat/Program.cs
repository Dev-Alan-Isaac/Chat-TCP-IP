using System;
using System.Threading;

namespace TCP_IP_Chat
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify 'server' or 'client' as an argument.");
                return;
            }

            string choice = args[0].ToLower();

            switch (choice)
            {
                case "server":
                    Console.WriteLine("Starting Server...");
                    Server.Server_Main();
                    break;
                case "client":
                    Console.WriteLine("Starting Client...");
                    Client.Client_Main();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please specify 'server' or 'client' as an argument.");
                    break;
            }
        }
    }
}
