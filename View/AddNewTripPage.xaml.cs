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
    /// Interaction logic for AddNewTripPage.xaml
    /// </summary>
    public partial class AddNewTripPage : Page
    {
        public AddNewTripPage()
        {
            InitializeComponent();
            List<Model.MemberModel> AllMember = new List<Model.MemberModel>();
            AllMember = Model.DatabaseAccess.LoadAllMember();
        }

        private List<Model.MemberInTripModel> memberList;
        private List<Model.PlaceModel> placeList;

        private void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new View.DetailPage());
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new View.HomePage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPlaceButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
