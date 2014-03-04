<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasswordRecovery.aspx.cs" Inherits="HealthyClub.Provider.Web.Account.PasswordRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http://code.jquery.com/jquery-1.7.min.js"></script>
    <script src="../Scripts/PasswordChecker.js"></script>
    <style>
        ul, li {
            margin: 0;
            padding: 0;
            list-style-type: none;
        }

        #pswd_info {
            display: none;
        }

        #pswd_info {
            position: relative;
            top: 15px;
            width: 250px;
            padding: 15px;
            background: #fefefe;
            font-size: .875em;
            border-radius: 5px;
            box-shadow: 0 1px 3px #ccc;
            border: 1px solid #ddd;
        }

            #pswd_info h4 {
                margin: 0 0 10px 0;
                padding: 0;
                font-weight: normal;
            }

            #pswd_info::before {
                content: "\25B2";
                position: absolute;
                top: -12px;
                left: 45%;
                font-size: 14px;
                line-height: 14px;
                color: #ddd;
                text-shadow: none;
                display: block;
            }

        .invalid {
            background: url(../Content/StyleImages/cancel.png) no-repeat 0 50%;
            padding-left: 22px;
            line-height: 24px;
            color: #ec3f41;
        }

        .valid {
            background: url(../Content/StyleImages/accept.png) no-repeat 0 50%;
            padding-left: 22px;
            line-height: 24px;
            color: #3a7d34;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Recovery" runat="server" visible="false">
        <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto">
            <tr>
                <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Complete
                </td>
            </tr>
            <tr>
                <td>Please enter your username on the field below to resend your confirmation email
                <br />
                    <div id="invalidToken" runat="server" style="padding: 20px;" visible="true">
                        <span>
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                        </span>
                    </div>
                    <div style="padding: 10px;">
                        <div style="width: 450px;">
                            <strong>New Password</strong><br />

                            <asp:TextBox ID="txtNewPassword" runat="server" Width="300px" Style="height: 25px; border: 1px solid #cccccc" TextMode="Password"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="field-validation-error" ControlToValidate="txtNewPassword" ErrorMessage="Password is Required"></asp:RequiredFieldValidator><br />
                            <br />
                            <strong>Verify password</strong><br />

                            <asp:TextBox ID="txtVerifyNewPassword" runat="server" Width="300px" Style="height: 25px; border: 1px solid #cccccc" TextMode="Password"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="field-validation-error" ControlToValidate="txtVerifyNewPassword" ErrorMessage="Please verify your password"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtVerifyNewPassword" ControlToValidate="txtNewPassword"
                                CssClass="field-validation-error" Display="Dynamic" ForeColor="Red" ErrorMessage="The password and confirmation password do not match." />&nbsp;&nbsp;<br />
                            <span>
                                <div id="pswd_info">
                                    <h4>Password must meet the following requirements:</h4>
                                    <ul>
                                        <li id="letter" class="invalid">At least <strong>one letter</strong></li>
                                        <li id="capital" class="invalid">At least <strong>one capital letter</strong></li>
                                        <li id="number" class="invalid">At least <strong>one number</strong></li>
                                        <li id="length" class="invalid">Be at least <strong>6 characters</strong></li>
                                    </ul>
                                </div>
                            </span>
                            <asp:HiddenField ID="hdnToken" runat="server" />
                            <br />
                            <br />
                            <div style="float: left; position: relative; left: 65px">
                                <asp:Button ID="btnChangePassword" runat="server" Text="Submit" CssClass="button button-login" Width="138px" OnClick="btnChangePassword_Click" />
                            </div>
                            <br />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="CompleteRecovery" runat="server" visible="false">
        <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto">
            <tr>
                <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Complete
                </td>
            </tr>
            <tr>
                <td>Your password has been changed.<br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="ContinueButton" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                        BorderStyle="Solid" BorderWidth="0px" CommandName="MoveNext" Font-Names="Verdana"
                        ForeColor="White" Height="28px" Width="80px" Text="Continue" ValidationGroup="CreateNewProvider"
                        OnClick="ContinueButton_Click" />
                    &nbsp;
                    &nbsp;<asp:Button ID="Home" runat="server" BackColor="#499ECA" BorderColor="#499ECA" Width="80px"
                        BorderStyle="Solid" BorderWidth="0px" CausesValidation="False"
                        Font-Names="Verdana" ForeColor="White" Height="28px" CommandName="Home"
                        Text="Home" ValidationGroup="CreateNewMember"
                        OnClick="HomeButton_Click" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
