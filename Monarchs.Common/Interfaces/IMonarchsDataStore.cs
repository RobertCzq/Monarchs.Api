using Monarchs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Common.Interfaces
{
    public interface IMonarchsDataStore
    {
        Task<IEnumerable<Monarch>> GetAll();
    }
}
