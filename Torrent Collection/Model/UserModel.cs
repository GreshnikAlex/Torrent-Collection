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
        private string password;
        private string email;
        //Второстепенные поля
        private string firstName;
        private string middleName;
        private string surName;

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
        /// Пароль
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(password));
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
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(firstName));
            }
        }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName
        {
            get => middleName;
            set
            {
                middleName = value;
                OnPropertyChanged(nameof(middleName));
            }
        }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string SurName
        {
            get => surName;
            set
            {
                surName = value;
                OnPropertyChanged(nameof(surName));
            }
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}