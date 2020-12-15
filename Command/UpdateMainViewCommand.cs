using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using We_Split_WPF.ViewModel;

namespace We_Split_WPF.Command
{
    public class UpdateMainViewCommand : ICommand
    {
        private MainViewModel ViewModel;
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public UpdateMainViewCommand(MainViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "SearchPage")
            {
                ViewModel.SelectedViewModel = new SearchPageViewModel();
            }
            if (parameter.ToString() == "HomePage")
            {
                ViewModel.SelectedViewModel = new HomePageViewModel(ViewModel);
            }
        }
       
    }
}
