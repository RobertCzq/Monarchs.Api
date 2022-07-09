using Monarchs.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Infastructure.Providers
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _connectionString;


        public ConnectionStringProvider(string connectionString)
        {
            _connectionString = ProcessConnectionString(connectionString);
        }

        public string GetConnectiongString()
        {
            return _connectionString;
        }

        private static string ProcessConnectionString(string connectionString)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, connectionString);
        }
    }
}
