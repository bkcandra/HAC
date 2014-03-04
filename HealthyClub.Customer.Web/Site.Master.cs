using HealthyClub.Customer.DA;
using HealthyClub.Customer.EDS;
using HealthyClub.Customer.BF;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace HealthyClub.Customer.Web
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetLinkTarget();
            }

            if (WebSecurity.IsAuthenticated)
            {
                LoginName loginName = HeadLoginView.FindControl("HeadLoginName") as LoginName;
                Guid UserID = new CustomerDAC().RetrieveUserGUID(WebSecurity.CurrentUserName);

                if (!string.IsNullOrEmpty((string)Session[SystemConstants.ses_FName]))
                {
                    loginName.FormatString = (string)Session[SystemConstants.ses_FName];
                }
                else
                {
                    var dr = new CustomerDAC().RetrieveUserProfiles(UserID);
                    if (dr != null)
                    {

                        Session[SystemConstants.ses_FName] = dr.FirstName;
                        Session[SystemConstants.ses_LName] = dr.LastName;
                        Session[SystemConstants.ses_Role] = SystemConstants.CustomerRole;
                        Session[SystemConstants.ses_Email] = dr.Email;
                        Session[SystemConstants.ses_UserID] = dr.UserID;
                        var drr = new CustomerDAC().RetrieveUserRewardDetails(dr.FirstName);
                        if (drr != null)
                        {
                            Session[SystemConstants.ses_Rwdpts] = Convert.ToString(drr.RewardPoint);
                        }
                    }
                    else
                    {
                        var drp = new CustomerDAC().RetrieveProviderProfiles(UserID);
                        if (drp != null)
                        {
                            WebSecurity.Logout();
                        }
                    }
                }
                loginName.FormatString = (string)Session[SystemConstants.ses_FName];
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                if (!path.Contains("Redeem"))
                    updatecartfilter();
                string pts = (String)(Session[SystemConstants.ses_Rwdpts]);
                //HyperLink hlnkRewards = HeadLoginView.FindControl("hlnkRewards") as HyperLink;

                loginName.FormatString = (string)Session[SystemConstants.ses_FName] +" (" + pts + " pts)";




            }

        }
        public void updatecartfilter()
        {
            int count = RewardCart.Instance.getQuantity();
            if (count > 0)
            {
                HyperLink cartitem = HeadLoginView.FindControl("Cart") as HyperLink;

                cartitem.Text = "My Cart(" + count + ")" + "[Checkout]";
                cartitem.NavigateUrl = "~/Rewards/Redeem.aspx";
            }
        }
        protected void btnProviders_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SystemConstants.ProviderUrl + "Account/SignIn.aspx");
        }

        protected void btnMembers_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        private void SetLinkTarget()
        {

            CustomerEDSC.v_MenuDTDataTable dt = new CustomerDAC().RetrieveMenuExplorers((int)SystemConstants.MenuType.MemberMenu);
            CustomerEDSC.PageDTDataTable dtPages = new CustomerDAC().RetrievePages();

            int x = 0;
            foreach (CustomerEDSC.v_MenuDTRow drParent in dt)
            {
                if (drParent.LinkType == 1)
                {
                    x++;
                    if (drParent.ParentMenuID == 0 && drParent.LinkText != null)
                    {
                        MenuItem menu = new MenuItem(drParent.LinkText, x.ToString(), "", "~/Activity/Default.aspx?" + SystemConstants.CategoryID + "=" + drParent.LinkValue, "");
                        MenuNavigation.Items.Add(menu);
                        foreach (CustomerEDSC.v_MenuDTRow drChild in dt)
                        {
                            if (drChild.ParentMenuID == drParent.ID && drChild.LinkText != null && drChild.LinkType != 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", "~/Activity/Default.aspx?" + SystemConstants.CategoryID + "=" + drChild.LinkValue, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                            else if (drChild.ParentMenuID == drParent.ID && drChild.LinkType == 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", drChild.LinkValue, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                        }
                    }
                }
                else if (drParent.LinkType == 2)
                {
                    x++;
                    if (drParent.ParentMenuID == 0 && drParent.LinkText != null)
                    {
                        MenuItem menu = new MenuItem(drParent.LinkText, x.ToString(), "", "~/Activity/Default.aspx?" + SystemConstants.ProviderID + "=" + drParent.LinkValue, "");
                        MenuNavigation.Items.Add(menu);
                        foreach (CustomerEDSC.v_MenuDTRow drChild in dt)
                        {
                            if (drChild.ParentMenuID == drParent.ID && drChild.LinkText != null && drChild.LinkType != 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", "~/Activity/Default.aspx?" + SystemConstants.ProviderID + "=" + drChild.LinkValue, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                            else if (drChild.ParentMenuID == drParent.ID && drChild.LinkType == 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", drChild.LinkValue, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                        }
                    }
                }
                else if (drParent.LinkType == 3)
                {
                    x++;
                    if (drParent.ParentMenuID == 0)
                    {
                        MenuItem menu = new MenuItem(drParent.LinkText, x.ToString(), "", "~/Activity/Default.aspx??" + SystemConstants.ActivityID + "=" + drParent.LinkValue, "");
                        MenuNavigation.Items.Add(menu);
                        foreach (CustomerEDSC.v_MenuDTRow drChild in dt)
                        {
                            if (drChild.ParentMenuID == drParent.ID && drChild.LinkType != 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", "~/Activity/Default.aspx?" + SystemConstants.ActivityID + "=" + drChild.LinkValue, " ");
                                menu.ChildItems.Add(ChildMenu);
                            }
                            else if (drChild.ParentMenuID == drParent.ID && drChild.LinkType == 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", drChild.LinkValue, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                        }
                    }
                }

                else if (drParent.LinkType == 4)
                {


                    var PageName = dtPages
                                   .Where(p => p.ID == Convert.ToInt32(drParent.LinkValue)).FirstOrDefault();
                    if (PageName == null)
                    {
                        PageName = new CustomerEDSC.PageDTDataTable().NewPageDTRow();
                        PageName.Name = "NotFound";
                    }

                    x++;
                    if (drParent.ParentMenuID == 0)
                    {

                        MenuItem menu = new MenuItem(drParent.LinkText, x.ToString(), "", "~/Pages/" + PageName.Name, "");
                        MenuNavigation.Items.Add(menu);
                        foreach (CustomerEDSC.v_MenuDTRow drChild in dt)
                        {

                            if (drChild.ParentMenuID == drParent.ID && drChild.LinkType != 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", "~/Pages/" + PageName.Name, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                            else if (drChild.ParentMenuID == drParent.ID && drChild.LinkType == 6)
                            {
                                if (drChild.LinkType == 4)
                                {
                                    var ChildPageName = dtPages
                                     .Where(p => p.ID == Convert.ToInt32(drChild.LinkValue)).FirstOrDefault();
                                    if (ChildPageName == null)
                                    {
                                        ChildPageName = new CustomerEDSC.PageDTDataTable().NewPageDTRow();
                                        ChildPageName.Name = "NotFound";
                                    }
                                    MenuItem ChildMenuPage = new MenuItem(drChild.LinkText, "", "", ChildPageName.Name, "");
                                    menu.ChildItems.Add(ChildMenuPage);
                                }
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", drChild.LinkValue, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                        }

                    }
                }
                else if (drParent.LinkType == 5)
                {
                    var PageName = dtPages
                                .Where(p => p.ID == Convert.ToInt32(drParent.LinkValue)).FirstOrDefault();
                    if (PageName == null)
                    {
                        PageName = new CustomerEDSC.PageDTDataTable().NewPageDTRow();
                        PageName.Name = "NotFound";
                    }
                    x++;
                    if (drParent.ParentMenuID == 0)
                    {
                        MenuItem menu = new MenuItem(drParent.LinkText, x.ToString(), "", "~/Pages/" + PageName.Name, "");
                        MenuNavigation.Items.Add(menu);
                        foreach (CustomerEDSC.v_MenuDTRow drChild in dt)
                        {
                            var ChildPageName = dtPages
                                    .Where(p => p.ID == Convert.ToInt32(drChild.LinkValue)).FirstOrDefault();
                            if (ChildPageName == null)
                            {
                                ChildPageName = new CustomerEDSC.PageDTDataTable().NewPageDTRow();
                                ChildPageName.Name = "NotFound";
                            }
                            if (drChild.ParentMenuID == drParent.ID && drChild.LinkType != 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", "~/Pages/" + ChildPageName.Name, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                            else if (drChild.ParentMenuID == drParent.ID && drChild.LinkType == 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", ChildPageName.Name, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                        }

                    }

                }
                else if (drParent.LinkType == 6)
                {
                    x++;
                    if (drParent.ParentMenuID == 0)
                    {
                        MenuItem menu = new MenuItem(drParent.LinkText, x.ToString(), "", drParent.LinkValue, "");
                        MenuNavigation.Items.Add(menu);
                        foreach (CustomerEDSC.v_MenuDTRow drChild in dt)
                        {

                            if (drChild.ParentMenuID == drParent.ID && drChild.LinkType != 6)
                            {
                                var ChildPageName = dtPages
                                   .Where(p => p.ID == Convert.ToInt32(drChild.LinkValue)).FirstOrDefault();
                                if (ChildPageName == null)
                                {
                                    ChildPageName = new CustomerEDSC.PageDTDataTable().NewPageDTRow();
                                    ChildPageName.Name = "NotFound";
                                }
                                if (drChild.ParentMenuID == drParent.ID && drChild.LinkType != 6)
                                {
                                    MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", "~/Pages/" + ChildPageName.Name, "");
                                    menu.ChildItems.Add(ChildMenu);
                                }
                                else if (drChild.ParentMenuID == drParent.ID && drChild.LinkType != 6)
                                {
                                    MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", "~/Pages/" + drChild.LinkValue, "");
                                    menu.ChildItems.Add(ChildMenu);
                                }

                            }
                            else if (drChild.ParentMenuID == drParent.ID && drChild.LinkType == 6)
                            {
                                MenuItem ChildMenu = new MenuItem(drChild.LinkText, "", "", drChild.LinkValue, "");
                                menu.ChildItems.Add(ChildMenu);
                            }
                        }
                    }
                }
            }
            MenuItem Rewards = new MenuItem("Rewards Program", x.ToString(), "", "~/Rewards/RewardsHome");
            MenuNavigation.Items.Add(Rewards);
            x++;
            MenuItem ContactUs = new MenuItem("Contact Us", x.ToString(), "", "~/ContactUs");
            MenuNavigation.Items.Add(ContactUs);
        }

        protected void HeadLoginStatus_LoggedOut(object sender, EventArgs e)
        {
            WebSecurity.Logout();
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-1);
        }

    }
}