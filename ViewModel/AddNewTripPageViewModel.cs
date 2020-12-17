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
using Microsoft.Win32;
using System.IO;

namespace We_Split_WPF.ViewModel
{
    public class AddNewTripPageViewModel: BaseViewModel
    {
        private ObservableCollection<MemberInTripModel> _memberList;
        public ObservableCollection<MemberInTripModel> MemberList
        {
            get
            {
                return _memberList;
            }
            set
            {
                _memberList = value;
                OnPropertyChanged(nameof(MemberList));
            }
        }

        private ObservableCollection<ExpenseModel> _expensesList;
        public ObservableCollection<ExpenseModel> ExpensesList
        {
            get
            {
                return _expensesList;
            }
            set
            {
                _expensesList = value;
                OnPropertyChanged(nameof(ExpensesList));
            }
        }


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
        private ObservableCollection<MemberModel> _allMember;
        public ObservableCollection<MemberModel> AllMember
        {
            get
            {
                return _allMember;
            }
            set
            {
                _allMember = value;
                OnPropertyChanged(nameof(AllMember));
            }
        }
        public ICommand addPlaceButtonCommand { get; set; }
        public ICommand addMemberButtonCommand { get; set; }

        public ICommand addExpensesButtonCommand { get; set; }

        public ICommand doneButtonCommand { get; set; }
        public ICommand addImageButtonCommand { get; set; }
        public MainViewModel MainViewModel;

        ///  Biến Binding
        ///  
        private string _tripName;
        public string TripName
        {
            get { return this._tripName; }
            set
            {
                if (this._tripName != value)
                {
                    this._tripName = value;
                    this.OnPropertyChanged(nameof(_tripName));
                }
            }
        }
        private string _placeNameData;
        public string PlaceNameData
        {
            get { return this._placeNameData; }
            set
            {
                if (this._placeNameData != value)
                {
                    this._placeNameData = value;
                    this.OnPropertyChanged(nameof(_placeNameData));
                }
            }
        }

        private string _placeInfoData;
        public string PlaceInfoData
        {
            get { return this._placeInfoData; }
            set
            {
                if (this._placeInfoData != value)
                {
                    this._placeInfoData = value;
                    this.OnPropertyChanged(nameof(_placeInfoData));
                }
            }
        }
        //DS: DateStart
        private DateTime _placeDSData;
        public DateTime PlaceDSData
        {
            get { return this._placeDSData; }
            set
            {
                if (this._placeDSData != value)
                {
                    this._placeDSData = value;
                    this.OnPropertyChanged(nameof(_placeDSData));
                }
            }
        }
        //DF: DateFinish
        private DateTime _placeDFData;
        public DateTime PlaceDFData
        {
            get { return this._placeDFData; }
            set
            {
                if (this._placeDFData != value)
                {
                    this._placeDFData = value;
                    this.OnPropertyChanged(nameof(_placeDFData));
                }
            }
        }

        private string _memberNameData;
        public string MemberNameData
        {
            get { return this._memberNameData; }
            set
            {
                if (this._memberNameData != value)
                {
                    this._memberNameData = value;
                    this.OnPropertyChanged(nameof(_memberNameData));
                }
            }
        }

        private string _memberMoneyData;
        public string MemberMoneyData
        {
            get { return this._memberMoneyData; }
            set
            {
                if (this._memberMoneyData != value)
                {
                    this._memberMoneyData = value;
                    this.OnPropertyChanged(nameof(_memberMoneyData));
                }
            }
        }

        private string _expensesNameData;
        public string ExpensesNameData
        {
            get { return this._expensesNameData; }
            set
            {
                if (this._expensesNameData != value)
                {
                    this._expensesNameData = value;
                    this.OnPropertyChanged(nameof(_expensesNameData));
                }
            }
        }

        private string _expensesMoneyData;
        public string ExpensesMoneyData
        {
            get { return this._expensesMoneyData; }
            set
            {
                if (this._expensesMoneyData != value)
                {
                    this._expensesMoneyData = value;
                    this.OnPropertyChanged(nameof(_expensesMoneyData));
                }
            }
        }

        private int _memberID;
        public int MemberID
        {
            get { return this._memberID; }
            set
            {
                if (this._memberID != value)
                {
                    this._memberID = value;
                    this.OnPropertyChanged(nameof(_memberID));
                }
            }
        }

