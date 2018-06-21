using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Torrents;

namespace Torrent_Collection.ViewModel
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private LogicWeb logicWeb;
        private bool indeterminate;
        private ObservableCollection<TorrentModel> torrentCollection;

        public SearchViewModel()
        {
            Indeterminate = false;
            logicWeb = new LogicWeb();
        }

        public ObservableCollection<TorrentModel> TorrentCollection
        {
            get => torrentCollection;
            set
            {
                torrentCollection = value;
                OnPropertyChanged(nameof(torrentCollection));
            }
        }

        public bool Indeterminate
        {
            get => indeterminate;
            set
            {
                indeterminate = value;
                OnPropertyChanged(nameof(indeterminate));
            }
        }

        public RelayCommand Search_Click => new RelayCommand(obj =>
        {
            Task.Factory.StartNew(() =>
            {
                Indeterminate = true;
                TorrentCollection = logicWeb.Search(obj as string);
                Indeterminate = false;
            });
        });

        public RelayCommand Download_Click => new RelayCommand(obj =>
        {
            try
            {
                WebClient webClient = new WebClient();
                string[] name = (obj as TorrentModel).UrlFile.Split('/');
                webClient.DownloadFile(new Uri((obj as TorrentModel).UrlFile), $"C:\\Users\\{ Environment.UserName}\\Downloads\\TorrentFiles\\{name[name.Length - 1]}.torrent");

                var connection = new SqlConnection()
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
                };

                var command = new SqlCommand("AddTorrent", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                connection.Open();

                command.Parameters.AddWithValue("@login", Properties.Settings.Default.Login.ToLower());
                command.Parameters.AddWithValue("@nameFile", name[name.Length - 1]+ ".torrent");

                command.ExecuteNonQuery();
            }
            catch
            { return; }
        });

        public RelayCommand FollowLink_Click => new RelayCommand(obj =>
        {
            Process.Start((obj as TorrentModel).UrlTorrent);
        });

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
