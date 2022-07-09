using FakeItEasy;
using Microsoft.Extensions.Logging;
using Monarchs.Api.Controllers;
using Monarchs.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Api.UnitTests.Utils
{
    internal static class Setup
    {
        internal static IMonarchsCache GetMockMonarchsCache()
        {
            var shiftCache = A.Fake<IMonarchsCache>();
            return shiftCache;
        }

        internal static ILogger<MonarchsController> GetMockMonarchsLogger()
        {
            var shiftCache = A.Fake<ILogger<MonarchsController>>();
            return shiftCache;
        }
    }
}
