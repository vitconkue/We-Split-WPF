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
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : UserControl
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void TripName_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MemberName.IsChecked == true)
                {
                    MemberName.IsChecked = false;
                }
            }
            catch
            {

            }
        }

        private void MemberName_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TripName.IsChecked == true)
                {
                    TripName.IsChecked = false;
                }
            }
            catch
            {

            }
        }
    }
}
