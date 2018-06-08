using System.Windows;

namespace Torrent_Collection.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        public RegistrationView()
        {
            InitializeComponent();

            DataContext = new ViewModel.RegistrationViewModel(this);
        }
    }
}
