using System;
using Authy_ADFS.DataStores.Model;
using System.Configuration;

namespace Authy_ADFS.DataStores
{
    public class DataStore
    {
        //Publics


        public AuthyRegisteredUser GetUserRegistration(string inputUserPhone)
        {
              
        }

        public AuthyRegisteredUser GetUserRegistration(string inputUserEmail)
        {
       
        }

        public AuthyRegisteredUser GetUserRegistration(AuthyRegisteredUser inputUserRegistration)
        {
           
        }

        public bool RegisterUser(string inputUserEmail, string inputUserPhone, string inputUserID)
        {

        }

        public bool RegisterUser(AuthyRegisteredUser inputUserRegistration)
        {

        }

        public bool UnRegisterUser(string inputUserEmail, string inputUserPhone, string inputUserID)
        {

        }

        public bool UnRegisterUser(AuthyRegisteredUser inputUserRegistration)
        {

        }

        //Privates
        private Interface.IDataStoreOperations dataStoreInstance;


        /// <summary>
        /// Configures and initializes the data store object to the appropriate configured datastore.
        /// </summary>
        private void _ConfigureDataStore()
        {
            try
            {

                switch (int.Parse(ConfigurationManager.AppSettings["DataStore"]))
                {
                    case 0:
                        //DocumentDB
                        dataStoreInstance = new DocumentDB.DocumentDBDataStore();
                        break;

                    case 1:
                        //Active Directory
                        dataStoreInstance = new ActiveDirectory.ActiveDirectoryDataStore();
                        break;

                    case 2:
                        //azure sql or sql
                        dataStoreInstance = new SQL.SQLDataStore();
                        break;

                    default:
                        //nothing? Should throw error instead of break.
                        throw new Exception("Invalid DataStore type selected.");
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        private AuthyRegisteredUser _GetUserRegistration(AuthyRegisteredUser inputUserRegistration)
        {
            try
            {
                
                

            }
            catch
            {
                throw;
            }

        }

        private bool _RegisterUser(AuthyRegisteredUser inputUserRegistration)
        {

        }

        private bool _UnRegisterUser(AuthyRegisteredUser inputUserRegistration)
        {

        }


        
    }
}
