using Microsoft.IdentityServer.Web.Authentication.External;
using System.Collections.Generic;

namespace Authy_ADFS
{
    internal class ADFSAuthenticationMetadataAdapter : IAuthenticationAdapterMetadata
    {
        /// <summary>
        /// Return the string to represent the custom module within ADFS to the administrator. Not
        /// end user visible. Pulling assembly version to append to string.
        /// </summary>
        public string AdminName
        {
            get
            {
                return ("Authy-ADFS v" + typeof(ADFSAuthenticationMetadataAdapter).Assembly.GetName().Version.ToString());
            }
        }

        /// <summary>
        /// Returns the auth method for One Time Password, which is what will be used for Authy token generation.
        /// </summary>
        public string[] AuthenticationMethods
        {
            get
            {
                return new string[] { "http://schemas.microsoft.com/ws/2012/12/authmethod/otp" };
            }
        }

        public int[] AvailableLcids
        {
            get
            {
                return new int[] { 1033 };
            }
        }

        public Dictionary<int, string> Descriptions
        {
            get
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                result.Add(1033, "Authy-ADFS Descriptions");
                return result;
            }
        }

        public Dictionary<int, string> FriendlyNames
        {
            get
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                result.Add(1033, "Authy-ADFS Friendlynames");
                return result;
            }
        }

        public string[] IdentityClaims
        {
            get
            {
                return new string[] { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn" };
            }
        }

        public bool RequiresIdentity
        {
            get
            {
                return true;
            }
        }

        public ADFSAuthenticationMetadataAdapter()
        {
        }
    }
}