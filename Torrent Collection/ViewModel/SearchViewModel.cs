using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Torrents;

namespace Torrent_Collection.ViewModel
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private LogicWeb logicWeb;
        private TorrentModel selectedTorrent;
        private List<TorrentModel> list;
        private bool indeterminate;

        public SearchViewModel()
        {
            Indeterminate = false;
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

        public List<TorrentModel> List
        {
            get => list;
            set
            {
                list = value;
                OnPropertyChanged("list");
            }
        }

        public bool Indeterminate
        {
            get => indeterminate;
            set
            {
                indeterminate = value;
                OnPropertyChanged("identerminate");
            }
        }

        public RelayCommand Search_Click => new RelayCommand(obj =>
        {
            Task.Factory.StartNew(() =>
            {
                Indeterminate = true;
                List = logicWeb.Search(obj as string);
                Indeterminate = false;
            }
            );
        });

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
