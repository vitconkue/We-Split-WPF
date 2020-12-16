using System;
using System.Collections.Generic;
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
    public class ClickButtonViewModel
    {
        public ClickButtonViewModel()
        {
            MVVMClick = new RelayCommand(new Action<object>(ShowMessage));
        }

        private ICommand m_ButtonCommand;

        public ICommand MVVMClick
        {
            get { return m_ButtonCommand; }
            set { m_ButtonCommand = value; }
        }

        public void ShowMessage(object obj)
        {
            MessageBox.Show("Test");
        }

    }
}
