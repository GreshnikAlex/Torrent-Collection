using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Torrent_Collection.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private Window window;

        public RegistrationViewModel(Window window)
        {
            this.window = window;
        }

        /// <summary>
        /// Метод команды для клика на кнопку "Отмена"
        /// </summary>
        public RelayCommand Cancel_Click => new RelayCommand(obj =>
        {
            window.Close();
        });

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
