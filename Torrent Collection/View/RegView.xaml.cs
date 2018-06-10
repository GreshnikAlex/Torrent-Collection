using System.Windows.Controls;

namespace Torrent_Collection.View
{
    /// <summary>
    /// Логика взаимодействия для RegView.xaml
    /// </summary>
    public partial class RegView : Page
    {
        public RegView(ViewModel.MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
