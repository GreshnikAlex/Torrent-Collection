using System.Windows.Controls;

namespace Torrent_Collection.View
{
    /// <summary>
    /// Логика взаимодействия для GlobalView.xaml
    /// </summary>
    public partial class GlobalView : Page
    {
        public GlobalView(ViewModel.MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
