using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Torrent_Collection.Model;

namespace Torrent_Collection.ViewModel
{
    /// <summary>
    /// Класс главной модели представления
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private Page loginPage; //Страница входа
        private Page regPage; //Страница регистрации
        private Page selectedPage; //Активная страница

        private Page searchPage; //Страница поиска
        private Page selectedGlobalPage; //Активная страница на глобальной странице

        private UserModel user; //Модель "Пользователь"

        private int opacityLoginError; //Показывает/Скрывает ошибку
        private bool indeterminate; //Показывает/Скрывает загрузку
        private bool enabledForm; //Включает/Отключает текстовые поля и кнопки
        private string errorString; //Сообщение с ошибкой

        /// <summary>
        /// Конструктор главной модели придставления
        /// </summary>
        public MainViewModel()
        {
            User = new UserModel();
            LoginPage = new View.LoginView() { DataContext = this};
            RegPage = new View.RegView() { DataContext = this };
            OpacityLoginError = 0;
            EnabledForm = true;
            SelectedPage = LoginPage;
        }

        //Методы
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
        /// Пользователь
        /// </summary>
        public UserModel User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(user));
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
        /// Метод для отображения загрузки
        /// </summary>
        public bool Indeterminate
        {
            get => indeterminate;
            set
            {
                indeterminate = value;
                OnPropertyChanged(nameof(indeterminate));
            }
        }
        /// <summary>
        /// Метод для включения и выключения кнопок
        /// </summary>
        public bool EnabledForm
        {
            get => enabledForm;
            set
            {
                enabledForm = value;
                OnPropertyChanged(nameof(enabledForm));
            }
        }
        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        public string ErrorString
        {
            get => errorString;
            set
            {
                errorString = value;
                OnPropertyChanged(nameof(errorString));
            }
        }

        //Страница входа
        /// <summary>
        /// Комманда для кнопки "Вход"
        /// </summary>
        public RelayCommand Enter_Click => new RelayCommand(obj =>
        {
            EnabledForm = false;
            var searchPage = new View.SearchView();
            var globalPage = new View.GlobalView(this);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OpacityLoginError = 0;
                    Indeterminate = true;
                    var connection = new SqlConnection()
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
                    };

                    var command = new SqlCommand("Verification", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                    };
                    connection.Open();

                    //Шифрование пароля
                    var md5 = new MD5CryptoServiceProvider();
                    byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes((obj as PasswordBox).Password));
                    var pass = BitConverter.ToString(checkSum).Replace("-", String.Empty);

                    command.Parameters.AddWithValue("@login", user.Login.ToLower());
                    command.Parameters.AddWithValue("@password", pass);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    { User.Email = reader.GetValue(0).ToString(); }

                    if (User.Email != null)
                    {
                        SearchPage = searchPage;
                        SelectedPage = globalPage;
                    }
                    else
                    {
                        ErrorString = "Логин или Пароль не верен...";
                        OpacityLoginError = 1;
                        searchPage = null;
                        globalPage = null;
                    }
                }
                catch
                {
                    ErrorString = "Что-то не так, попробуйте позже...";
                    OpacityLoginError = 1;
                }
                finally
                {
                    Indeterminate = false;
                    EnabledForm = true;
                }
            });
        });
        /// <summary>
        /// Метод команды для клика на кнопку "Регистрация"
        /// </summary>
        public RelayCommand Reg_Click => new RelayCommand(obj =>
        {
            SelectedPage = RegPage;
        });

        //Страница регистрации
        /// <summary>
        /// Регистрация
        /// </summary>
        public RelayCommand Registration_Click => new RelayCommand(obj =>
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    OpacityLoginError = 0;
                    Indeterminate = true;

                    var connection = new SqlConnection()
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
                    };

                    var command = new SqlCommand("Registration", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                    };
                    connection.Open();

                    //Шифрование пароля
                    var md5 = new MD5CryptoServiceProvider();
                    byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes((obj as PasswordBox).Password));
                    var pass = BitConverter.ToString(checkSum).Replace("-", String.Empty);

                    command.Parameters.AddWithValue("@login", user.Login.ToLower());
                    command.Parameters.AddWithValue("@password", pass);
                    command.Parameters.AddWithValue("@email", user.Email.ToLower());

                    command.ExecuteNonQuery();
                    MessageBox.Show("Поздравляем, вы успешно прошли регистрацию, теперь вы можете авторизоваться.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    ErrorString = "Что-то не так, попробуйте позже...";
                    OpacityLoginError = 1;
                }
                finally
                {
                    Indeterminate = false;
                    EnabledForm = true;
                }
            });
        });
        /// <summary>
        /// Команда для кнопки "Назад"
        /// </summary>
        public RelayCommand Back_Click => new RelayCommand(obj =>
        {
            SelectedPage = LoginPage;
            User.Email = null;
        });

        //Глобальная страница
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
            User.Email = null;
            SearchPage = null;
            SelectedGlobalPage = null;
            SelectedPage = LoginPage;
        });

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}