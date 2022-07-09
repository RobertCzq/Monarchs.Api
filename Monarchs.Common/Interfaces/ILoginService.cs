using Monarchs.Common.Models;
using Monarchs.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Common.Interfaces
{
    public interface ILoginService
    {
        string GenerateToken(UserModel user);
        UserModel Authenticate(UserLogin userLogin);
    }
}
