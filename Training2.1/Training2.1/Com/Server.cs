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

namespace Training2._1.Com
{
    public class Server
    {
        private Action<string> updateGui;
        Socket serverSocket;
        List<ClientHandler> Clients = new List<ClientHandler>();
        const int port = 10100;
        Thread acceptingThread;

        public Server(Action<string> updateGui)
        {
            this.updateGui = updateGui;

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
            serverSocket.Listen(10);

            StartAccepting();
        }

        private void StartAccepting()
        {
            acceptingThread = new Thread(Accept);
            acceptingThread.IsBackground = true;
            acceptingThread.Start();
        }

        private void Accept()
        {
            while (acceptingThread.IsAlive)
            {
                Clients.Add(new ClientHandler(serverSocket.Accept(), updateGui));
            }
        }


    }
}
