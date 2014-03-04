<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAccountUC.ascx.cs"
    Inherits="HealthyClub.Web.UserControls.CustomerAccountUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .style3 {
        width: 176px;
        color: #1B274F;
        font-size: 16px;
    }

    .style4 {
        font-size: 14px;
        color: red;
    }

    .style31 {
        width: 170px;
        color: #1B274F;
        font-size: 14px;
        font-family: Arial;
    }

    .style32 {
        width: 5px;
    }

    .style5 {
        width: 225px;
        vertical-align: top;
        color: #1B274F;
        font-size: 14px;
        font-family: Arial;
    }

    .style1 {
        width: 5px;
        vertical-align: top;
    }

    .style39 {
        color: red;
    }

    .style40 {
        width: 175px;
        font-size: small;
    }

    .style42 {
        width: 620px;
        vertical-align: top;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <span class="textTitle" style="margin-top: 10px;">Account Information</span>
        <br />
        <div style="color: Black">
            <span class="style3"><strong>Club Member information </strong>(<span class="style4">* </span>indicates
        required field)</span>
            <br />
            <table>
                <tr>
                    <td class="style31">Username
                    </td>
                    <td class="style32">&nbsp;</td>
                    <td class="style38">
                        <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style31">Email address
                    </td>
                    <td class="style32">&nbsp;</td>
                    <td class="style38">
                        <asp:Label ID="lblEmailAdress" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txtEmailAdress" runat="server" Width="350px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmailAdress"
                            Style="color: #FF0000" Text="Invalid email format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </td>
                </tr>
                <tr>
                    <td class="style31">&nbsp;
                    </td>
                    <td class="style32">&nbsp;
                    </td>
                    <td class="style38">
                        <asp:TextBox ID="txtEmailAdress2" runat="server" Width="350px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <strong><span class="style3">Club Member contact details</span></strong>
            <table style="width: 100%">
                <tr>
                    <td class="style5">Name
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td class="style42">
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="ddlTitle" runat="server" TabIndex="0">
                            <asp:ListItem Value="0">Title</asp:ListItem>
                            <asp:ListItem Value="1">Mr</asp:ListItem>
                            <asp:ListItem Value="2">Mrs</asp:ListItem>
                            <asp:ListItem Value="3">Ms</asp:ListItem>
                            <asp:ListItem Value="4">Miss</asp:ListItem>
                            <asp:ListItem Value="5">Dr</asp:ListItem>
                            <asp:ListItem Value="6">Rev</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtFirstName" Width="150px" Text="First Name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFirstName"
                            ErrorMessage="FirstName is required." ToolTip="E-mail is required." ValidationGroup="CreateNewMember"
                            ForeColor="Red">*</asp:RequiredFieldValidator>
                        &nbsp;
                <asp:TextBox ID="txtLastName" Width="150px" Text="Last Name" runat="server"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLastName"
                            ErrorMessage="E-mail is required." ToolTip="LastName is required." ValidationGroup="CreateNewMember"
                            ForeColor="Red">*</asp:RequiredFieldValidator>
                        &nbsp;
                    </td>
                </tr>
                <tr id="trGender" runat="server">
                    <td class="style5">Gender
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td class="style42">
                        <asp:Label ID="lblSex" runat="server" Text=""></asp:Label>
                        <asp:RadioButton ID="radMale" Text="Male" runat="server" Checked="True" GroupName="CustomerGender" />
                        &nbsp;&nbsp;
                <asp:RadioButton ID="radFemale" Text="Female" runat="server" GroupName="CustomerGender" />
                    </td>
                </tr>
                <tr id="trDOB" runat="server">
                    <td class="style5">Date of birth</td>
                    <td class="style1">&nbsp;</td>
                    <td class="style42">
                        <asp:Label ID="lblBirthdate" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txtDOB" runat="server" Width="130px" MaxLength="1" Style="text-align: justify"
                            ValidationGroup="MKE" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-AU" runat="server"
                            TargetControlID="txtDOB" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="None" AcceptNegative="None"
                            ErrorTooltipEnabled="True" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDOB">
                        </ajaxToolkit:CalendarExtender>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDOB"
                            ErrorMessage="Date of Birth is required." ToolTip="Date of Birth is required."
                            ValidationGroup="CreateNewMember" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="style5">Address
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td class="style42">
                        <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txtAddress1" Text="Address Line 1" Width="450px" runat="server"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress1"
                            ErrorMessage="Address is required." ToolTip="Address is required." ValidationGroup="CreateNewMember"
                            ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style5">&nbsp;
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style42">
                        <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txtSuburb" runat="server"></asp:TextBox>
                        &nbsp;
                <asp:DropDownList ID="ddlState" runat="server">
                </asp:DropDownList>
                        &nbsp;&nbsp;
                <asp:TextBox ID="txtPostCode" Width="75px" Text="Postcode" runat="server"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtPostCode_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" MaskType="Number" TargetControlID="txtPostCode" Mask="9999">
                        </asp:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style5">Contact number
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td class="style42">
                        <asp:Label ID="lblPhoneNumber" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txtContactNumber" Width="150px" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContactNumber"
                            ErrorMessage="Phone Number is required." ToolTip="Phone Number is required."
                            ValidationGroup="CreateNewMember" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style5">Other contact number
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td class="style42">
                        <asp:Label ID="lblMobileNumber" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="txtMobileNumber" Width="150px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style5">How do you prefer to be contacted? </td>
                    <td class="style1">&nbsp;</td>
                    <td class="style42">
                        <asp:Label ID="lblPrefered" runat="server" Text=""></asp:Label>
                        <asp:RadioButton ID="radiobyEMail" runat="server" Checked="true" GroupName="Contact"
                            Text="Email" />
                        &nbsp;&nbsp;
                <asp:RadioButton ID="radiobyPhone" runat="server" GroupName="Contact" Text="Phone" />
                        &nbsp;&nbsp;
                <asp:RadioButton ID="radiobyMail" runat="server" GroupName="Contact" Text="Mail" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3" style="color: Red;">
                        <asp:CompareValidator ID="EmailCompare" runat="server" ControlToCompare="txtEmailAdress"
                            ControlToValidate="txtEmailAdress2" ErrorMessage="The email and confirmation email must match."
                            Style="color: #FF0000"></asp:CompareValidator>
                        <br />
                        <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3" style="color: Red;">
                        <asp:Label ID="lblError" Visible="false" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3" style="color: Red;">
                        <asp:Label ID="lblError0" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>


        </div>
        <div style="float: right">

            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CssClass="buttonFinish" />
            <asp:Button ID="btnSave" runat="server" Text="Save" Width="75px" OnClick="btnSave_Click"
                CssClass="buttonFinish" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                Style="margin-right: 15px" CssClass="buttonFinish" />
        </div>
        <div style="clear: both"></div>
        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:HiddenField ID="hdnUsername" runat="server" />
        <asp:HiddenField ID="hdnAgreement" runat="server" />
        <asp:HiddenField ID="hdnAddEditMode" runat="server" />
        <asp:HiddenField ID="hdnEmailAddress" runat="server" />
        <asp:HiddenField ID="hdnCreatedBy" runat="server" />
        <asp:HiddenField ID="hdnCreatedDatetime" runat="server" />
        <asp:HiddenField ID="hdnAccountDeletion" runat="server" />
        <asp:HiddenField ID="hdnModifiedBy" runat="server" />
        <asp:HiddenField ID="hdnModifiedDatetime" runat="server" />

    </ContentTemplate>
</asp:UpdatePanel>
