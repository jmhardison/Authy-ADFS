using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authy_ADFS.DataStores.Model;

namespace Authy_ADFS.DataStores.Interface
{
    interface IDataStoreOperations
    {
        bool CheckandRegisterUser(AuthyRegisteredUser inputUser);
        AuthyRegisteredUser GetRegisteredUser(AuthyRegisteredUser inputUser);
        bool DeleteRegisteredUser(AuthyRegisteredUser inputUser);
    }
}
