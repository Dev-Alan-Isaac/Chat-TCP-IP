using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCP_IP_Chat
{
    internal class Client
    {
        public static void Client_Main()
        {
            string serverIP = "127.0.0.1";
            int port = 12345;
            TcpClient client = new TcpClient();

            try
            {
                client.Connect(serverIP, port);
                Console.WriteLine("Connected to server");

                Thread receiveThread = new Thread(() => ReceiveMessages(client));
                receiveThread.Start();

                SendMessages(client);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private static void ReceiveMessages(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int byteCount;

            try
            {
                while ((byteCount = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, byteCount);
                    Console.WriteLine("Received: " + message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Server connection lost: " + e.Message);
            }
        }

        private static void SendMessages(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            while (true)
            {
                string message = Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
