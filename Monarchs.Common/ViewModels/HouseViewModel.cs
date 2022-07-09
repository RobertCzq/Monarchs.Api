using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Common.ViewModels
{
    public class HouseViewModel
    {
        public string House { get; set; }
        public int NrOfYearsRuled { get; set; }

        public HouseViewModel(string house, int nrOfYearsRuled)
        {
            House = house;
            NrOfYearsRuled = nrOfYearsRuled;
        }
    }
}
