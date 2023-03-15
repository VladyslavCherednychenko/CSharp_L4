using System.Windows;
using System.Windows.Input;

using StudentManager.Utilities;
using StudentManager.MVVM.ViewModel;

namespace StudentManager
{
    public partial class MainWindow : Window, IClosable
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
