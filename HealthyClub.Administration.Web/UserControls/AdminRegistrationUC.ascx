<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminRegistrationUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.AdminRegistrationUC" %>
<style type="text/css">
    .barIndicatorBorder {
        border: solid 1px #c0c0c0;
        width: 200px;
    }

    .barIndicator_poor {
        background-color: gray;
    }

    .barIndicator_weak {
        background-color: cyan;
    }

    .barIndicator_good {
        background-color: lightblue;
    }

    .barIndicator_strong {
        background-color: blue;
    }

    .barIndicator_excellent {
        background-color: navy;
    }
</style>
<div>
    <h2>Create a New 
        Administrator Account
    </h2>
    <p>
        Use the form below to create a new account.
    </p>
    <fieldset class="Register" style="padding: 25px;">
        <legend>Account Information</legend>
        <br />
        <div style="width: 150px">Username</div>
        <div style="width: 700px">
            <asp:TextBox ID="txtUsername" runat="server" Width="200px" MaxLength="20" CausesValidation="true"></asp:TextBox>
            &nbsp;
           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUsername" InitialValue="" ForeColor="Red" runat="server" ErrorMessage="Username is required."></asp:RequiredFieldValidator>
        </div>

        <br />
        <div style="width: 150px">E-Mail</div>
        <div style="width: 700px">
            <asp:TextBox ID="txtEmail" runat="server" Width="200px" MaxLength="20" CausesValidation="true"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="Email is required."></asp:RequiredFieldValidator>&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail" runat="server"
                ErrorMessage="Value is not valid Email Address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
        </div>
        <br />
        <div style="width: 150px">Password</div>
        <div style="width: 700px">
            <asp:TextBox ID="txtPassword" runat="server" CausesValidation="true" Width="200px" MaxLength="20" TextMode="Password"></asp:TextBox>
            &nbsp;
            <ajaxToolkit:PasswordStrength runat="server" ID="PasswordStrength1"
                TargetControlID="txtPassword"
                DisplayPosition="RightSide"
                MinimumSymbolCharacters="1"
                MinimumUpperCaseCharacters="1"
                PreferredPasswordLength="8"
                CalculationWeightings="25;25;15;35"
                RequiresUpperAndLowerCaseCharacters="true"
                TextStrengthDescriptions="Poor; Weak; Good; Strong; Excellent"
                HelpStatusLabelID="Label1"
                StrengthIndicatorType="BarIndicator"
                HelpHandlePosition="AboveLeft"
                BarBorderCssClass="barIndicatorBorder"
                StrengthStyles="barIndicator_poor; barIndicator_weak; barIndicator_good; barIndicator_strong; barIndicator_excellent">
            </ajaxToolkit:PasswordStrength>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword"
                ErrorMessage="Password is required" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <br />
        <div style="width: 150px">Confirm Password</div>
        <div style="width: 700px">
            <asp:TextBox ID="txtPassword2" CausesValidation="true" runat="server" Width="200px" MaxLength="20" TextMode="Password"></asp:TextBox>
            &nbsp;
            &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword2" ControlToValidate="txtPassword"
                CssClass="field-validation-error" Display="Dynamic" ForeColor="Red" ErrorMessage="The password and confirmation password do not match." />
        </div>

        <br />
        <div style="width: 700px; padding-left: 50px;">
            &nbsp;<asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
        </div>
        &nbsp;<br />
        <div style="width: 700px;">
            <asp:Button ID="btnSubmit" runat="server" Text="Create User"
                OnClick="btnSubmit_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        OnClick="btnCancel_Click" />
        </div>

    </fieldset>
</div>
