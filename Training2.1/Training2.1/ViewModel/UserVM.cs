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
using Training2._1.Model;

namespace Training2._1.ViewModel
{
    public class UserVM
    {
        private string username;
        private ObservableCollection<Message> messages = new ObservableCollection<Message>();

        public UserVM(string username)
        {
            this.Username = username;
        }


        public string Username
        {
            get => username; set
            {
                username = value;
                //RaisePropertyChanged();
            }
        }

        public ObservableCollection<Message> Messages
        {
            get => messages;
            set
            {
                messages = value;                
            }
        }

        internal void AddMessage(string msg)
        {
            Messages.Add(new Message(msg));
        }
    }
}
