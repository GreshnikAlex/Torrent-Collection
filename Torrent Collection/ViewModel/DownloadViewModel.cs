using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Torrent_Collection.Client;
using Torrent_Collection.Model;

namespace Torrent_Collection.ViewModel
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DownloadModel> downloadCollection;

        public DownloadViewModel()
        {
            DownloadCollection = new ObservableCollection<DownloadModel>()
            {
                new DownloadModel() { NameFile="[kinozal.tv]id1604008.torrent" },
                new DownloadModel() { NameFile="[kinozal.tv]id1621504.torrent" }
            };

            foreach (var D in DownloadCollection)
            {
                Task.Factory.StartNew(() =>
                {
                    new Engine().Start(@"C:\Users\1995B\Downloads\", @"C:\Users\1995B\Downloads\", D);
                });
            }
        }

        private void Engine()
        {
            throw new NotImplementedException();
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

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}