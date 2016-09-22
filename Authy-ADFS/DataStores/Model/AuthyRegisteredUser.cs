namespace Authy_ADFS.DataStores.Model
{
    /// <summary>
    /// Structure of an Authy Registered User stored in the datastores.
    /// </summary>
    public class AuthyRegisteredUser
    {
        /// <summary>
        /// This represents the authy returned ID for the user that we must store.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// This represents the users email address as registered with Authy. UPN from AD for
        /// example, which is the preferred sign on method.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// This represents the users phone number that is valid within authy.
        /// </summary>
        public string UserPhone { get; set; }
    }
}