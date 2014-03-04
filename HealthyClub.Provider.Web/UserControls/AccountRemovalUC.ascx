<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountRemovalUC.ascx.cs" Inherits="HealthyClub.Provider.Web.UserControls.AccountRemovalUC" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div id="accountCancel" runat="server" style="margin: 0px; padding: 0px; border: 0px;">
            <span class="textTitle" style="margin-top: 10px;">Cancel Account</span>
            <br />
            <p>
                If a specific problem is causing you to remove your account from the Healthy Australia Club we may be able to help resolve your issue. Click
    <asp:HyperLink ID="hlnkContactUs" runat="server" NavigateUrl="~/ContactUs/">here</asp:HyperLink>
                &nbsp;to contact us. Your account will be closed within two business days of your request and all content will be permanently deleted.
            </p>
            Are you sure you want to delete your account?<br />
            <asp:CheckBox ID="chkRemoveAccount" runat="server" AutoPostBack="true" Text="Yes, I want to permanently delete my Activity Provider account." OnCheckedChanged="chkRemoveAccount_CheckedChanged" />
            <p>
                <label for="Passwd" style="margin: 7px 0px; display: block; font-weight: bold; color: rgb(51, 51, 51); font-family: arial, helvetica, sans-serif; font-size: 13px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: 13px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
                    Enter password</label>
                <asp:TextBox ID="txtPassword" runat="server" MaxLength="25" Style="font-family: arial, helvetica, sans-serif; font-size: 14px; -webkit-appearance: none; display: inline-block; height: 25px; margin: 0px; padding: 0px 8px; background-color: rgb(255, 255, 255); border-width: 1px; border-style: solid; border-color: rgb(192, 192, 192) rgb(217, 217, 217) rgb(217, 217, 217); box-sizing: border-box; border-top-left-radius: 1px; border-top-right-radius: 1px; border-bottom-right-radius: 1px; border-bottom-left-radius: 1px; font-style: normal; font-variant: normal; font-weight: normal; orphans: auto; white-space: normal; widows: auto; -webkit-text-stroke-width: 0px; background-position: initial initial; width: 200px; background-repeat: initial initial;" TextMode="Password" />
            </p>
            <p>
                <asp:Label ID="lblError" Visible="false" runat="server" Text="Error" Style="font-size: 14px; font-family: arial, helvetica, sans-serif; Color: rgb(221, 75, 57)"></asp:Label>
            </p>
            <table>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnDelete" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                            BorderStyle="Solid" BorderWidth="0px" CommandName="MoveNext" Font-Names="Verdana"
                            ForeColor="White" Height="28px" Width="120px" Text="Delete Account" OnClick="btnDelete_Click" />
                        &nbsp;
                    &nbsp;<asp:Button ID="btnCancel" runat="server" BackColor="#499ECA" BorderColor="#499ECA" Width="80px"
                        BorderStyle="Solid" BorderWidth="0px" CausesValidation="False"
                        Font-Names="Verdana" ForeColor="White" Height="28px" CommandName="Home"
                        Text="Cancel" ValidationGroup="CreateNewMember" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="CompleteConfirm" runat="server" visible="false">
            <table style="font-family: Verdana; font-size: 100%; width: 600px; margin: 0 auto">
                <tr>
                    <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Account has been cancelled
                    </td>
                </tr>
                <tr>
                    <td>Your account will be closed within 2 business days.&nbsp; </td>
                </tr>
                <tr>
                    <td align="right">&nbsp;</td>
                </tr>
            </table>
        </div>

    </ContentTemplate>

</asp:UpdatePanel>
