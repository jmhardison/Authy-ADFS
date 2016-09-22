using Microsoft.IdentityServer.Web.Authentication.External;
using System.Net;
using System.Security.Claims;

namespace Authy_ADFS
{
    /// <summary>
    /// ADFS Authentication Adapter
    /// </summary>
    internal class ADFSAuthenticationAdapter : IAuthenticationAdapter
    {
        /// <summary>
        /// Return Metadata Adapter.
        /// </summary>
        public IAuthenticationAdapterMetadata Metadata
        {
            get
            {
                return new ADFSAuthenticationMetadataAdapter();
            }
        }

        /// <summary>
        /// BeginAuthentication returns a new ADFS Presentation Adapter.
        /// </summary>
        /// <param name="identityClaim"></param>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IAdapterPresentation BeginAuthentication(Claim identityClaim, HttpListenerRequest request, IAuthenticationContext context)
        {
            return new ADFSPresentationAdapter();
        }

        /// <summary>
        /// Checks if OTP is availabe to be performed for the given user.
        /// </summary>
        /// <param name="identityClaim"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool IsAvailableForUser(Claim identityClaim, IAuthenticationContext context)
        {
            //need to adjust this probably
            return true;
        }

        /// <summary>
        /// Not Used
        /// </summary>
        /// <param name="configData"></param>
        public void OnAuthenticationPipelineLoad(IAuthenticationMethodConfigData configData)
        {
            //not used
        }

        /// <summary>
        /// Not Used
        /// </summary>
        public void OnAuthenticationPipelineUnload()
        {
            //not used
        }

        /// <summary>
        /// Returns error to screen.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public IAdapterPresentation OnError(HttpListenerRequest request, ExternalAuthenticationException ex)
        {
            return new ADFSPresentationAdapter(ex.Message, true);
        }

        /// <summary>
        /// Performs authentication step after user is challenged for pin code.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="proofData"></param>
        /// <param name="request"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public IAdapterPresentation TryEndAuthentication(IAuthenticationContext context, IProofData proofData, HttpListenerRequest request, out Claim[] claims)
        {
            claims = null;
            IAdapterPresentation result = null;
            string userPIN = proofData.Properties["pin"].ToString();

            Authy_ADFS.AUTHYAuthenticationAdapter authyAdapter = new AUTHYAuthenticationAdapter();

            //add authy code and perform lookup to their service.
            if (authyAdapter.IsOTPValid(proofData.Properties["Email"].ToString(), userPIN))
            {
                System.Security.Claims.Claim claim = new System.Security.Claims.Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "http://schemas.microsoft.com/ws/2012/12/authmethod/otp");
                claims = new System.Security.Claims.Claim[] { claim };
            }
            else
            {
                result = new ADFSPresentationAdapter("Authentication failed.", false);
            }
            return result;
        }
    }
}