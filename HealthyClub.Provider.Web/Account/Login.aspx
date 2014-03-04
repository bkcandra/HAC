<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HealthyClub.Provider.Web.Account.Login" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="padding: 20px;">
        <p class="pageTitle">Sign in or Register as an Activity Provider </p>
        <section id="loginForm">
            <asp:Login ID="Login1" runat="server" ViewStateMode="Disabled" RenderOuterTable="false" OnAuthenticate="Login1_Authenticate" OnLoggedIn="Login1_LoggedIn"> 
                <LayoutTemplate>
                    <asp:Panel ID="pnlDefaultButton" runat="server" DefaultButton="LoginButton" Width="434px">
                        <span class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                        </span>
                        <br />
                        <asp:Label ID="linkCOnfirmError" runat="server" class="failureNotification" Visible="false">You have not confirmed your account. Click <a href="Confirm.aspx">here</a> to resend confirmation email.</asp:Label>
                        <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                            ValidationGroup="LoginUserValidationGroup" />
                        <div class="accountInfo">
                            <fieldset class="login">
                                <legend><a style="color: #102463;">Login Information</a></legend>
                                <p>
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username</asp:Label>
                                    <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                    <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:CheckBox ID="RememberMe" runat="server" />
                                    <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                                </p>
                            </fieldset>
                        </div>
                        <div class="submitButton">
                            <div style="float: left">
                                <p>
                                    <asp:HyperLink ID="hlnkforgot" runat="server" NavigateUrl="~/Account/ForgotPassword.aspx"
                                        Text="Forgot Your Password?" /><br />
                                    Don't have an account?
                               <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled" NavigateUrl="~/Registration/">Join now</asp:HyperLink>
                                    &nbsp;
                                </p>
                            </div>
                            <div style="float: right">
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" CssClass="button button-login"
                                    ValidationGroup="LoginUserValidationGroup" />
                            </div>
                        </div>
                    </asp:Panel>
                </LayoutTemplate>
            </asp:Login>

        </section>
    </div>
</asp:Content>
