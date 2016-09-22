using Authy.Net;
using System.Configuration;

namespace Authy_ADFS
{
    public class AUTHYAuthenticationAdapter
    {
        private AuthyClient authyClient = null;

        /// <summary>
        /// AuthyAPIKey referenced from configuration file. Must be valid for Authy access.
        /// </summary>
        private string AuthyID = ConfigurationManager.AppSettings["AuthyAPIKey"];

        /// <summary>
        /// AuthyTestMode referenced from configuration file. Enable or disable test mode. Production
        /// should be disabled by setting to false in configuration.
        /// </summary>
        private bool AuthyTestMode = bool.Parse(ConfigurationManager.AppSettings["AuthyTestMode"]);

        /// <summary>
        /// Returns results of validation for one-time-password (OTP) against a registered user.
        /// </summary>
        /// <param name="userIdInput"></param>
        /// <param name="otpInput"></param>
        /// <returns></returns>
        public bool IsOTPValid(string userIdInput, string otpInput)
        {
            try
            {
                // work here
                var response = authyClient.VerifyToken(userIdInput, otpInput);

                return response.Success;
            }
            catch
            {
                //return false for failures.
                return false;
            }
        }

        public bool RegisterUser(string userPhoneInput, string userEmailInput, int userCountryCodeInput)
        {
            try
            {
                var response = authyClient.RegisterUser(userEmailInput, userPhoneInput, userCountryCodeInput);
                if(response.Success)
                {
                    //record the ID in Datastore.


                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                throw;
            }

        }

        /// <summary>
        /// Instance defaults for class.
        /// </summary>
        public AUTHYAuthenticationAdapter()
        {
            authyClient = new AuthyClient(AuthyID, test: AuthyTestMode);
        }
    }
}