        private string _imageSource;
        public string ImageSource
        {
            get { return this._imageSource; }
            set
            {
                if (this._imageSource != value)
                {
                    this._imageSource = value;
                    this.OnPropertyChanged(nameof(_imageSource));
                }
            }
        }

        /// 
        public AddNewTripPageViewModel()
        {
            AllMember = new ObservableCollection<MemberModel>();
            List<MemberModel> tempList = DatabaseAccess.LoadAllMember();
            for(int i=0;i<tempList.Count();i++)
            {
                AllMember.Add(tempList[i]);
            }
            addPlaceButtonCommand = new RelayCommand(o => addPlaceButtonClick());
            addMemberButtonCommand = new RelayCommand(o => addMemberButtonClick());
            addExpensesButtonCommand = new RelayCommand(o => addExpensesButtonClick());
            doneButtonCommand = new RelayCommand(o => doneButtonClick());
            addImageButtonCommand = new RelayCommand(o => addImageButtonClick());
            PlaceList = new ObservableCollection<PlaceModel>();
            MemberList = new ObservableCollection<MemberInTripModel>();
            ExpensesList = new ObservableCollection<ExpenseModel>();
            PlaceDSData = DateTime.Now;
            PlaceDFData = DateTime.Now;
        }

        private void addPlaceButtonClick()
        {
            if(PlaceNameData!=null&&PlaceInfoData!=null)
            {
                PlaceModel temp = new PlaceModel();
                temp.Name = PlaceNameData;
                temp.Information = PlaceInfoData;
                temp.DateStart = PlaceDSData.ToString("dd/MM/yyyy");
                temp.DateFinish = PlaceDFData.ToString("dd/MM/yyyy");
                PlaceList.Add(temp);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin trước khi thêm!!!");
            }
            PlaceNameData = null ;
            PlaceInfoData = null ;
            PlaceDSData = DateTime.Now;
            PlaceDFData = DateTime.Now;
            OnPropertyChanged(nameof(PlaceList));
            OnPropertyChanged(nameof(PlaceNameData));
            OnPropertyChanged(nameof(PlaceInfoData));
            OnPropertyChanged(nameof(PlaceDSData));
            OnPropertyChanged(nameof(PlaceDFData));
        }

        private void addMemberButtonClick()
        {
            if(MemberNameData!=null && MemberMoneyData!=null)
            {
                MemberInTripModel temp = new MemberInTripModel();
                temp.ID = MemberID;
                temp.Name = MemberNameData;
                if (MemberMoneyData != null)
                {
                    temp.MoneyPaid = Int32.Parse(MemberMoneyData);
                }
                else
                {
                    temp.MoneyPaid = 0;
                }
                MemberList.Add(temp);
                if(MemberID>=0)
                {
                    AllMember.RemoveAt(MemberID);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin trước khi thêm!!!");
            }
            MemberID = -1;
            MemberNameData = null;
            MemberMoneyData = null;
            OnPropertyChanged(nameof(MemberList));
            OnPropertyChanged(nameof(AllMember));
            OnPropertyChanged(nameof(MemberNameData));
            OnPropertyChanged(nameof(MemberMoneyData));
        }

        private void addExpensesButtonClick()
        {
            if (ExpensesNameData != null && ExpensesMoneyData!=null)
            {
                ExpenseModel temp = new ExpenseModel();
                temp.Name = ExpensesNameData;
                if (ExpensesMoneyData != null)
                {
                    temp.AmountMoney = Int32.Parse(ExpensesMoneyData);
                }
                else
                {
                    temp.AmountMoney = 0;
                }
                ExpensesList.Add(temp);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin trước khi thêm!!!");
            }
            ExpensesNameData = null;
            ExpensesMoneyData = null;
            OnPropertyChanged(nameof(ExpensesList));
            OnPropertyChanged(nameof(ExpensesNameData));
            OnPropertyChanged(nameof(ExpensesMoneyData));
        }

        private void doneButtonClick()
        {
            
        }

        private void addImageButtonClick()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                File.ReadAllText(openFileDialog.FileName);
                ImageSource = openFileDialog.FileName;
            }
            OnPropertyChanged(nameof(ImageSource));
        }
    }

}
