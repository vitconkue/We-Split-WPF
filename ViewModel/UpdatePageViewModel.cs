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
        public TripModel Trip { get; set; }
        public UpdatePageViewModel(int ID, MainViewModel param)
        {
            id = ID;
            viewModel = param;
            Trip = DatabaseAccess.LoadSingleTrip(id);
            Expenses = new ObservableCollection<ExpenseModel>(Trip.expensesList);
            AddMoneyForMemmber = new RelayCommand(o => UpdateMemberMoney(o));
            AddExpense = new RelayCommand(o => AddExpenseForTrip(o));
        }
        public void UpdateMemberMoney(object o)
        {
            var parameter = o;
            MessageBox.Show("Click");

        }
        public void AddExpenseForTrip(object o)
        {
            var parameter = (object[])o;
            if (parameter[0].ToString() != "" || parameter[1].ToString() != "")
            {
                ExpenseModel temp = new ExpenseModel();
                temp.Name = parameter[0].ToString();
                temp.AmountMoney = int.Parse(parameter[1].ToString());
                Expenses.Add(temp);
                OnPropertyChanged(nameof(Expenses));
                Trip.AddExpense(temp);
            }
        }
    }
}