using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Torrent_Collection.ViewModel
{
    /// <summary>
    /// Класс главной модели представления
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        Page loginPage;
        Page regPage;
        Page selectedPage;

        Page searchPage;
        Page selectedGlobalPage;

        private int opacityLoginError;

        /// <summary>
        /// Конструктор главной модели придставления
        /// </summary>
        public MainViewModel()
        {
            LoginPage = new View.LoginView(this);
            RegPage = new View.RegView(this);
            OpacityLoginError = 0;
            SelectedPage = LoginPage;
        }

        /// <summary>
        /// Метод для перехода на страницу входа
        /// </summary>
        public Page LoginPage
        {
            get => loginPage;
            set
            {
                loginPage = value;
                OnPropertyChanged("loginPage");
            }
        }
        /// <summary>
        /// Метод для перехода на страницу регистрации
        /// </summary>
        public Page RegPage
        {
            get => regPage;
            set
            {
                regPage = value;
                OnPropertyChanged("regPage");
            }
        }
        /// <summary>
        /// Метод для работы с выбранной страницей
        /// </summary>
        public Page SelectedPage
        {
            get => selectedPage;
            set
            {
                selectedPage = value;
                OnPropertyChanged("selectedPage");
            }
        }

        /// <summary>
        /// Метод для перехода на страницу поиска
        /// </summary>
        public Page SearchPage
        {
            get => searchPage;
            set
            {
                searchPage = value;
                OnPropertyChanged("searchPage");
            }
        }
        /// <summary>
        /// Метод для работы с выбраной страницей в аккаунте пользователя
        /// </summary>
        public Page SelectedGlobalPage
        {
            get => selectedGlobalPage;
            set
            {
                selectedGlobalPage = value;
                OnPropertyChanged("selectedGlobalPage");
            }
        }
        /// <summary>
        /// Метод для отображения ошибки о не правильно вводе данных
        /// </summary>
        public int OpacityLoginError
        {
            get => opacityLoginError;
            set
            {
                opacityLoginError = value;
                OnPropertyChanged("opacityLoginError");
            }
        }

        /// <summary>
        /// Комманда для кнопки "Вход"
        /// </summary>
        public RelayCommand Enter_Click => new RelayCommand(obj =>
        {
            //OpacityLoginError = 1;
            SearchPage = new View.SearchView();
            SelectedPage = new View.GlobalView(this);
        });
        /// <summary>
        /// Метод команды для клика на кнопку "Регистрация"
        /// </summary>
        public RelayCommand Reg_Click => new RelayCommand(obj =>
        {
            SelectedPage = RegPage;
        });
        /// <summary>
        /// Команда для кнопки "Назад"
        /// </summary>
        public RelayCommand Back_Click => new RelayCommand(obj =>
        {
            SelectedPage = LoginPage;
        });

        /// <summary>
        /// Команда для кнопки "Поиск"
        /// </summary>
        public RelayCommand Search_Click => new RelayCommand(obj =>
        {
            SelectedGlobalPage = SearchPage;

        });
        /// <summary>
        /// Команда для кнопки "Выход"
        /// </summary>
        public RelayCommand Exit_Click => new RelayCommand(obj => 
        {
            SelectedPage = LoginPage;
            SearchPage = null;
            SelectedGlobalPage = null;
        });

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}