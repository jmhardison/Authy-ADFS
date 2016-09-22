using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Net;
using Authy_ADFS.DataStores.Model;
using System.Linq;
using Authy_ADFS.DataStores.Interface;

namespace Authy_ADFS.DataStores.DocumentDB
{
    public class DocumentDBDataStore : IDataStoreOperations
    {
        //retrieve configuration for URI and KEY from application config.
        private string DocumentDBURI = ConfigurationManager.AppSettings["DocumentDBURI"];
        private string DocumentDBKey = ConfigurationManager.AppSettings["DocumentDBKey"];

        private DocumentClient documentDBClient;
        
        //these might be temporary for now, before release potentially extend them into configuration?
        private const string dbName = "authy-adfs-db";
        private const string collectionName = "usermappings";

        /// <summary>
        /// Checks if user is registered. If not, it will register the user.
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public bool CheckandRegisterUser(Authy_ADFS.DataStores.Model.AuthyRegisteredUser inputUser)
        {
            try
            {
                this.documentDBClient.ReadDocumentAsync(UriFactory.CreateDocumentUri(dbName, collectionName, inputUser.UserEmail));
                return true;
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    this.documentDBClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, collectionName), inputUser);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get and return the RegisteredUser object.
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public AuthyRegisteredUser GetRegisteredUser(AuthyRegisteredUser inputUser)
        {
            try
            {
                IQueryable<AuthyRegisteredUser> regUserQuery = this.documentDBClient.CreateDocumentQuery<AuthyRegisteredUser>(
                    UriFactory.CreateDocumentCollectionUri(dbName, collectionName), null).Where(item => item.UserEmail == inputUser.UserEmail);

                return regUserQuery.Single();
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("NOTFOUND");
                    
                }
                else
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// Deletes user from DocumentDB.
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public bool DeleteRegisteredUser(AuthyRegisteredUser inputUser)
        {
            try
            {
                //try the delete and return true.
                this.documentDBClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(dbName, collectionName, inputUser.UserEmail));
                return true;
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    //if not found throw not found exception.
                    throw new Exception("NOTFOUND");
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Establish connection to DocumentDB URI
        /// </summary>
        private void ConnectDocumentDB()
        {
            try
            {
                this.documentDBClient = new DocumentClient(new Uri(DocumentDBURI), DocumentDBKey);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Checks for the existence of 'authy-adfs-db' db and creates if not there.
        /// </summary>
        /// <returns></returns>
        private void CheckandCreateDatabase()
        {
            // Check to verify a database with the id=FamilyDB does not exist
            try
            {
                this.documentDBClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(dbName)).Wait();
            }
            catch (DocumentClientException de)
            {
                // If the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    this.documentDBClient.CreateDatabaseAsync(new Database { Id = dbName }).Wait();
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Checks for the existence of 'usermapping' collection and creates if not there.
        /// </summary>
        /// <returns></returns>
        private void CheckandCreateCollection()
        {
            try
            {
                this.documentDBClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(dbName, collectionName)).Wait();
            }
            catch (DocumentClientException de)
            {
                // If the document collection does not exist, create a new collection
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    DocumentCollection collectionInfo = new DocumentCollection();
                    collectionInfo.Id = collectionName;

                    // Configure collections for maximum query flexibility including string range queries.
                    collectionInfo.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });

                    // Here we create a collection with 400 RU/s.
                    this.documentDBClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(dbName),
                        collectionInfo,
                        new RequestOptions { OfferThroughput = 400 }).Wait();
                }
                else
                {
                    throw;
                }
            }
        }



        /// <summary>
        /// Instantiate and setup class.
        /// </summary>
        public DocumentDBDataStore()
        {
            //connect db first
            ConnectDocumentDB();
            //check and create database
            CheckandCreateDatabase();
            //check and create collection
            CheckandCreateCollection();
        }
    }
}