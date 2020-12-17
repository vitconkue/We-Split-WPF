using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using We_Split_WPF.ViewModel;

namespace We_Split_WPF.Command
{
    public class UpdateTripFromHomeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MainViewModel viewModel;
        private int id;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.SelectedViewModel = new UpdatePageViewModel(int.Parse(parameter.ToString()), viewModel);
        }
        public UpdateTripFromHomeCommand(MainViewModel param)
        {
            this.viewModel = param;
           
        }
    }
}
