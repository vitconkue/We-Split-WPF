using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using We_Split_WPF.Model;
using We_Split_WPF.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace We_Split_WPF.ViewModel
{
    public class SearchPageViewModel : BaseViewModel
    {
        private ObservableCollection<TripModel> _trips;
        public ICommand Search { get; set; }
        public ObservableCollection<TripModel> Trips
        {
            get
            {
                return _trips;
            }
            set
            {
                _trips = value;
                OnPropertyChanged(nameof(Trips));
            }
        }
        public SearchPageViewModel(){
            Search = new RelayCommand(o => SearchFunction(o));
        }
        public void SearchFunction(object o)
        {

        }
    }
}
