using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TravMsgServer
{
    public class client
    {
        Program prog;
        public TcpClient protclient;
        public string userName;

        public client(Program p, TcpClient c)
        {
            prog = p;
            protclient = c;
            (new Thread(new ThreadStart(SetupConn))).Start();
        }

        public NetworkStream netStream;  // Raw-data stream of connection.
        public BinaryReader br;          // Read simple data
        public BinaryWriter bw;          // Write simple data

        void SetupConn()
        {
            netStream = protclient.GetStream();
            br = new BinaryReader(netStream);
            bw = new BinaryWriter(netStream);


            bw.Write(IM_Hello);
            bw.Flush();

            int hello = br.ReadInt32();

            if (hello == IM_Hello)
            {
                userName = br.ReadString();

                Console.WriteLine("Checking to See if Username is ok!");
                if (CheckUserName())
                {
                    Console.WriteLine(userName + " Is Connected!");
                    prog.CurrentUsers.Add(this);
                    Reciever();
                }

           

            }

          


            CloseConn();

        }
        
        void CloseConn()
        {
            Console.WriteLine("Someone is Logged out!");
            br.Close();
            bw.Close();
            netStream.Close();
            protclient.Close();
            prog.CurrentUsers.Remove(this);
        }

        public const int IM_Hello = 2012;      // Hello
        public const byte IM_OK = 0;           // OK
        public const byte IM_Login = 1;        // Login
        public const byte IM_Register = 2;     // Register
        public const byte IM_TooUsername = 3;  // Too long username
        public const byte IM_UsernameTaken = 4;  // Username is Taken
        public const byte IM_Exists = 5;       // Already exists
        public const byte IM_NoExists = 6;     // Doesn't exists
        public const byte IM_WrongPass = 7;    // Wrong password
        public const byte IM_IsAvailable = 8;  // Is user available?
        public const byte IM_Available = 9;    // User is available or not
        public const byte IM_Send = 10;        // Send message
        public const byte IM_Received = 11;    // Message received


        void Reciever()
        {
            
            try
            {
                while (protclient.Connected)
                {
                    byte type = br.ReadByte();

                    if (type == IM_Send)
                    {
                        string msg = br.ReadString();

                        prog.MessageRecieved(userName, msg);
                    }

                }
            }
            catch (IOException) { }

        }

        bool CheckUserName ()  
        {
            if (userName.Length >= 15)  // if length is greater than 15, then no bueno
            {
                Console.WriteLine("Username " + userName + " is too long!");
                bw.Write(IM_TooUsername);
                bw.Flush();

                return false;
            }

            for (int i = 0; i < prog.CurrentUsers.Count; i++) // if name taken, no bueno
            {
               if (userName ==  prog.CurrentUsers[i].userName)
               {
                   Console.WriteLine("Username " + userName + " is Taken!");
                   bw.Write(IM_UsernameTaken);
                   bw.Flush();

                   return false;
               }
            }

            Console.WriteLine("Username " + userName + " is OK");
            bw.Write(IM_OK);  // send the bueno
            bw.Flush();

            return true;  // bueno
        }
    }
}
