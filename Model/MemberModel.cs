using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split_WPF.Model
{
   public class MemberModel
    {
        public int ID { get; set; }
        public string Name { get; set;  }

        public void ShowConsole()
        {
            Console.WriteLine($"Member ID: {ID},Member Name: {Name}"); 
        }
    }
}
