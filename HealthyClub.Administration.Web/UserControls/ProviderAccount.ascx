<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderAccount.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.ProviderAccount" %>

<style type="text/css">
    .style3 {
        width: 176px;
    }

    .style4 {
        width: 175px;
        color: red;
    }

    .style30 {
        vertical-align: bottom;
    }

    .style31 {
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
<div id="divNoProvider" runat="server" visible="false">
    <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto">
        <tr>
            <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Invalid information</td>
        </tr>
        <tr>
            <td style="padding: 10px;">

                <asp:Label ID="lblNoProvider" runat="server" Text="Invalid Provider ID, Click 'Back' to continue."></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">&nbsp;
                &nbsp;<asp:Button ID="Home" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                    BorderStyle="Solid" BorderWidth="0px" CausesValidation="False" CommandName="Home" Width="80px"
                    Text="Back" Font-Names="Verdana" ForeColor="White" Height="28px" ValidationGroup="CreateNewMember"
                    OnClientClick="history.back();" />
            </td>
        </tr>
    </table>
</div>
<div id="divProvider" runat="server">
    <strong><span class="style3">Provider Information </span></strong><span class="style4">(*indicates
        required field) </span>
    <br />
    <hr />
    <table>
        <tr>
            <td class="style31">Organisation Name
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style38">
                <asp:Label ID="lblCompany" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtCompany" Width="400px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCompany"
                    ErrorMessage="Organisation Name is required." ToolTip="Organisation Name is required."
                    ValidationGroup="CreateNewProvider"><label  class="required">* Required</label></asp:RequiredFieldValidator>
            </td>
        </tr>
       <tr>
                <td class="style5" class="bodyText1" style="vertical-align: top">Organisation Suburb
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>
                    <asp:TextBox ID="txtBranch" runat="server" Width="400px" Style="padding-left: 1px;"></asp:TextBox><label class="required">*</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBranch"
                        ErrorMessage="Company Name is required." ToolTip="Organisation Suburb is required."><label 
                                        class="required"> Required</label></asp:RequiredFieldValidator>

                    <div id="EditSuburb" runat="server" class="noBorder">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewSuburb" runat="server" AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false" BorderStyle="None" BorderWidth="0px">
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
                                        <asp:Button ID="ButtonAdd" runat="server" Text="Add another suburb" CssClass="button button-login" CausesValidation="false" OnClick="ButtonAdd_Click" /></span>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
    </table>
    <br />
    <strong><span class="style3">Provider Contact Details</span></strong><span class="style4">
        (*indicates required field)</span>
    <hr />
    <table width="100%">
        <tr>
            <td class="style39">Name
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style30">
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
                <asp:TextBox ID="txtFirstName" Width="100px" Text="First Name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFirstName"
                    ErrorMessage="FirstName is required." ToolTip="E-mail is required." ValidationGroup="CreateNewProvider"><label Class="required">*</label></asp:RequiredFieldValidator>
                &nbsp;
                <asp:TextBox ID="txtMiddleName" Width="100px" Text="Middle Name (Optional)" runat="server"
                    ></asp:TextBox>
                &nbsp;&nbsp;
                <asp:TextBox ID="txtLastName" Width="100px" Text="Last Name" runat="server" ></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLastName"
                    ErrorMessage="E-mail is required." ToolTip="LastName is required." ValidationGroup="CreateNewProvider"><label Class="required">*</label></asp:RequiredFieldValidator>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style39">Where the company is located
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style30">
                <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtAddress1" Text="Address Line 1" Width="400px" runat="server"
                    ></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress1"
                    ErrorMessage="Address is required." ToolTip="Address is required." ValidationGroup="CreateNewProvider"><label Class="required">* Required</label></asp:RequiredFieldValidator>
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
                <asp:TextBox ID="txtPostCode" Width="75px" Text="Postcode" runat="server" ></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="txtPostCode_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                    Enabled="True" MaskType="Number" TargetControlID="txtPostCode" Mask="9999">
                </ajaxToolkit:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td class="style39">Primary Contact Number
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style30">
                <asp:Label ID="lblContactNumber" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtContactNumber" Width="150px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContactNumber"
                    ErrorMessage="Phone Number is required." ToolTip="Phone Number is required."
                    ValidationGroup="CreateNewProvider"><label Class="required">* Required</label></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style39">Other Contact Number
            </td>
            <td class="style32">&nbsp;</td>
            <td class="style30">
                <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtMobile" Width="150px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style39">How do you prefer to be contacted
            </td>
            <td class="style34">&nbsp;</td>
            <td class="style30">
                <asp:Label ID="lblPrefered" runat="server" Text=""></asp:Label>
                <asp:RadioButton ID="radiobyEMail" runat="server" Checked="true" GroupName="Contact"
                    Text="Email" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="radiobyPhone" runat="server" GroupName="Contact" Text="Phone" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="radiobyMail" runat="server" GroupName="Contact" Text="Mail/Brochure" />
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
    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CssClass="buttonFinish" />
    <asp:Button ID="btnSave" runat="server" Text="Save" Width="75px" OnClick="btnSave_Click"
        CssClass="buttonFinish" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
        Style="margin-right: 15px" CssClass="buttonFinish" />
    <asp:HiddenField ID="hdnProviderID" runat="server" />
    <asp:HiddenField ID="hdnUsername" runat="server" />
    <asp:HiddenField ID="hdnAgreement" runat="server" />
    <asp:HiddenField ID="hdnAddEditMode" runat="server" />
    <asp:HiddenField ID="hdnEmailAddress" runat="server" />
    <asp:HiddenField ID="hdnCreatedBy" runat="server" />
    <asp:HiddenField ID="hdnCreatedDatetime" runat="server" />
    <asp:HiddenField ID="hdnModifiedBy" runat="server" />
    <asp:HiddenField ID="hdnModifiedDatetime" runat="server" />
</div>

