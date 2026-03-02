using UsingViewModelsinMVVM.ViewModel;
using System.Windows;

namespace Using_ViewModels_in_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            MainWindowsViewModel vm = new MainWindowsViewModel();
            DataContext = vm;
        }
    }
}