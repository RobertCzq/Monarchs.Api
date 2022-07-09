using Monarchs.Common.Interfaces;
using Monarchs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Common.Utils
{
    public class MonarchConverter : IMonarchConverter
    {
        public MonarchConverter() { }

        public Monarch GetMonarchFromJsonModel(MonarchJsonModel monarchJsonModel)
        {
            var firstName = GetFirstName(monarchJsonModel.Name);
            var nrOfYearsRuled = GetRullingYears(monarchJsonModel.Years);
            return new Monarch(monarchJsonModel.Id, firstName, monarchJsonModel.Name, monarchJsonModel.Country, monarchJsonModel.House, nrOfYearsRuled);
        }

        private string GetFirstName(string fullName)
        {
            var firstName = fullName.Split(" ").FirstOrDefault();
            return firstName ?? string.Empty;
        }

        private int GetRullingYears(string yearsString)
        {
            if (!string.IsNullOrEmpty(yearsString))
            {
                var years = yearsString.Split("-");
                if (years != null && years.Length == 2)
                {
                    var firstParsed = int.TryParse(years[0], out var firstYear);
                    var lastParsed = int.TryParse(years[1], out var lastYear);
                    var check = firstParsed && lastParsed;
                    if (check)
                        return lastYear - firstYear;
                    else if (firstParsed && !lastParsed)
                    {
                        //asssume it's current ruler
                        return DateTime.Now.Year - firstYear;
                    }
                }
                else
                {
                    var onlyOneYear = int.TryParse(yearsString, out var year);
                    if (onlyOneYear)
                        return 1;
                }
            }

            return 0;
        }
    }
}
