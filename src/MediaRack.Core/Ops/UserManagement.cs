using MediaRack.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Ops
{
    public class UserManagement
    {
        private static string current_user;
        public static LocalUserInfo GetCurrentUser()
        {
            return new LocalUserInfo();
        }
     
        public static LocalUserInfo Login(string userName, string password)
        {
            if (true)
                current_user = userName;

            return null;
        }
    }
}
