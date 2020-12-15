using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using We_Split_WPF.ViewModel;

namespace We_Split_WPF.Command
{
    public class UpdateTripCommand:ICommand
    {
        private MainViewModel viewModel;
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Update")
            {
                viewModel.SelectedViewModel = new UpdatePageViewModel();
            }
        }
        public UpdateTripCommand(MainViewModel param)
        {
            this.viewModel = param;
        }
    }
}
