using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Torrents;

namespace Torrent_Collection.ViewModel
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        LogicWeb logicWeb;
        TorrentModel selectedTorrent;
        private List<TorrentModel> list;

        public SearchViewModel()
        {
            logicWeb = new LogicWeb();
        }

        public TorrentModel SelectedTorrent
        {
            get => selectedTorrent;
            set
            {
                selectedTorrent = value;
                OnPropertyChanged("selectedTorrent");
            }
        }

        public RelayCommand Search_Click => new RelayCommand(obj =>
        {
            List = logicWeb.Search(obj as string);
        });

        public List<TorrentModel> List
        {
            get => list;
            set
            {
                list = value;
                OnPropertyChanged("list");
            }
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
