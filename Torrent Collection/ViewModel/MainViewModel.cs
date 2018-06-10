using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Torrent_Collection.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        Page loginPage;
        Page regPage;
        Page selectedPage;

        Page searchPage;
        Page selectedGlobalPage;

        private int opacityLoginError;

        public MainViewModel()
        {
            LoginPage = new View.LoginView(this);
            RegPage = new View.RegView(this);
            OpacityLoginError = 0;
            SelectedPage = LoginPage;
        }

        public Page LoginPage
        {
            get => loginPage;
            set
            {
                loginPage = value;
                OnPropertyChanged("loginPage");
            }
        }
        public Page RegPage
        {
            get => regPage;
            set
            {
                regPage = value;
                OnPropertyChanged("regPage");
            }
        }
        public Page SelectedPage
        {
            get => selectedPage;
            set
            {
                selectedPage = value;
                OnPropertyChanged("selectedPage");
            }
        }

        public Page SearchPage
        {
            get => searchPage;
            set
            {
                searchPage = value;
                OnPropertyChanged("searchPage");
            }
        }
        public Page SelectedGlobalPage
        {
            get => selectedGlobalPage;
            set
            {
                selectedGlobalPage = value;
                OnPropertyChanged("selectedGlobalPage");
            }
        }
        public int OpacityLoginError
        {
            get => opacityLoginError;
            set
            {
                opacityLoginError = value;
                OnPropertyChanged("opacityLoginError");
            }
        }

        public RelayCommand Enter_Click => new RelayCommand(obj =>
        {
            OpacityLoginError = 1;
            /*SearchPage = new View.SearchView();
            SelectedPage = new View.GlobalView(this);*/
        });
        /// <summary>
        /// Метод команды для клика на кнопку "Регистрация"
        /// </summary>
        public RelayCommand Reg_Click => new RelayCommand(obj =>
        {
            SelectedPage = RegPage;
        });
        public RelayCommand Back_Click => new RelayCommand(obj =>
        {
            SelectedPage = LoginPage;
        });

        public RelayCommand Search_Click => new RelayCommand(obj =>
        {
            SelectedGlobalPage = SearchPage;
        });
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