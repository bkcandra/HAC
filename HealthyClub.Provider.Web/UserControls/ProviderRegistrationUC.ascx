<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderRegistrationUC.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ProviderRegistrationUC" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<%@ Register Src="~/UserControls/ProviderImageRegistration.ascx" TagPrefix="uc1" TagName="ProviderImageRegistration" %>
<script src="http://code.jquery.com/jquery-1.7.min.js"></script>
<script src="../Scripts/PasswordChecker.js"></script>
<script>

    function CheckNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

</script>
<style type="text/css">
    .style1 {
        width: 5px;
    }

    .style3 {
        font-size: large;
        color: #102463;
    }

    .style4 {
        font-size: small;
    }

    .style5 {
        width: 157px;
    }

    .style7 {
        width: 157px;
        height: 26px;
    }

    .style8 {
        width: 5px;
        height: 26px;
    }

    .style9 {
        height: 26px;
    }

    .required {
        color: red;
    }

    div.noBorder td {
        border: 0;
    }

    .required {
        color: red;
    }

    ul, li {
        margin: 0;
        padding: 0;
        list-style-type: none;
        font-family: Arial;
    }

    #pswd_info {
        display: none;
    }

    #pswd_info {
        position: relative;
        left: 150px;
        top: 5px;
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
            color: #1B274F;
        }

        #pswd_info::before {
            content: "\25B2";
            position: absolute;
            top: -12px;
            left: 35%;
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
<div id="divError" runat="server" class="errorBox" visible="false" style="width: 600px">
    <span id="Title">Oops! You haven't finished entering your organisation details. </span>
    <span>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" Style="color: #FF0000"></asp:Label>
    </span>
