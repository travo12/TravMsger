using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace TravMsgServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program P = new Program();
            Console.WriteLine();
            Console.WriteLine("Press enter to close program.");
            Console.ReadLine();
        }

        public IPAddress ip = IPAddress.Parse("192.168.2.231");
        public int port = 2000;
        public bool running = true;
        public TcpListener server;
        public List<client> CurrentUsers;
        int usertracker;

       

        public Program()
        {
            Console.Title = "InstantMessenger Server";
            Console.WriteLine("----- Trav's IM Server ------");

            Console.WriteLine("Please enter the IP of the server");
            ip = IPAddress.Parse(Console.ReadLine());

            server = new TcpListener(ip, port);
            server.Start();

            Console.WriteLine("[{0}] Server is running!", DateTime.Now);
            usertracker = 0;
            CurrentUsers = new List<client>();
            Listen();

        }
        void Listen()
        {
            while(running)
            {
                TcpClient tcpClient = server.AcceptTcpClient();
                client client = new client(this, tcpClient);
                usertracker++;
                Console.WriteLine("Someone is Trying to Connect!");
            }
        }

        public const byte IM_Received = 11;    // Message received

        public void MessageRecieved(string user, string message)  // send recieved message to all users
        {
            for (int i = 0; i < CurrentUsers.Count; i++)
            {
                CurrentUsers[i].bw.Write(IM_Received);
                CurrentUsers[i].bw.Write(user);
                CurrentUsers[i].bw.Write(message);
                CurrentUsers[i].bw.Flush();

                Console.WriteLine(user + ": " + message);
            }
        }

        void showUsers(client client)
        {
            string userString = "";
            client.bw.Write(client.IM_Send);
            client.bw.Write("Server");

                if (CurrentUsers.Count == 0)
                {
                    client.bw.Write("There are no other users connected");
                }
                else
                {
                    for (int i = 0; i < CurrentUsers.Count; i++)
                    {
                        string.Concat(userString, CurrentUsers[i].userName + ", ");
                    }

                    client.bw.Write(userString);
                }
            client.bw.Flush();
        }

    }
}
