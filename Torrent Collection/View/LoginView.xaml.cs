using System.Windows.Controls;

namespace Torrent_Collection.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView(ViewModel.MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
