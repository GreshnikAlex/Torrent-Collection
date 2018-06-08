using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Torrent_Collection.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Метод команды для клика на кнопку "Регистрация"
        /// </summary>
        public RelayCommand Reg_Click => new RelayCommand(obj =>
        {
            new View.RegistrationView().ShowDialog();
        });

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}