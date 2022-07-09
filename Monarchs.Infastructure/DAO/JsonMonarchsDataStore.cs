using Monarchs.Common.Interfaces;
using Monarchs.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Infastructure.DAO
{
    public class JsonMonarchsDataStore : IMonarchsDataStore
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly string _jsonFilePath;

        public JsonMonarchsDataStore(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
            _jsonFilePath = _connectionStringProvider.GetConnectiongString();
        }

        public async Task<int?> GetNumberOfMonarchs()
        {
            IEnumerable<MonarchJsonModel>? allMonarchs = null;

            if (!string.IsNullOrEmpty(_jsonFilePath) && File.Exists(_jsonFilePath))
            {
                await Task.Run(() =>
                {
                    var retrievedMonarchs = JsonConvert.DeserializeObject<IEnumerable<MonarchJsonModel>>(File.ReadAllText(_jsonFilePath));
                    if (retrievedMonarchs != null)
                        allMonarchs = retrievedMonarchs;               
                });
            }

            return allMonarchs?.Count();
        }
    }
}
