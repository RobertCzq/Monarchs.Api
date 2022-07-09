using Monarchs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.Infastructure.Constants
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "admin", EmailAddress = "admin@somemail.com", Password = "admin_PW", GivenName = "The", Surname = "Admin", Role = "Administrator" },
            new UserModel() { Username = "normal", EmailAddress = "normal@somemail.com", Password = "normal_PW", GivenName = "Normal", Surname = "User", Role = "Normal" },
        };
    }
}
