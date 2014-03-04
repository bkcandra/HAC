<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderAcountSettingUC.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ProviderAcoountSettingUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
    .style3 {
        width: 176px;
        color: #1B274F;
        font-size: 14px;
    }

    .style4 {
        width: 175px;
        color: red;
    }
    .style5{
        vertical-align:top;
    }

    .style30 {
        vertical-align: bottom;
    }

    .style31 {
        vertical-align: top;
        width: 170px;
    }

    .style32 {
        width: 5px;
    }

    .style34 {
        width: 5px;
    }

    .style38 {
    }

    .style39 {
        width: 225px;
    }

    .required {
        color: red;
    }
</style>
<span class="textTitle" style="margin-top: 10px;">Account Information</span>
<br />
<div>
    <span class="style3"><strong>Activity Provider Information </strong>(<span class="style4">* </span>indicates
        required field)</span>
    <br />
    <hr />
    <table>
        <tr>
            <td class="style31">Organisation name
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style38">
                <asp:Label ID="lblCompany" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtCompany" Width="400px" runat="server"></asp:TextBox><span style="color: red">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCompany"
                    ErrorMessage="Organisation Name is required." ToolTip="Organisation Name is required."
                    ValidationGroup="CreateNewProvider"><label  class="required"> Required</label></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style31">Organisation location
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style38">
                <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtBranch" runat="server" Width="400px"></asp:TextBox>
                <div id="divSecSuburb" runat="server" class="noBorder">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridViewSuburb" runat="server" AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false" BorderStyle="None" BorderWidth="0">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSuburb" runat="server" Width="400px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div style="width: 410px">
                                <span style="float: left">
                                    <asp:Label ID="lblErrorSuburb" runat="server" ForeColor="Red"></asp:Label></span>
                                <span style="float: right">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Content/StyleImages/add_limit.png" CausesValidation="false" OnClick="ImageButton1_Click"></asp:ImageButton><span>Add another suburb</span>
                                </span>

                            </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <strong><span class="style3">Activity Provider Contact Details</span></strong>
    <hr />
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <table width="100%">
        <tr>
            <td class="style39">Name
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style30">
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlTitle" runat="server" TabIndex="0" AutoPostBack="true" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                    <asp:ListItem Value="0">No Title</asp:ListItem>
                    <asp:ListItem Value="1">Mr</asp:ListItem>
                    <asp:ListItem Value="2">Mrs</asp:ListItem>
                    <asp:ListItem Value="3">Ms</asp:ListItem>
                    <asp:ListItem Value="4">Miss</asp:ListItem>
                    <asp:ListItem Value="5">Dr</asp:ListItem>
                    <asp:ListItem Value="6">Rev</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtFirstName" Width="300px" runat="server"></asp:TextBox>

                <asp:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" 
                    runat="server" TargetControlID="txtFirstName" WatermarkText="Name">
                </asp:TextBoxWatermarkExtender>

                &nbsp;&nbsp;
                <asp:TextBox ID="txtLastName" Width="150px" Visible="false" runat="server"></asp:TextBox><span style="color: red">*</span>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLastName"
                    ErrorMessage="E-mail is required." ToolTip="LastName is required." ValidationGroup="CreateNewProvider"><label Class="required"></label></asp:RequiredFieldValidator>
                &nbsp;
                 <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" 
                    runat="server" TargetControlID="txtLastName" WatermarkText="Last Name">
                </asp:TextBoxWatermarkExtender>
            </td>
        </tr>
                
        <tr>
            <td class="style5">
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="Email" CssClass="bodyText1">E-mail Address</asp:Label>
            </td>
            <td class="style1">&nbsp;
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblEmailAddress" runat="server" Text=""></asp:Label>
                        <asp:TextBox ID="Email" Width="200px" runat="server" MaxLength="50" AutoPostBack="true" OnTextChanged="Email_TextChanged"></asp:TextBox>
                        <span
                            class="required">*</span><asp:RequiredFieldValidator ID="ReqEmailAddress"
                                runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required." ForeColor="Red"></asp:RequiredFieldValidator>
                        &nbsp;
                     <asp:Label ID="lblEmailTaken" runat="server" Text="Email address has already been registered." Visible="false" ForeColor="Red"></asp:Label>
                        &nbsp;<asp:Label ID="lblInvalidEmail" runat="server" Text="Invalid email address." Visible="false" ForeColor="Red"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Email" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="trConfirmEmail" runat="server">
            <td class="style5">Retype E-mail Address
            </td>
            <td class="style1">&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtEmailVerify" Width="200px" runat="server" MaxLength="50"></asp:TextBox><span
                    class="required">*</span>
                <asp:CompareValidator ID="EmailCompare" runat="server" ControlToCompare="Email" ControlToValidate="txtEmailVerify"
                    ErrorMessage="Email address did not matched." Style="color: #FF0000"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="style5">&nbsp;</td>
            <td class="style1">&nbsp;
            </td>
            <td >

                <asp:CheckBox ID="chkBoxChangeActivityEmail" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style39">Organisation address</td>
            <td class="style32">&nbsp;</td>
            <td class="style30">
                <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtAddress1" Text="Address Line 1" Width="400px" runat="server"></asp:TextBox><span style="color: red">*</span>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress1"
                    ErrorMessage="Address is required." ToolTip="Address is required." ValidationGroup="CreateNewProvider"><label Class="required"> Required</label></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style39">&nbsp;
            </td>
            <td class="style32">&nbsp;
            </td>
            <td class="style30">
                <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtSuburb" Width="150px" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
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
            <td class="style39">Primary contact number
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style30">
                <asp:Label ID="lblContactNumber" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtContactNumber" Width="150px" runat="server"></asp:TextBox><span style="color: red">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContactNumber"
                    ErrorMessage="Phone Number is required." ToolTip="Phone Number is required."
                    ValidationGroup="CreateNewProvider"><label Class="required"> Required</label></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style39">Other contact number
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style30">
                <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtMobile" Width="150px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style39">How do you prefer to be contacted?</td>
            <td class="style34">&nbsp;</td>
            <td class="style30">
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
            <td align="center" colspan="3">
                <br />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="color: Red;">
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
        </ContentTemplate>
    </asp:UpdatePanel>
    
</div>
<div style="float: right">
    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CssClass="buttonFinish" />
    <asp:Button ID="btnSave" runat="server" Text="Save" Width="75px" OnClick="btnSave_Click"
        CssClass="buttonFinish" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
        Style="margin-right: 15px" CssClass="buttonFinish" />
</div>
<div style="clear: both"></div>
<asp:HiddenField ID="hdnProviderID" runat="server" />
<asp:HiddenField ID="hdnUsername" runat="server" />
<asp:HiddenField ID="hdnAgreement" runat="server" />
<asp:HiddenField ID="hdnAddEditMode" runat="server" />
<asp:HiddenField ID="hdnAccountDeletion" runat="server" />
<asp:HiddenField ID="hdnEmailAddress" runat="server" />
<asp:HiddenField ID="hdnCreatedBy" runat="server" />
<asp:HiddenField ID="hdnCreatedDatetime" runat="server" />
<asp:HiddenField ID="hdnModifiedBy" runat="server" />
<asp:HiddenField ID="hdnModifiedDatetime" runat="server" />
