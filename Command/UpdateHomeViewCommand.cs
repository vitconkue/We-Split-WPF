using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using We_Split_WPF.ViewModel;

namespace We_Split_WPF.Command
{
    public class UpdateHomeViewCommand : ICommand
    {

        private MainViewModel ViewModel;
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            ViewModel.SelectedViewModel = new DetailPageViewModel(int.Parse(parameter.ToString()));
        }
        public UpdateHomeViewCommand(MainViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }
    }
}
