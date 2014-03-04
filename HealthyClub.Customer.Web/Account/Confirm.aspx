<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="HealthyClub.Customer.Web.Account.ConfirmPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <div id="Confirm" runat="server">
        <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto">
<tr>
                <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">&nbsp;
                Confirm your account</td>
            </tr>            <tr>
                <td>Please enter your email or username below to resend your confirmation email.<br />
                    <div style="padding: 10px;">
                        <br />
                        <asp:TextBox ID="txtUsername" runat="server" Width="200px" Style="height: 25px; border: 1px solid #cccccc"></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="btnSendConfirmation" runat="server" Text="Send Email" CssClass="button button-login" OnClick="btnSendConfirmation_Click" />
                    &nbsp;</div>
                    <div id="divEnterCode" runat="server" visible="false" style="padding: 10px;">
                        <asp:TextBox ID="txtConfirmationCode" runat="server" Width="200px" Style="height: 25px; border: 1px solid #cccccc"></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="btnConfirmCode" runat="server" Text="Confirm Account" CssClass="button button-login" OnClick="btnConfirmCode_Click"/>
                    </div>
                    <span>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    </span>
                </td>
            </tr>



        </table>
    </div>
    <div id="CompleteConfirm" runat="server" visible="false">
        <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto">
            <tr>
                <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Complete
                </td>
            </tr>
            <tr>
                <td>Your account is confirmed.&nbsp;
                <br />
                    <br />
                    Click continue to log in and start searching for healthy activities. </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="ContinueButton" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                    BorderStyle="Solid" BorderWidth="0px" CausesValidation="False" CommandName="Continue" Width="80px"
                    Font-Names="Verdana" ForeColor="White" Height="28px" Text="Continue" ValidationGroup="CreateNewMember"
                    OnClick="ContinueButton_Click" />
                &nbsp;<asp:Button ID="Home" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                    BorderStyle="Solid" BorderWidth="0px" CausesValidation="False" CommandName="Home" Width="80px"
                    Text="Home" Font-Names="Verdana" ForeColor="White" Height="28px" ValidationGroup="CreateNewMember"
                    OnClick="HomeButton_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
