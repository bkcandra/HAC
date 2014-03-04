using HealthyClub.Customer.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Customer.Web.Rewards
{
    public partial class Performance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Reward Login";
            if (WebSecurity.IsAuthenticated)
            {
             
                if (Session != null)
                {
                    Response.Redirect("UserAsses.aspx");
                }
                else
                    WebSecurity.Logout();
            }
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
            Label linkCOnfirmError = Login1.FindControl("linkCOnfirmError") as Label;
            if (WebSecurity.UserExists(Login1.UserName))
            {
                if (!WebSecurity.IsConfirmed(Login1.UserName))
                {
                    linkCOnfirmError.Visible = true;
                    Login1.FailureText = "";
                    e.Authenticated = false;
                }
                else
                {
                    CheckBox RememberMe = Login1.FindControl("RememberMe") as CheckBox;
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
            else
            {
                e.Authenticated = false;
                linkCOnfirmError.Visible = false;
                Login1.FailureText = "Username is not registered";
            }
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            var dr = new CustomerDAC().RetrieveUserProfiles(new MembershipHelper().GetProviderUserKey(Login1.UserName));
            if (dr != null)
            {
                Session[SystemConstants.ses_FName] = dr.FirstName;
                Session[SystemConstants.ses_LName] = dr.LastName;
                Session[SystemConstants.ses_Role] = SystemConstants.CustomerRole;
                Session[SystemConstants.ses_Email] = dr.Email;
                var drr = new CustomerDAC().RetrieveUserRewardDetails(dr.FirstName);
                if (drr != null)
                {
                    Session[SystemConstants.ses_Rwdpts] = Convert.ToString(drr.RewardPoint);
                }
            }
            else
            {
                var drp = new CustomerDAC().RetrieveProviderProfiles(new MembershipHelper().GetProviderUserKey(Login1.UserName));
                if (drp != null)
                {
                    Session[SystemConstants.ses_FName] = drp.FirstName;
                    Session[SystemConstants.ses_LName] = drp.LastName;
                    Session[SystemConstants.ses_Role] = SystemConstants.ProviderRole;
                    Session[SystemConstants.ses_Email] = drp.Email;
                }
            }
            Response.Redirect("UserAsses.aspx");
        }


    }
}