using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            }
            );
        });

        public RelayCommand Download_Click => new RelayCommand(obj =>
        {
            TorrentModel torrentModel = obj as TorrentModel;
            MessageBox.Show(torrentModel.UrlTorrent);
        });

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
