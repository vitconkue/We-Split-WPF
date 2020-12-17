using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using We_Split_WPF.Command;
using We_Split_WPF.Model;

namespace We_Split_WPF.ViewModel
{
    public class UpdatePageViewModel : BaseViewModel
    {
        private int id;
        private MainViewModel viewModel;
        public ICommand UpdateTrip { get; set; }
        public ICommand AddMoneyForMemmber { get; set; }
        public ICommand AddExpense { get; set; }
        public ICommand AddPlace { get; set; }
        public ICommand BackToTrip { get; set; }
        private string _currentMoney = "";
        private string _moneyReceived;
        public string MoneyReceived
        {
            get
            {
                return _moneyReceived;
            }
            set
            {
                _moneyReceived = value;
                OnPropertyChanged(nameof(MoneyReceived));
            }
        }
        public string CurrentMoney
        {
            get
            {
                return _currentMoney;
            }
            set
            {
                _currentMoney = value;
                OnPropertyChanged(nameof(CurrentMoney));
            }
        }
        private ObservableCollection<PlaceModel> _placeList;
        public ObservableCollection<PlaceModel> PlaceList
        {
            get
            {
                return _placeList; ;
            }
            set
            {
                _placeList = value;
                OnPropertyChanged(nameof(PlaceList));
            }
        }
        private ObservableCollection<ExpenseModel> _expenses;
        public ObservableCollection<ExpenseModel> Expenses
        {
            get
            {
                return _expenses;
            }
            set
            {
                _expenses = value;
                OnPropertyChanged(nameof(Expenses));
            }
        }
        private ObservableCollection<MemberInTripModel> _memeberList;
        public ObservableCollection<MemberInTripModel> MemberList
        {
            get
            {
                return _memeberList;
            }
            set
            {
                _memeberList = value;
                OnPropertyChanged(nameof(MemberList));
            }
        }
        public TripModel Trip { get; set; }
        public UpdatePageViewModel(int ID, MainViewModel param)
        {
            id = ID;
            viewModel = param;
            Trip = DatabaseAccess.LoadSingleTrip(id);
            Expenses = new ObservableCollection<ExpenseModel>(Trip.expensesList);
            PlaceList = new ObservableCollection<PlaceModel>(Trip.placeList);
            AddMoneyForMemmber = new RelayCommand(o => UpdateMemberMoney(o));
            AddPlace = new RelayCommand(o => AddPlaceForTrip(o));
            AddExpense = new RelayCommand(o => AddExpenseForTrip(o));
            MemberList = new ObservableCollection<MemberInTripModel>(Trip.memberList);
            MoneyReceived = Trip.MoneyReceived.ToString();
            BackToTrip = new RelayCommand(o => BackToTripPage());
        }
        public void UpdateMemberMoney(object o)
        {
            int result;
            if (int.TryParse(CurrentMoney,out result))
            {
                var temp = (MemberInTripModel)o;
                Trip.GetMoreMoneyFromMember(temp, result);
                MemberList = new ObservableCollection<MemberInTripModel>(Trip.memberList);
                OnPropertyChanged(nameof(MemberList));
                MoneyReceived = Trip.MoneyReceived.ToString();
                CurrentMoney = "";
            }
        }
        public void AddExpenseForTrip(object o)
        {
            int result;
            var parameter = (object[])o;
            if ((parameter[0].ToString() != "" && parameter[1].ToString() != "") && int.TryParse(parameter[1].ToString(),out result))
            {
                ExpenseModel temp = new ExpenseModel();
                temp.Name = parameter[0].ToString();
                temp.AmountMoney = result;
                Expenses.Add(temp);
                OnPropertyChanged(nameof(Expenses));
                Trip.AddExpense(temp);
            }
        }
        public void AddPlaceForTrip(object o)
        {
            var parameter = (object[])o;
            List<string> Data = new List<string>();
           
            foreach (var x in parameter)
            {
                if (x.GetType().Equals(typeof(DateTime)))
                {
                    DateTime y = (DateTime)x;
                    Data.Add(y.ToString("dd/MM/yyyy"));
                }
                else
                    Data.Add(x.ToString());
            }
            PlaceModel temp = new PlaceModel();
            temp.Name=Data[0];
            temp.Information = Data[1];
            temp.DateStart = Data[2];
            temp.DateFinish = Data[3];
            Trip.AddPlace(temp);
            PlaceList.Add(temp);
            OnPropertyChanged(nameof(Trip));
          
        }
        public void BackToTripPage()
        {
            ICommand temp = new UpdateHomeViewCommand(viewModel);
            temp.Execute((object)Trip.ID);
        }
    }
}