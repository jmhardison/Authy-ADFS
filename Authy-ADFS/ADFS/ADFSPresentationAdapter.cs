using Microsoft.IdentityServer.Web.Authentication.External;
using System;

namespace Authy_ADFS
{
    internal class ADFSPresentationAdapter : IAdapterPresentation, IAdapterPresentationForm
    {
        private string message;
        private bool isPermanentFailure;

        public string GetFormHtml(int lcid)
        {
            string result = "";
            if (!String.IsNullOrEmpty(this.message))
            {
                result += "<p>" + message + "</p>";
            }
            if (!this.isPermanentFailure)
            {
                result += "<form method=\"post\" id=\"loginForm\" autocomplete=\"off\">";
                result += "PIN: <input id=\"pin\" name=\"pin\" type=\"password\" />";
                result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
                result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
                result += "<input id=\"continueButton\" type=\"submit\" name=\"Continue\" value=\"Continue\" />";
                result += "</form>";
            }
            return result;
        }

        public string GetFormPreRenderHtml(int lcid)
        {
            return string.Empty;
        }

        public string GetPageTitle(int lcid)
        {
            return "Authy - ADFS";
        }

        public ADFSPresentationAdapter()
        {
            this.message = string.Empty;
            this.isPermanentFailure = false;
        }

        public ADFSPresentationAdapter(string message, bool isPermanentFailure)
        {
            this.message = message;
            this.isPermanentFailure = isPermanentFailure;
        }
    }
}