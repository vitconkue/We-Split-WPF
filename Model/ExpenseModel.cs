using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split_WPF.Model
{
    class ExpenseModel
    {
        public int TripID { get; set; }

        public int Number { get; set; }
        public string Name { get; set;  }

        public int AmountMoney { get; set; }

        public void ShowConsole()
        {
            Console.WriteLine($" Expense  #: {Number},Expense Name: {Name}, AmountMoney: {AmountMoney}");
        }
    }
}
