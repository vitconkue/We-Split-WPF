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

namespace We_Split_WPF.View
{
    /// <summary>
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Page
    {
        public UpdatePage()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new View.SearchPage());
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new View.HomePage());
        }

    

        private void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new View.DetailPage());
        }

        private void MemberButton_Click(object sender, RoutedEventArgs e)
        {
            An.Visibility = Visibility.Visible;
        }

        private void AddMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            An.Visibility = Visibility.Collapsed;
        }
    }
}
