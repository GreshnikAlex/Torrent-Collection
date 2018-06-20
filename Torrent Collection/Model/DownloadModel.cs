using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Torrent_Collection.Model
{
    /// <summary>
    /// Модель загрузок
    /// </summary>
    public class DownloadModel : INotifyPropertyChanged
    {
        private string name; //Имя
        private int percent; //Проценты
        private int download; //Загружают
        private int upload; //Раздают

        /// <summary>
        /// Имя
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(name));
            }
        }
        /// <summary>
        /// Проценты
        /// </summary>
        public int Percent
        {
            get => percent;
            set
            {
                percent = value;
                OnPropertyChanged(nameof(percent));
            }
        }
        /// <summary>
        /// Загружают
        /// </summary>
        public int Download
        {
            get => download;
            set
            {
                download = value;
                OnPropertyChanged(nameof(download));
            }
        }
        /// <summary>
        /// Раздают
        /// </summary>
        public int Upload
        {
            get => upload;
            set
            {
                upload = value;
                OnPropertyChanged(nameof(upload));
            }
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
