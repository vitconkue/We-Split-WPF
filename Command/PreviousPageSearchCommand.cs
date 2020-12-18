using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using We_Split_WPF.Helper;
using We_Split_WPF.ViewModel;

namespace We_Split_WPF.Command
{
    class PreviousPageSearchCommand : ICommand
    {
        private SearchPageViewModel _viewModel;

        public SearchPageViewModel ViewModel { get => _viewModel; set => _viewModel = value; }



        public PreviousPageSearchCommand(SearchPageViewModel viewModel)
        {
            _viewModel = viewModel;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return true;
            }
            var pagingVar = parameter as Paging;
            bool result = true;

            if (pagingVar.CurrentPage == 1)
            {
                result = false;
            }
            return result;
        }

        public void Execute(object parameter)
        {
            _viewModel.GoToPreviousPage();
        }
    }
}