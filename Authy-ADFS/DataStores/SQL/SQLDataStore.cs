using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authy_ADFS.DataStores.Interface;
using Authy_ADFS.DataStores.Model;

namespace Authy_ADFS.DataStores.SQL
{
    class SQLDataStore : IDataStoreOperations
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
