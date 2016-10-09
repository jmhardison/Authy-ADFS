using System;
using Authy_ADFS.DataStores.Model;
using System.Configuration;

namespace Authy_ADFS.DataStores
{
    public class DataStore
    {
        //Publics

        /// <summary>
        /// Get's user registration from datastore by phone number.
        /// </summary>
        /// <param name="inputUserPhone"></param>
        /// <returns></returns>
        public AuthyRegisteredUser GetUserRegistration(string inputUserPhone, string inputUserEmail)
        {
            try
            {
                var inputFix = new AuthyRegisteredUser();
                inputFix.UserPhone = inputUserPhone != null ? inputUserPhone : string.Empty;
                inputFix.UserEmail = inputUserEmail != null ? inputUserEmail : string.Empty;

                var response = this._GetUserRegistration(inputFix);

                return response;
            }
            catch
            {
                throw;
            }
            
                 
        }

        /// <summary>
        /// Get's user registration from datastore by provided AuthyRegisteredUser type.
        /// </summary>
        /// <param name="inputUserRegistration"></param>
        /// <returns></returns>
        public AuthyRegisteredUser GetUserRegistration(AuthyRegisteredUser inputUserRegistration)
        {
            try
            {
                var response = this._GetUserRegistration(inputUserRegistration);

                return response;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Registers user in datastore by provided inputs for email, phone, and userID.
        /// </summary>
        /// <param name="inputUserEmail"></param>
        /// <param name="inputUserPhone"></param>
        /// <param name="inputUserID"></param>
        /// <returns></returns>
        public bool RegisterUser(string inputUserEmail, string inputUserPhone, string inputUserID)
        {
            try
            {
                //add logic to validate inputs are not null. This method expects them to be properly defined.
                var inputFix = new AuthyRegisteredUser();
                inputFix.UserEmail = inputUserEmail;
                inputFix.UserPhone = inputUserPhone;
                inputFix.UserID = inputUserID;

                var response = this._RegisterUser(inputFix);

                return response;

            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Registers user in datastore by provided input of AuthyRegisteredUser type.
        /// </summary>
        /// <param name="inputUserRegistration"></param>
        /// <returns></returns>
        public bool RegisterUser(AuthyRegisteredUser inputUserRegistration)
        {
            try
            {
                //add logic to validate inputs are not null. This method expects them to be properly defined.
                var response = this._RegisterUser(inputUserRegistration);

                return response;

            }
            catch
            {

                throw;
            }

        }

        /// <summary>
        /// Unregisters user in datastore by provided inputs of email, phone, and userid.
        /// </summary>
        /// <param name="inputUserEmail"></param>
        /// <param name="inputUserPhone"></param>
        /// <param name="inputUserID"></param>
        /// <returns></returns>
        public bool UnRegisterUser(string inputUserEmail, string inputUserPhone, string inputUserID)
        {
            try
            {
                //add logic to validate inputs are not null. This method expects them to be properly defined.
                var inputFix = new AuthyRegisteredUser();
                inputFix.UserEmail = inputUserEmail;
                inputFix.UserPhone = inputUserPhone;
                inputFix.UserID = inputUserID;

                var response = this._UnRegisterUser(inputFix);

                return response;

            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Unregisters user in datastore by provided input of AuthyRegisteredUser type.
        /// </summary>
        /// <param name="inputUserRegistration"></param>
        /// <returns></returns>
        public bool UnRegisterUser(AuthyRegisteredUser inputUserRegistration)
        {
            try
            {
                //add logic to validate inputs are not null. This method expects them to be properly defined.
                var response = this._UnRegisterUser(inputUserRegistration);

                return response;

            }
            catch
            {

                throw;
            }
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

        /// <summary>
        /// Private method to perform actual retrieval from datastore.
        /// </summary>
        /// <param name="inputUserRegistration"></param>
        /// <returns></returns>
        private AuthyRegisteredUser _GetUserRegistration(AuthyRegisteredUser inputUserRegistration)
        {
            try
            {
                //maybe add logic to determin if null, and if null throw error.
                var response = dataStoreInstance.GetRegisteredUser(inputUserRegistration);
                return response;

            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Private method to perform actual registration to datastore.
        /// </summary>
        /// <param name="inputUserRegistration"></param>
        /// <returns></returns>
        private bool _RegisterUser(AuthyRegisteredUser inputUserRegistration)
        {
            try
            {
                var response = dataStoreInstance.CheckandRegisterUser(inputUserRegistration);
                return response;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Private method to perform actual deletion from datastore.
        /// </summary>
        /// <param name="inputUserRegistration"></param>
        /// <returns></returns>
        private bool _UnRegisterUser(AuthyRegisteredUser inputUserRegistration)
        {
            try
            {
                var response = dataStoreInstance.DeleteRegisteredUser(inputUserRegistration);
                return response;
            }
            catch
            {
                throw;
            }

        }


        
    }
}
