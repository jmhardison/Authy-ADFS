using System;
using Authy_ADFS.DataStores.Interface;
using Authy_ADFS.DataStores.Model;

namespace Authy_ADFS.DataStores.ActiveDirectory
{
    internal class ActiveDirectoryDataStore : IDataStoreOperations
    {
        public bool CheckandRegisterUser(AuthyRegisteredUser inputUser)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRegisteredUser(AuthyRegisteredUser inputUser)
        {
            throw new NotImplementedException();
        }

        public AuthyRegisteredUser GetRegisteredUser(AuthyRegisteredUser inputUser)
        {
            throw new NotImplementedException();
        }
    }
}