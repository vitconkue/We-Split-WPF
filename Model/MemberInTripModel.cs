using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split_WPF.Model
{
    class MemberInTripModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int MoneyPaid { get; set; } = 0; 

        public void ShowConsole()
        {
            Console.WriteLine($"Member ID: {ID},Member Name: {Name},  Money Paid: {MoneyPaid}");
        }
    }
}
