using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using We_Split_WPF.Command;
using We_Split_WPF.Model;

namespace We_Split_WPF.ViewModel
{
   public class UpdatePageViewModel: BaseViewModel
    {
        private int id;
        private MainViewModel viewModel;
        public ICommand UpdateTrip { get; set; }
        private List<ExpenseModel> _expenses;
        public List<ExpenseModel> Expenses
        {
            get
            {
                return _expenses;
            }
            set
            {
                _expenses = value;
                OnPropertyChanged("Expenses");
            }
        }
        public TripModel Trip { get; set; }
        public UpdatePageViewModel(int ID,MainViewModel param)
        {
            id = ID;
            viewModel = param;
            Trip = DatabaseAccess.LoadSingleTrip(id);
            Expenses = Trip.expensesList;
           
        }
        public void test()
        {
            ExpenseModel temp = new ExpenseModel();
            Trip.AddExpense(temp);
        }
    }
}
