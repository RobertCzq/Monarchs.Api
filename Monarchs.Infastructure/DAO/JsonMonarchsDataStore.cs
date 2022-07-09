using Monarchs.Common.Interfaces;
using Monarchs.Common.Models;
using Monarchs.Common.Utils;
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

        public async Task<IEnumerable<Monarch>> GetAll()
        {
            var allMonarchs = new List<Monarch>();

            if (!string.IsNullOrEmpty(_jsonFilePath) && File.Exists(_jsonFilePath))
            {
                await Task.Run(() =>
                {
                    var retrievedMonarchsJsonModels = JsonConvert.DeserializeObject<IEnumerable<MonarchJsonModel>>(File.ReadAllText(_jsonFilePath));
                    if (retrievedMonarchsJsonModels != null)
                    {
                        var converter = new MonarchConverter();
                        foreach (var monarchJsonModel in retrievedMonarchsJsonModels)
                        {
                            allMonarchs.Add(converter.GetMonarchFromJsonModel(monarchJsonModel));
                        }
                    }
                });
            }

            return allMonarchs;
        }
    }
}
