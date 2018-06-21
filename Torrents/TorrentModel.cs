using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Torrents
{
    /// <summary>
    /// Класс торрент модели
    /// </summary>
    public class TorrentModel : INotifyPropertyChanged
    {
        private string urlTorrent;
        private string name;
        private string size;
        private int distribute;
        private int download;
        private string urlFile;

        /// <summary>
        /// Метод для ссылки на торрент
        /// </summary>
        public string UrlTorrent
        {
            get => urlTorrent;
            set
            {
                urlTorrent = value;
                OnPropertyChanged("urlTorrent");
            }
        }
        /// <summary>
        /// Метод для названия торрента
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("name");
            }
        }
        /// <summary>
        /// Метод на размер торрента
        /// </summary>
        public string Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged("size");
            }
        }
        /// <summary>
        /// Метод для количества раздающих торрент
        /// </summary>
        public int Distribute
        {
            get => distribute;
            set
            {
                distribute = value;
                OnPropertyChanged("distribute");
            }
        }
        /// <summary>
        /// Метод для количества скачивающих торрент
        /// </summary>
        public int Download
        {
            get => download;
            set
            {
                download = value;
                OnPropertyChanged("download");
            }
        }
        /// <summary>
        /// Ссылка на адрес торрент файла
        /// </summary>
        public string UrlFile
        {
            get => urlFile;
            set
            {
                urlFile = value;
                OnPropertyChanged(nameof(urlFile));
            }
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
