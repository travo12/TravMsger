using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TravMsgClient
{
    class IMClient
    {
        Thread tcpThread;
        bool _conn = false;
        string _user;
        Form1 _form1;

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
        public const byte IM_Received = 11;  // Message received


        public TcpClient client;
        public NetworkStream netstream;
        public BinaryReader br;
        public BinaryWriter bw;


        public string Server { get { return "192.168.2.231"; } }
        public int Port { get { return 2000; } }
        public string UserName { get { return _user; } }

        void SetupConn()
        {
            client = new TcpClient(Server, Port);
            netstream = client.GetStream();

            br = new BinaryReader(netstream);
            bw = new BinaryWriter(netstream);
    
        
            int hello = br.ReadInt32();

            if (hello == IM_Hello)
            {

                bw.Write(IM_Hello);
                bw.Flush();

                bw.Write(_user);
                bw.Flush();

            }

            _conn = true;

            if (_form1.NameStatusCheck())
            {
                _form1.Reciever();

            }
       
         



            if (_conn)
            {
                CloseConn();
            }
        }

        void CloseConn()
        {
            br.Close();
            bw.Close();
            netstream.Close();
            client.Close();
        }

        void Connect(string user)
        {
            if(!_conn)
            {
                _conn = true;
                _user = user;

                tcpThread = new Thread(new ThreadStart(SetupConn));
                tcpThread.Start();
            }
        }
        public void Login(string user, Form1 form1)
        {
            _form1 = form1;
            Connect(user);
        }
        public void Disconnect()
        {
            if (_conn)
            {
                CloseConn();
            }
        }

        /*void Reciever()
        {
            try
            {
                while (client.Connected)
                {
                    byte type = br.ReadByte();

                    if (type == IM_Received)
                    {
                        string from = br.ReadString();
                        string msg = br.ReadString();

        
                        // OnMessageRecieved(new IMRecievedEventArgs(from, msg));
                    }
                }
            }
            catch (IOException) { }

        } */
        public void SendMessage (string msg)
        {
            bw.Write(IM_Send);
            bw.Write(msg);
            bw.Flush();
        }

    }
}
