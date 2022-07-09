using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Common.ViewModels
{
    public class MonarchViewModel
    {
        public string FullName { get; set; }
        public int NrOfYearsRuled { get; set; }

        public MonarchViewModel (string fullName, int nrOfYearsRuled)
        {
            FullName = fullName;
            NrOfYearsRuled = nrOfYearsRuled;
        }
    }
}
