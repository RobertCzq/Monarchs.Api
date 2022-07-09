using Monarchs.Common.Interfaces;
using Monarchs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Infastructure.Services
{
    public class MonarchsCache : IMonarchsCache
    {
        private readonly IMonarchsDataStore _dataStore;
        private readonly ICacheProvider _cacheProvider;
        private const string _shiftsCacheKey = "_monarchs";
        private static readonly SemaphoreSlim _monarchsSemaphore = new SemaphoreSlim(1, 1);

        public MonarchsCache(IMonarchsDataStore dataStore, ICacheProvider cacheProvider)
        {
            _dataStore = dataStore;
            _cacheProvider = cacheProvider;

        }
        public async Task<IEnumerable<Monarch>> GetAll()
        {
            var monarchs = _cacheProvider.GetFromCache<IEnumerable<Monarch>>(_shiftsCacheKey);

            if (monarchs != null)
                return monarchs;

            try
            {
                await _monarchsSemaphore.WaitAsync();
                monarchs = _cacheProvider.GetFromCache<IEnumerable<Monarch>>(_shiftsCacheKey);
                if (monarchs != null)
                {
                    return monarchs;
                }
                monarchs = await _dataStore.GetAll();
                _cacheProvider.SetCache(_shiftsCacheKey, monarchs, DateTimeOffset.Now.AddHours(1));
            }
            finally
            {
                _monarchsSemaphore.Release();
            }

            return monarchs;
        }
    }
}
