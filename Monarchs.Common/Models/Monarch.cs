using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Common.Models
{
    public class Monarch
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string House { get; set; }
        public int NrOfYearsRuled { get; set; }

        public Monarch(int id, string firstName, string fullName, string country, string house, int nrOfYearsRuled) 
        {
            Id = id;
            FirstName = firstName;
            FullName = fullName;
            Country = country;
            House = house;
            NrOfYearsRuled = nrOfYearsRuled;
        }
    }
}
