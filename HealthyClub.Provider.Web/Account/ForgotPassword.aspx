<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="HealthyClub.Provider.Web.Account.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="PasswordRecovery" runat="server">
        <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto">
            <tr>
                <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Forgot your password</td>
            </tr>
            <tr>
                <td>Please enter your email or username. You will then receive an email with a link allowing you to choose a new password.
                <br />
                    <div style="padding: 10px;">
                        <br />
                        <asp:TextBox ID="txtUsername" runat="server" Width="200px" Style="height: 25px; border: 1px solid #cccccc"></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="btnGetPasswordToken" runat="server" Text="Submit" CssClass="button button-login" OnClick="btnGetPasswordToken_Click" />
                        <br />
                        <span>
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                        </span>
                    </div>


                </td>
            </tr>



        </table>
    </div>
    <div id="PasswordRecoveryComplete" runat="server" visible="false">
        <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto">
            <tr>
                <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Email sent</td>
            </tr>
            <tr>
                <td style="padding: 10px;"><span>An email has been sent to '<asp:Label ID="lblEmail" runat="server" Text="" Font-Bold="true"></asp:Label>&#39; with instructions on how to change your password.
                </span></td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;<asp:Button ID="Home" runat="server" BackColor="#499ECA" BorderColor="#499ECA" Width="80px"
                        BorderStyle="Solid" BorderWidth="0px" CausesValidation="False" Height="28px" CommandName="Home"
                        Text="Home"  Font-Names="Verdana" ForeColor="White"  ValidationGroup="CreateNewMember"
                        OnClick="HomeButton_Click" />

                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
