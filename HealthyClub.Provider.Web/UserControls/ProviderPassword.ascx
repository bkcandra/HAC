<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderPassword.ascx.cs" Inherits="HealthyClub.Provider.Web.UserControls.ProviderPassword" %>
<asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/"
    EnableViewState="false" RenderOuterTable="false">
    <ChangePasswordTemplate>
        <span class="textTitle" style="margin-top: 10px;">Change Password</span>
        <br />
        <p>
            Complete the form below to change your password.
        </p>
        <span class="failureNotification">
            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
        </span>
        <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification"
            ValidationGroup="ChangeUserPasswordValidationGroup" />
        <div class="accountInfo">
            <fieldset class="changePassword">
                <legend>Account Information</legend>
                <p>
                    <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Current Password</asp:Label>
                    <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required."
                        ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password</asp:Label>
                    <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                        CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required."
                        ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password</asp:Label>
                    <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                        CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                        ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                        ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                        ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                </p>
            </fieldset>
            <p class="submitButton">
                <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" />
                <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                    Text="Change Password" ValidationGroup="ChangeUserPasswordValidationGroup" OnClick="ChangePasswordPushButton_Click" />
            </p>
        </div>
    </ChangePasswordTemplate>
</asp:ChangePassword>
<div id="CompleteConfirm" runat="server" visible="false">
    <table style="font-family: Verdana; font-size: 100%; width: 600px; margin: 0 auto">
        <tr>
            <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Complete
            </td>
        </tr>
        <tr>
            <td>Your password has been changed.
                <br />
                <br />
            </td>
        </tr>
        
    </table>
</div>
