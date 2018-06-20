using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
                new DownloadModel() { Name = "Сверхъестественное", Percent=90, Download=34, Upload=54},
                new DownloadModel() { Name="Assassin's Creed", Percent=30, Download=43, Upload=589 },
                new DownloadModel() { Name = "Tomb Raider", Percent=100, Download=5, Upload=47 },
                new DownloadModel() { Name = "Аватар", Percent=0, Download=74, Upload=48 }
            };
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