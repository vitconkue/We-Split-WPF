using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using We_Split_WPF.Model;
namespace We_Split_WPF.ViewModel
{
    public class DetailPageViewModel: BaseViewModel
    {
        public TripModel Trip { get; set; }
        private MainViewModel viewModel;
        public MemberInTripModel Members { get; set; }
        public ICommand UpdateTrip { get; set; }
        public DetailPageViewModel(int ID,MainViewModel param)
        {
            Trip = DatabaseAccess.LoadSingleTrip(ID);
            Trip.Name = Trip.Name.ToUpper();
            this.viewModel = param;
        }
    }
}
