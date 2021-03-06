﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
        private Page downloadPage; //Страница загрузок
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
        /// Метод для перехода на страницу загрузок
        /// </summary>
        public Page DownloadPage
        {
            get => downloadPage;
            set
            {
                downloadPage = value;
                OnPropertyChanged(nameof(downloadPage));
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
            Properties.Settings.Default.Login = "";
            Properties.Settings.Default.Save();
            EnabledForm = false;
            var searchPage = new View.SearchView();
            var downloadPage = new View.DownloadView();
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
                    reader.Close();

                    if (User.Email != null)
                    {
                        SearchPage = searchPage;
                        DownloadPage = downloadPage;
                        SelectedPage = globalPage;
                        Properties.Settings.Default.Login = User.Login;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        ErrorString = "Логин или Пароль не верен...";
                        OpacityLoginError = 1;
                        searchPage = null;
                        globalPage = null;
                        downloadPage = null;
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

                    connection.Open();

                    var command = new SqlCommand("SearchUser", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                    };

                    if (new Regex("@").Matches(User.Email).Count == 1)
                    {
                        command.Parameters.AddWithValue("@login", user.Login.ToLower());
                        command.Parameters.AddWithValue("@email", user.Email.ToLower());
                        var reader = command.ExecuteReader();
                        bool con = true;
                        while (reader.Read())
                        {
                            if (User.Login.ToLower() == reader.GetValue(0).ToString())
                            {
                                ErrorString = "Пользователь с таким логином уже зарегистрирован...";
                                OpacityLoginError = 1;
                                con = false;
                                break;
                            }
                            else
                            {
                                if (User.Email.ToLower() == reader.GetValue(1).ToString())
                                {
                                    ErrorString = "Пользователь с таким E-mail уже зарегистрирован...";
                                    OpacityLoginError = 1;
                                    con = false;
                                    break;
                                }
                            }
                        }
                        reader.Close();

                        if (con)
                        {
                            //Шифрование пароля
                            var md5 = new MD5CryptoServiceProvider();
                            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes((obj as PasswordBox).Password));
                            var pass = BitConverter.ToString(checkSum).Replace("-", String.Empty);

                            command.CommandText = "Registration";
                            command.Parameters.AddWithValue("@password", pass);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Поздравляем, вы успешно прошли регистрацию, теперь вы можете авторизоваться.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        ErrorString = "E-mail указан не коректно...";
                        OpacityLoginError = 1;
                    }
                }
                catch (Exception ex)
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
        /// Команда для кнопки "Загрузки"
        /// </summary>
        public RelayCommand Download_Click => new RelayCommand(obj =>
        {
            SelectedGlobalPage = DownloadPage;
        });
        /// <summary>
        /// Команда для кнопки "Выход"
        /// </summary>
        public RelayCommand Exit_Click => new RelayCommand(obj => 
        {
            User.Email = null;
            SearchPage = null;
            DownloadPage = null;
            SelectedGlobalPage = null;
            SelectedPage = LoginPage;
        });

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}