using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCP_IP_Chat
{
    internal class Server
    {
        private static List<TcpClient> clients = new List<TcpClient>();
        private static TcpListener server;

        public static void Server_Main()
        {
            int port = 12345;
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Server started on port " + port);

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                Console.WriteLine("New client connected");

                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }

        private static void HandleClient(TcpClient client)
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
                    Broadcast(message, client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Client disconnected: " + e.Message);
            }
            finally
            {
                clients.Remove(client);
                client.Close();
            }
        }

        private static void Broadcast(string message, TcpClient excludeClient)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            foreach (TcpClient client in clients)
            {
                if (client != excludeClient)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
