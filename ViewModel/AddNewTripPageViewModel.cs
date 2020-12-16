using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using We_Split_WPF.Command;
using We_Split_WPF.Model;

namespace We_Split_WPF.ViewModel
{
    public class AddNewTripPageViewModel: BaseViewModel
    {
        public List<MemberInTripModel> memberList { get; set; } = new List<MemberInTripModel>();

        public List<ExpenseModel> expensesList { get; set; } = new List<ExpenseModel>();

        private ObservableCollection<PlaceModel> _placeList;
        public ObservableCollection<PlaceModel> PlaceList
        {
            get
            {
                return _placeList;
            }
            set
            {
                _placeList = value;
                OnPropertyChanged(nameof(PlaceList));
            }
        }
        public List<MemberModel> allMember { get; set; } = new List<MemberModel>();
        public ICommand addPlaceButtonCommand { get; set; }
        public MainViewModel MainViewModel;
        public AddNewTripPageViewModel()
        {
            allMember = DatabaseAccess.LoadAllMember();
            addPlaceButtonCommand = new RelayCommand(o => addPlaceButtonClick());
            PlaceList = new ObservableCollection<PlaceModel>();
            PlaceModel temp = new PlaceModel();
            temp.Name = "Xyz";
            temp.Information = "Dz";
            temp.DateStart = "11/11/2011";
            temp.DateFinish = "12/12/2012";
            PlaceList.Add(temp);
        }

        private void addPlaceButtonClick()
        {
            MessageBox.Show("Button clicked");
            PlaceModel temp = new PlaceModel();

            temp.Name = "an";
            temp.Information = "D";
            temp.DateStart = "1/11/2011";
            temp.DateFinish = "12/1/2012";
            PlaceList.Add(temp);
            OnPropertyChanged(nameof(PlaceList));
        }
    }
}
