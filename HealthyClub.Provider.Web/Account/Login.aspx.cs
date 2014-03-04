using HealthyClub.Provider.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Provider.Web.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink RegisterHyperLink = Login1.FindControl("RegisterHyperLink") as HyperLink;

            RegisterHyperLink.NavigateUrl = "~/Registration";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            CheckBox RememberMe = Login1.FindControl("RememberMe") as CheckBox;
            Label linkCOnfirmError = Login1.FindControl("linkCOnfirmError") as Label;

            if (WebSecurity.UserExists(Login1.UserName))
            {
                if (!WebSecurity.IsConfirmed(Login1.UserName))
                {
                    linkCOnfirmError.Visible = true;
                    e.Authenticated = false;
                    Login1.FailureText = "";
                }
                else
                {
                    if (!Roles.IsUserInRole(Login1.UserName, SystemConstants.ProviderRole))
                    {
                        e.Authenticated = false;
                        Login1.FailureText = "Incorrect username for Activity Provider account";
                    }
                    else
                    {
                        if (WebSecurity.Login(Login1.UserName, Login1.Password, RememberMe.Checked))
                        {
                            e.Authenticated = true;
                        }
                        else
                        {
                            e.Authenticated = false;
                            Login1.FailureText = "Incorrect username or password";
                        }
                    }
                }
            }
            else
            {
                e.Authenticated = false;
                linkCOnfirmError.Visible = false;
                Login1.FailureText = SystemConstants.ErrorUserNotRegistered;
            }
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            var drp = new ProviderDAC().RetrieveProviderProfiles(new MembershipHelper().GetProviderUserKey(Login1.UserName));
            if (drp != null)
            {
                string name = drp.ProviderName;

                if (name.Length > 20)
                {
                    name = name.Remove(20);
                    name = name + "...";
                }
                Session[SystemConstants.ses_FName] = name;
                Session[SystemConstants.ses_LName] = "";
                Session[SystemConstants.ses_Role] = SystemConstants.ProviderRole;
                Session[SystemConstants.ses_Email] = drp.Email;
            }
            else
            {
                var dr = new ProviderDAC().RetrieveUserProfiles(new MembershipHelper().GetProviderUserKey(Login1.UserName));
                if (dr != null)
                {
                    WebSecurity.Logout();                    
                //    Session[SystemConstants.ses_FName] = dr.FirstName;
                //    Session[SystemConstants.ses_LName] = dr.LastName;
                //    Session[SystemConstants.ses_Role] = SystemConstants.CustomerRole;
                //    Session[SystemConstants.ses_Email] = dr.Email;
                }
            }
            Response.Redirect("~/Activities");
        }
    }
}