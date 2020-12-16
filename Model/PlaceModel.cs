using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace We_Split_WPF.Model
{
    public class PlaceModel
    {
        public int TripID { get; set; }

        public int Number { get; set;  }

        public string Name { get; set; }
        
        public string Information { get; set;  } 

        public string DateStart { get; set;  }

        public string DateFinish { get; set;  }

    }
}
