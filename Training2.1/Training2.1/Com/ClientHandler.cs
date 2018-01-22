using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Training2._1.Com
{
    public class ClientHandler
    {
        private Socket clientSocket;
        private Action<string> updateGui;
        Thread receivingThread;
        byte[] buffer = new byte[512];
        string username = "";
        

        public ClientHandler(Socket socket, Action<string> updateGui)
        {
            clientSocket = socket;
            this.updateGui = updateGui;

            StartReceiving();
        }

        private void StartReceiving()
        {
            receivingThread = new Thread(Receive);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        private void Receive()
        {
            while (receivingThread.IsAlive)
            {
                string msg = "";
                while (true)
                {
                    int length = clientSocket.Receive(buffer);
                    string received = Encoding.UTF8.GetString(buffer, 0, length);
                    if (received != "\r\n")
                    {
                        msg += received;
                    }
                    else break;     
                }

                if(username == "")
                {
                    username = Regex.Unescape(msg);
                    msg = "joined";
                }

                updateGui(username + ":" + Regex.Unescape(msg));
            }
        }


    }
}
