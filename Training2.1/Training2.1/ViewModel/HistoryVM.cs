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

namespace Training2._1.ViewModel
{
    public class HistoryVM
    {
        private string name;
        private string text;

        public HistoryVM(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }

        public string Name { get => name; set => name = value; }
        public string Text { get => text; set => text = value; }
    }
}
