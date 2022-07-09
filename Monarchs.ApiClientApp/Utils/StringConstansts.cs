using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.ApiClientApp.Utils
{
    internal class OptionsConstansts
    {
        internal const string Login = "Login";
        internal const string GetNumberOfMonarchs = "GetNumberOfMonarchs";
        internal const string GetLongestRulingMonarch = "GetLongestRulingMonarch";
        internal const string GetLongestRulingHouse = "GetLongestRulingHouse";
        internal const string GetMostCommonFirstName = "GetMostCommonFirstName";
        internal const string Exit = "Exit";
    }

    internal class EnpointsConstansts
    {
        internal const string LoginEndpoint = "https://localhost:7068/Login";
        internal const string LongestRulingMonarchEndpoint = "https://localhost:7068/Monarchs/GetLongestRulingMonarch";
        internal const string NrOfMonarchsEndpoint = "https://localhost:7068/Monarchs/GetNumberOfMonarchs";
        internal const string LongestRulingHouseEndpoint = "https://localhost:7068/Monarchs/GetLongestRulingHouse";
        internal const string MostCommonFirstNameEndpoint = "https://localhost:7068/Monarchs/GetMostCommonFirstName";
    }
}
