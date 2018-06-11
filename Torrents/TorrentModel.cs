using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Torrents
{
    public class TorrentModel : INotifyPropertyChanged
    {
        private string urlTorrent;
        private string name;
        private string size;
        private int distribute;
        private int download;

        public string UrlTorrent
        {
            get => urlTorrent;
            set
            {
                urlTorrent = value;
                OnPropertyChanged("urlTorrent");
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("name");
            }
        }
        public string Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged("size");
            }
        }
        public int Distribute
        {
            get => distribute;
            set
            {
                distribute = value;
                OnPropertyChanged("distribute");
            }
        }
        public int Download
        {
            get => download;
            set
            {
                download = value;
                OnPropertyChanged("download");
            }
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
