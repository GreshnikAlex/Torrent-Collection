using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Torrent_Collection.Client;
using Torrent_Collection.Model;

namespace Torrent_Collection.ViewModel
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DownloadModel> downloadCollection;

        public DownloadViewModel()
        {
            DownloadCollection = new ObservableCollection<DownloadModel>();
            Task.Factory.StartNew(() =>
            {
                Search();
            });
        }

        /// <summary>
        /// Колекция загрузок
        /// </summary>
        public ObservableCollection<DownloadModel> DownloadCollection
        {
            get => downloadCollection;
            set
            {
                downloadCollection = value;
                OnPropertyChanged(nameof(downloadCollection));
            }
        }

        /// <summary>
        /// Поиск новых торрентов для отображения
        /// </summary>
        private void Search()
        {
            var connection = new SqlConnection()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
            };

            var command = new SqlCommand("Dow", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure,
            };
            connection.Open();

            command.Parameters.AddWithValue("@login", Properties.Settings.Default.Login.ToLower());

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                bool Add = true;
                var Dow = new DownloadModel() { NameFile = reader.GetValue(0).ToString() };
                foreach (var Index in DownloadCollection)
                {
                    if (Dow.NameFile == Index.NameFile)
                    {
                        Add = false;
                        break;
                    }
                }
                if (Add)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        this.DownloadCollection.Add(Dow);
                    }));
                    Task.Factory.StartNew(() =>
                    {
                        new Engine().Start($"C:\\Users\\{Environment.UserName}\\Downloads\\TorrentFiles\\", $"C:\\Users\\{Environment.UserName}\\Downloads\\", DownloadCollection[DownloadCollection.Count - 1]);
                    });
                }
                Thread.Sleep(500);
            }
            reader.Close();
            Thread.Sleep(1000);
            Search();
        }

        public RelayCommand Open_Click => new RelayCommand(obj =>
        {
            Process.Start($"C:\\Users\\{Environment.UserName}\\Downloads\\{(obj as DownloadModel).Name}");
        });

        public RelayCommand Delete_Click => new RelayCommand(obj =>
        {
            try
            {
                var connection = new SqlConnection()
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
                };

                var command = new SqlCommand("Delete", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                connection.Open();

                command.Parameters.AddWithValue("@login", Properties.Settings.Default.Login.ToLower());
                command.Parameters.AddWithValue("@nameFile", (obj as DownloadModel).NameFile);

                command.ExecuteNonQuery();

                File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\TorrentFiles\\{(obj as DownloadModel).NameFile}");
                DownloadCollection.Remove(obj as DownloadModel);
            }
            catch
            { return; }
        });

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}