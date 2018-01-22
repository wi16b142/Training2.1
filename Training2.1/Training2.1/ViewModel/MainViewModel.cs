using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Training2._1.Com;

namespace Training2._1.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        Server server;
        ObservableCollection<UserVM> users;
        ObservableCollection<HistoryVM> history;
        UserVM selectedUser;
        bool isConnected = false;
        RelayCommand startBtnClick;
        RelayCommand exportBtnClick;
        bool newUser = true;

        public MainViewModel()
        {
            Users = new ObservableCollection<UserVM>();
            History = new ObservableCollection<HistoryVM>();
            SelectedUser = null;

            if(IsInDesignMode)
            {
                Users.Add(new UserVM("user1"));
                UpdateGui("user1:msg1");
                UpdateGui("user1:msg2");
                UpdateGui("user1:msg3");
            }
            else
            {
                Users.Add(new UserVM("user1"));
                UpdateGui("user1:msg1");
                UpdateGui("user1:msg2");
                UpdateGui("user1:msg3");

                StartBtnClick = new RelayCommand(()=> 
                {
                    server = new Server(UpdateGui);
                    isConnected = true;
                }, ()=> { return !isConnected; });

                ExportBtnClick = new RelayCommand(()=> 
                {
                    Export();
                }, ()=> { return (SelectedUser == null) ? false : true ; });

            }
        }

        private void Export()
        {
            string[] export = new string[SelectedUser.Messages.Count];
            int i = 0;

            string folder = @"../../../logfiles/";
            string filename = SelectedUser.Username + DateTime.Now.ToFileTimeUtc();
            string extension = ".csv";

            foreach (var message in SelectedUser.Messages)
            {
                export[i] = SelectedUser.Username + ";" + message.Timestamp.ToShortTimeString() + ";" + message.Text;
                i++;
            }

            File.WriteAllLines(folder + filename + extension, export);
        }

        private void UpdateGui(string msg)
        {
            App.Current.Dispatcher.Invoke(()=>
            {
                string[] raw = msg.Split(':');

                if (Users.Count > 0)
                {
                    foreach (var user in Users)
                    {
                        if (user.Username.Equals(raw[0]))
                        {
                            user.AddMessage(raw[1]);
                            History.Add(new HistoryVM(raw[0], raw[1]));
                            newUser = false;
                            break;
                        }
                    }
                }

                if(newUser == true)
                {
                    Users.Add(new UserVM(raw[0]));
                    Users[Users.Count - 1].AddMessage(raw[1]);
                }

                newUser = true;
                RaisePropertyChanged("Users");
                RaisePropertyChanged("History");
            });
        }

        public ObservableCollection<UserVM> Users
        {
            get => users; set
            {
                users = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<HistoryVM> History
        {
            get => history; set
            {
                history = value;
                RaisePropertyChanged();
            }
        }
        public UserVM SelectedUser
        {
            get => selectedUser; set
            {
                selectedUser = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand StartBtnClick { get => startBtnClick; set => startBtnClick = value; }
        public RelayCommand ExportBtnClick { get => exportBtnClick; set => exportBtnClick = value; }
    }
}