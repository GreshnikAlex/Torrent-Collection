using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Torrent_Collection.Model
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UserModel : INotifyPropertyChanged
    {
        //Основные поля
        private string login;
        private string email;

        /// <summary>
        /// Логин
        /// </summary>
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(login));
            }
        }
        /// <summary>
        /// E-mail
        /// </summary>
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(email));
            }
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}