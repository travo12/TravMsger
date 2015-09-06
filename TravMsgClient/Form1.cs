using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

    public partial class Form1 : Form
    {
        IMClient im = new IMClient();

        public Form1()
        {
          
            InitializeComponent();
            Send_Button.Enabled = false;
            Chat_Window.Items.Add("Please Enter your Username In the box below and click the Login button");
            Control.CheckForIllegalCrossThreadCalls = false;

      

        }
        public void Reciever()
        {
            byte IM_Received = 11;
            try
            {
                while (im.client.Connected == true)
                {
                    byte type = im.br.ReadByte();

                    if (type == IM_Received)
                    {
                        string from = im.br.ReadString();
                        string msg = im.br.ReadString();

                        Chat_Window.Items.Add(from + ": " + msg);

                        // OnMessageRecieved(new IMRecievedEventArgs(from, msg));
                    }
                    
                }
            }
            catch (IOException) { }

        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            string username = Text_Box_Message.Text;
            Chat_Window.Items.Add("Logging in with Username " + username);
            im.Login(username, this);
            Text_Box_Message.Clear();
            Send_Button.Enabled = true;
            Login_Button.Text = "Connected";
            Login_Button.Enabled = false;

        }
        public const byte IM_TooUsername = 3;  // Too long username
        public const byte IM_UsernameTaken = 4;  // Username is Taken
        public const byte IM_OK = 0;           // OK

        public bool NameStatusCheck()
        {
            Chat_Window.Items.Add("Checking username..");
            int statuscheck = im.br.ReadByte();

            if (statuscheck == IM_TooUsername)
            {
                Send_Button.Enabled = false;
                Login_Button.Text = "Connect";
                Login_Button.Enabled = true;
                Chat_Window.Items.Add("Username is Too Long, please enter another username, maximum 15 characters");

                return false;
                
            }
            else if (statuscheck == IM_UsernameTaken)
            {
                Send_Button.Enabled = false;
                Login_Button.Text = "Connect";
                Login_Button.Enabled = true;
                Chat_Window.Items.Add("Username is taken, please enter another username, maximum 15 characters");

                return false;
            }
            else if(statuscheck == IM_OK)
            {
                Chat_Window.Items.Add("Login Successful!");
                Chat_Window.Items.Add("begin typing now!");


                return true;
            }

            return true;
           
        }

        private void Send_Button_Click(object sender, EventArgs e)
        {
            im.SendMessage(Text_Box_Message.Text);
            Text_Box_Message.Clear();
        }


  
    }
}
