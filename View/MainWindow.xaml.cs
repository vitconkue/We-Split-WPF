using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using We_Split_WPF.ViewModel;

namespace We_Split_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowViewModel : Window
    {
        public MainWindowViewModel()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        public SearchPageViewModel SelectedViewModel { get; internal set; }
    }
}
