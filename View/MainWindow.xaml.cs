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

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomePageIcon.Foreground = Brushes.Yellow;
            HomePageName.Foreground = Brushes.Yellow;
            LocationIcon.Foreground = Brushes.White;
            LocationName.Foreground = Brushes.White;
            InfoIcon.Foreground = Brushes.White;
            InfoName.Foreground = Brushes.White;
            SearchIcon.Foreground = Brushes.White;
            SearchName.Foreground = Brushes.White;
        }

       
        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            HomePageIcon.Foreground = Brushes.White;
            HomePageName.Foreground = Brushes.White;
            LocationIcon.Foreground = Brushes.White;
            LocationName.Foreground = Brushes.White;
            InfoIcon.Foreground = Brushes.Yellow;
            InfoName.Foreground = Brushes.Yellow;
            SearchIcon.Foreground = Brushes.White;
            SearchName.Foreground = Brushes.White;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            HomePageIcon.Foreground = Brushes.White;
            HomePageName.Foreground = Brushes.White;
            LocationIcon.Foreground = Brushes.White;
            LocationName.Foreground = Brushes.White;
            InfoIcon.Foreground = Brushes.White;
            InfoName.Foreground = Brushes.White;
            SearchIcon.Foreground = Brushes.Yellow;
            SearchName.Foreground = Brushes.Yellow;
        }
    }
}
