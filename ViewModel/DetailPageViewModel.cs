using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using We_Split_WPF.Model;
namespace We_Split_WPF.ViewModel
{
    public class DetailPageViewModel: BaseViewModel
    {
        public TripModel Trip { get; set; }
        public MemberInTripModel Members { get; set; }
        public DetailPageViewModel(int ID)
        {
            Trip = DatabaseAccess.LoadSingleTrip(ID);
            Trip.Name = Trip.Name.ToUpper();
        }
    }
}
