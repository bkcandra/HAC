<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerLoginUC.ascx.cs"
    Inherits="HealthyClub.Web.UserControls.CustomerLoginUC" %>

<asp:Panel ID="pnlDefaultButton" runat="server" DefaultButton="btnLogin">
</asp:Panel>
<div class="accountInfo">
    <fieldset class="login">
        <legend><a style="color: #102463;">Account Information</a></legend>
        <p>
            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUsername">Username</asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="textEntry"></asp:TextBox>
            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUsername"
                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="textEntry" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:CheckBox ID="RememberMe" runat="server" />
            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
        </p>
    </fieldset>
    <div>
        <div style="float: left">
            <p>
                <asp:HyperLink ID="hlnkforgot" runat="server" NavigateUrl="~/Account/ForgotPassword.aspx"
                    Text="Forgot Your Password?" /><br />
                Don't have an account?
                <asp:HyperLink ID="hlnkSIgnup" runat="server" NavigateUrl="~/Registration" Text="Join now"></asp:HyperLink>
                &nbsp;</p>
        </div>
        <div style="float: right">
            <asp:Button ID="btnLogin" runat="server" Text="Log in" OnClick="btnLogin_Click" CssClass="button button-login" />
        </div>
    </div>
    <span>&nbsp;</span>
</div>
