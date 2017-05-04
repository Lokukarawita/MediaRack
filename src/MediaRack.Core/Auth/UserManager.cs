using MediaRack.Core.Data.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Auth
{
    public sealed class UserManager
    {
        private IRemoteStorage remote_data;

        public UserManager(IRemoteStorage storage)
        {
            this.remote_data = storage;
        }


        public void CheckUser(string username)
        {
            //remote_data.
        }

    }
}