</div>
<div id="RegistrationStep" runat="server">
    <fieldset>
        <legend style="color: #102463"><span class="style3"><strong>Activity Provider Registration
        </strong></span>
            <br />
            <span style="font-size: 14px; font-weight: normal">Please enter the following details to 
                            register
                as an Activity Provider.</span>


        </legend><strong><span class="style3">Organisation details</span></strong><span
            class="style4">&nbsp; (<label class="required">*
            </label>
            indicates required field) </span>
        <br />
        <hr />
        <table>
            <tr>
                <td class="style5 bodyText1">Organisation Name
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtCompany" Width="400px" runat="server" MaxLength="50" OnTextChanged="txtCompany_TextChanged" CssClass="textbox"></asp:TextBox><label class="required">*</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCompany"
                        ErrorMessage="Organisation Name is required." ToolTip="Company Name is required."><label 
                                        class="required"> Required</label></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="lblOrganisationName" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style5 bodyText1" style="vertical-align: top">Organisation Suburb
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtBranch" runat="server" Width="400px" MaxLength="50" Style="padding-left: 1px;" CssClass="textbox"></asp:TextBox>
                    <label class="required">*</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBranch"
                        ErrorMessage="Company Name is required." ToolTip="Organisation Suburb is required."><label 
                                        class="required"> Required</label></asp:RequiredFieldValidator>
                    <div class="noBorder">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridViewSuburb" runat="server" AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false" BorderStyle="None" BorderWidth="0px">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSuburb" MaxLength="50" runat="server" Width="400px" CssClass="textbox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="width: 410px">
                                    <span style="float: left">
                                        <asp:Label ID="lblErrorSuburb" runat="server" ForeColor="Red"></asp:Label></span>
                                    <span style="float: right">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Content/StyleImages/add_limit.png"
                                            CausesValidation="false" OnClick="ImageButton1_Click" Style="padding-top: 5px"></asp:ImageButton>
                                        <span>
                                            <asp:LinkButton ID="lblAddSub" runat="server" OnClick="ImageButton1_Click" CausesValidation="false">Add another suburb</asp:LinkButton></span>

                                    </span>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <div>
            <strong><span class="style3 bodyText1">Create your login information </span>
            </strong>
            <br />
            <hr />
        </div>
        <table>
            <tr>
                <td class="style5">
                    <asp:Label ID="lblUsername" AssociatedControlID="UserName" runat="server">Username</asp:Label>
                </td>
                <td class="style1">&nbsp;
                </td>
                <td class="style6">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="Username" Width="200px" runat="server" AutoPostBack="true" OnTextChanged="Username_TextChanged" MaxLength="50" CssClass="textbox"></asp:TextBox><label class="required">*</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Username"
                                CssClass="bodyText1" ToolTip="User Name is required."><label Class="required"> Required</label></asp:RequiredFieldValidator>
                            &nbsp;
                    
                    <asp:Label ID="lblUsernameError" runat="server" Text="This username has been taken." Visible="false" ForeColor="Red"></asp:Label>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Label ID="lblPassword1" runat="server" AssociatedControlID="Password">Password</asp:Label>
                </td>
                <td class="style1">&nbsp;
                </td>
                <td class="style6">
                    <asp:TextBox ID="Password" Width="200px" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox><span
                        class="required">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password"
                        ErrorMessage="Password is required." ToolTip="Password is required."><label Class="required"> Required</label></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style5 bodyText1">Retype Password
                </td>
                <td class="style1">&nbsp;
                </td>
                <td class="">
                    <asp:TextBox ID="txtPasswordVerify" TextMode="Password" Width="200px" runat="server" CssClass="textbox"></asp:TextBox><label
                        class="required">*</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPasswordVerify"
                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."><label Class="required"> Required</label></asp:RequiredFieldValidator>
                    &nbsp;
                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                        ControlToValidate="txtPasswordVerify" Display="Dynamic" ErrorMessage="The password and confirmation password must match."
                        Style="color: #FF0000"></asp:CompareValidator>
                </td>
            </tr>
        </table>
        <div id="pswd_info">
            <h4>Password must meet the following requirements:</h4>
            <ul>
                <li id="letter" class="invalid">At least <strong>one letter</strong></li>
                <li id="capital" class="invalid">At least <strong>one capital letter</strong></li>
                <li id="number" class="invalid">At least <strong>one number</strong></li>
                <li id="length" class="invalid">Be at least <strong>six characters</strong></li>
            </ul>
        </div>
        <asp:HiddenField ID="hdnpswd_info" runat="server" />
        <br />
        <strong><span class="style3">Please list your organisations contact details</span></strong>
        <hr />


        <table style="width: 100%">
            <tr>
                <td class="style5">Name
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTitle" runat="server" TabIndex="0" AutoPostBack="true" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                <asp:ListItem Value="0">No Title</asp:ListItem>
                                <asp:ListItem Value="1">Mr</asp:ListItem>
                                <asp:ListItem Value="2">Mrs</asp:ListItem>
                                <asp:ListItem Value="3">Ms</asp:ListItem>
                                <asp:ListItem Value="4">Miss</asp:ListItem>
                                <asp:ListItem Value="5">Dr</asp:ListItem>
                                <asp:ListItem Value="6">Rev</asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtFirstName" Width="300px" runat="server" MaxLength="50" CssClass="textbox"></asp:TextBox><span class="required">*</span>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" WatermarkText="Name"
                                runat="server" TargetControlID="txtFirstName" WatermarkCssClass="textbox bodyText2">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFirstName"
                                ErrorMessage="FirstName is required." ToolTip="E-mail is required."><label class="required"></label></asp:RequiredFieldValidator>
                            &nbsp;
                                    
                                    <asp:TextBox ID="txtLastName" Width="200px" Visible="false" runat="server" MaxLength="50" CssClass="textbox"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" WatermarkText="Surname"
                                WatermarkCssClass="textbox bodyText2" runat="server" TargetControlID="txtLastName">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTitle" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="Email" CssClass="bodyText1">E-mail Address</asp:Label>
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="Email" Width="200px" runat="server" MaxLength="50" AutoPostBack="true" OnTextChanged="Email_TextChanged" CssClass="textbox"></asp:TextBox>
                            <span class="required">*</span>&nbsp;<asp:RequiredFieldValidator ID="ReqEmailAddress"
                                runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required."><label class="required">Required</label>
                            </asp:RequiredFieldValidator>
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
            <tr>
                <td class="style5">Retype E-mail Address
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtEmailVerify" Width="200px" runat="server" MaxLength="50" CssClass="textbox"></asp:TextBox><span
                        class="required">*</span>
                    <asp:CompareValidator ID="EmailCompare" runat="server" ControlToCompare="Email" ControlToValidate="txtEmailVerify"
                        ErrorMessage="The email and confirmation email must match." Style="color: #FF0000"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style5">Postal Address
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtAddress1" Width="450px" runat="server" CssClass="textbox"></asp:TextBox><label class="required">*</label>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtAddress1_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtAddress1" WatermarkText="Street number and name" WatermarkCssClass="textbox bodyText2">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress1"
                        ErrorMessage="Address is required." ToolTip="Address is required."><label Class="required"> Required</label></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style5">&nbsp;
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtSuburb" runat="server" Width="150px" MaxLength="50" CssClass="textbox"></asp:TextBox><label class="required">*</label>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtSuburb_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtSuburb" WatermarkCssClass="textbox bodyText2" WatermarkText="Suburb">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlState" runat="server">
                                    </asp:DropDownList>
                    <span class="required">*</span> &nbsp;&nbsp;
                                    <asp:TextBox ID="txtPostCode" Width="80px" runat="server" MaxLength="4" onkeypress="return CheckNumber(event)" CssClass="textbox"></asp:TextBox><span class="required">*</span>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtPostCode_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtPostCode" WatermarkText="Postcode" WatermarkCssClass="textbox bodyText2">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td class="style7">Primary Contact Number
                </td>
                <td class="style8">&nbsp;
                </td>
                <td class="style9">
                    <asp:TextBox ID="txtContactNumber" Width="150px" runat="server" MaxLength="20" CssClass="textbox"></asp:TextBox><label
                        class="required">*</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContactNumber"
                        ErrorMessage="Phone Number is required." ToolTip="Phone Number is required."><label Class="required"> Required</label></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style5">Other Contact Number
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtMobileNumber" Width="150px" runat="server" MaxLength="20" CssClass="textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style5">How do you prefer to be contacted?
                </td>
                <td class="style1">&nbsp;
                </td>
                <td>
                    <asp:RadioButton ID="radiobyEMail" runat="server" Checked="true" GroupName="Contact"
                        Text="Email" />
                    &nbsp;&nbsp;
                                    <asp:RadioButton ID="radiobyPhone" runat="server" GroupName="Contact" Text="Phone" />
                    &nbsp;&nbsp;
                                    <asp:RadioButton ID="radiobyMail" runat="server" GroupName="Contact" Text="Mail" />
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">

                    <strong><span class="style3">Please upload your organisation logo</span></strong><br />
                    To upload your organisation logo, click &quot;browse.&quot;&nbsp; Once you have selected your image, click &quot;Upload.&quot;
                    <br />
                    <hr />
                    <uc1:ProviderImageRegistration runat="server" ID="ProviderImageUpload" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>

                <td colspan="3">
                    <span class="bodyText1"><strong>Review and accept the following agreements</strong></span>
                    <div style="padding-left: 20px">
                        <ul class="style10" style="outline: 0;">
                            <li class="style10" style="outline: 0;">
                                <asp:HyperLink ID="hlntoc" runat="server" Target="_blank" NavigateUrl="~/Pages/20">Privacy Statement</asp:HyperLink>
                            <li>
                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/Pages/21">Terms and Conditions</asp:HyperLink></li>
                        </ul>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">To accept the terms of service click &quot;I accept.&quot;&nbsp; By clicking I accept
                                    you are agreeing to the above Terms and Conditions and Privacy Statement.&nbsp;
                                    If you do not agree with these terms, click &quot;Cancel.&quot;
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trCaptcha" runat="server">
                <td colspan="3">Please enter the code below.
                    <recaptcha:RecaptchaControl
                        ID="RecaptchaControl1" PublicKey=" " PrivateKey=" "
                        runat="server"
                        Theme="clean" />
                </td>

            </tr>

            <tr>
                <td colspan="3" style="text-align: right">
                    <asp:Button ID="StepNextButton" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                        BorderStyle="Solid" BorderWidth="0px" CommandName="MoveNext" Font-Names="Verdana"
                        ForeColor="White" Height="28px" Text="I accept"
                        Width="80px" OnClick="StepNextButton_Click" />
                    &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="CancelButton" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                                    BorderStyle="Solid" BorderWidth="0px" CausesValidation="False" CommandName="Cancel"
                                    Font-Names="Verdana" ForeColor="White" Height="28px" Text="Cancel" Width="80px" OnClientClick="history.back();" />
                </td>
            </tr>
        </table>
    </fieldset>
</div>
<div id="CompleteStep" runat="server" visible="false">
    <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto">
        <tr>
            <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Confirm your account
            </td>
        </tr>
        <tr>
            <td>A confirmation email has been sent to you.&nbsp; Please confirm your account via the link in the email.
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>&nbsp;<asp:Button ID="Home" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                BorderStyle="Solid" BorderWidth="0px" CausesValidation="False" CommandName="Home" Width="80px"
                Text="Continue" Font-Names="Verdana" ForeColor="White" Height="28px"
                OnClick="HomeButton_Click" />
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnEnableRecap" runat="server" />
<asp:HiddenField ID="hdnisSupported" runat="server" />





