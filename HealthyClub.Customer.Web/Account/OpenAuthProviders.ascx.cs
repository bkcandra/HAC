using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNet.Membership.OpenAuth;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

namespace HealthyClub.Customer.Web.Account
{
    public partial class OpenAuthProviders : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                var providerName = Request.Form["provider"];
                if (providerName == null)
                {
                    return;
                }

                var redirectUrl = "~/Account/RegisterExternalLogin.aspx";
                if (!String.IsNullOrEmpty(ReturnUrl))
                {
                    var resolvedReturnUrl = ResolveUrl(ReturnUrl);
                    redirectUrl += "?ReturnUrl=" + HttpUtility
                          .UrlEncode(resolvedReturnUrl);
                }

                var provider = OpenAuth.AuthenticationClients
                    .GetByProviderName(providerName);
                if (provider == null)
                {
                    provider = OpenAuth.AuthenticationClients
                       .GetByProviderName(providerName.ToLowerInvariant());
                    if (providerName == null)
                    {
                        throw new InvalidOperationException(string.Format(
                        "The provider '{0}' has not been registered with {2}." +
                        "This is likely an error with AuthConfig.cs settings",
                           Server.HtmlEncode(providerName),
                           typeof(OpenAuth).FullName));
                    }

                    providerName = providerName.ToLowerInvariant();
                }

                OpenAuth.RequestAuthentication(providerName, redirectUrl);
            }
        }



        public string ReturnUrl { get; set; }


        public IEnumerable<ProviderDetails> GetProviderNames()
        {
            return OpenAuth.AuthenticationClients.GetAll();
        }

    }
}