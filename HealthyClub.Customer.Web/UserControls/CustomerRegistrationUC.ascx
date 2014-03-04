<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerRegistrationUC.ascx.cs"
    Inherits="HealthyClub.Web.UserControls.CustomerRegistrationUC1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
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
        color: #17438C;
    }

    .style4 {
        font-size: small;
    }

    input {
        padding: 2px;
    }

    .style12 {
        width: 130px;
        font-size: 14px;
        font-family: Arial;
        font-weight: normal;
        color: #1B274F;
        line-height: 1.5em;
    }

    .style14 {
        width: 620px;
    }

    .style15 {
        height: 26px;
        color: Red;
    }

    .style16 {
        font-size: 14px;
        font-family: Arial;
        font-weight: normal;
        color: #1B274F;
        line-height: 1.5em;
        vertical-align: middle;
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

<div id="divError" runat="server" class="errorBox" visible="false" style="width: 940px">
    <span id="Title">Oops! You haven't finished entering in your details. </span>
    <span>
        <asp:Label ID="lblError" Visible="false" runat="server" Text=""></asp:Label>
    </span>
</div>
<div id="Registration" runat="server">
    <fieldset>
        <legend><span class="style3"><strong>Club Member Registration </strong></span>
            <br />
            <span style="font-size: 14px; font-weight: normal">Please enter the following details to 
                            register 
                as a Club Member.</span></legend>
        <div>
            <strong><span class="style3">Create your log in information </span></strong><span
                class="style4">&nbsp;(<span style="color: Red">* </span>indicates required field)
            </span>
            <br />
            <hr />
        </div>
        <table>
            <tr>
                <td class="style12">
                    <asp:Label ID="lblUsername" AssociatedControlID="UserName" runat="server">Username</asp:Label>
                </td>
                <td class="style1">&nbsp;
                </td>
                <td class="style15">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="Username" Width="200px" runat="server" AutoPostBack="true" MaxLength="50" OnTextChanged="Username_TextChanged" CssClass="textbox"></asp:TextBox><span
                                style="color: Red"> *</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Username"
                                CssClass="bodyText1" ToolTip="User Name is required." ValidationGroup="CreateNewMember"><span class="required">&nbsp;Required</span></asp:RequiredFieldValidator>
                            &nbsp;
                    
                    <asp:Label ID="lblUsernameError" runat="server" Text="This username has been taken." Visible="false" ForeColor="Red"></asp:Label>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style12">
                    <asp:Label ID="lblPassword1" runat="server" AssociatedControlID="Password">Password</asp:Label>
                </td>
                <td class="style1">&nbsp;
                </td>
                <td class="style15">
                    <asp:TextBox ID="Password" Width="200px" MaxLength="50" runat="server" TextMode="Password" name="Password" CssClass="textbox"></asp:TextBox><span
                        style="color: Red"> *</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password"
                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateNewMember">&nbsp;Required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style12">
                    <asp:Label ID="Label1" runat="server">Retype Password</asp:Label>
                </td>
                <td class="style1">&nbsp;
                </td>
                <td class="style15">
                    <asp:TextBox ID="txtPasswordVerify" TextMode="Password" MaxLength="50" Width="200px" runat="server" name="confirmPasswordInput" CssClass="textbox"></asp:TextBox><span
                        style="color: Red"> *</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPasswordVerify"
                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                        ValidationGroup="CreateNewMember">&nbsp;Required</asp:RequiredFieldValidator>
                    &nbsp;&nbsp;
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                        ControlToValidate="txtPasswordVerify" Display="Dynamic" ErrorMessage="The password and confirmation password must match."
                                        Style="color: #FF0000" ValidationGroup="CreateNewMember"></asp:CompareValidator>
                </td>
            </tr>

        </table>
        <div id="pswd_info">
            <h4>Password must meet the following requirements:</h4>
            <ul>
                <li id="letter" class="invalid">At least <strong>one letter</strong></li>
                <li id="capital" class="invalid">At least <strong>one capital letter</strong></li>
                <li id="number" class="invalid">At least <strong>one number</strong></li>
                <li id="length" class="invalid">At least <strong>six characters</strong></li>
            </ul>
        </div>
        <asp:HiddenField ID="hdnpswd_info" runat="server" />
        <div id="rest">
            <br />
            <br />
            <span class="style4"><strong><span class="style3">Personal Details</span></strong></span>
            <hr />
            <table style="width: 100%">
                <tr>
                    <td class="style16">Name
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:DropDownList ID="ddlTitle" runat="server" TabIndex="0">
                            <asp:ListItem Value="0">Title</asp:ListItem>
                            <asp:ListItem Value="1">Mr</asp:ListItem>
                            <asp:ListItem Value="2">Mrs</asp:ListItem>
                            <asp:ListItem Value="3">Ms</asp:ListItem>
                            <asp:ListItem Value="4">Miss</asp:ListItem>
                            <asp:ListItem Value="5">Dr</asp:ListItem>
                            <asp:ListItem Value="6">Rev</asp:ListItem>
                        </asp:DropDownList><span style="color: Red">
                                        *</span>
                        &nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtFirstName" Width="150px" MaxLength="50" runat="server" CssClass="textbox"></asp:TextBox><span style="color: Red">
                                        *</span>
                        &nbsp;&nbsp;
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFirstName"
                            WatermarkText="Given Name">
                        </asp:TextBoxWatermarkExtender>
                        <asp:TextBox ID="txtLastName" Width="150px" MaxLength="50" runat="server" CssClass="textbox"></asp:TextBox><span style="color: Red">
                                        *</span>
                        <asp:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtLastName" WatermarkText="Surname">
                        </asp:TextBoxWatermarkExtender>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLastName"
                            ErrorMessage="" ToolTip="LastName is required." ValidationGroup="CreateNewMember" CssClass="required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style16">Gender
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:RadioButton ID="radMale" Text="Male" runat="server" Checked="True" GroupName="CustomerGender" />
                        &nbsp;&nbsp;
                                    <asp:RadioButton ID="radFemale" Text="Female" runat="server" GroupName="CustomerGender" />
                    </td>
                </tr>
                <tr>
                    <td class="style16">Date of Birth
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="txtDOB" runat="server" Width="90px" MaxLength="1" Style="text-align: justify" CssClass="textbox"
                            ValidationGroup="MKE" /><span style="color: Red">*</span>
                        <asp:TextBoxWatermarkExtender ID="txtDOB_TextBoxWatermarkExtender" WatermarkText="dd/mm/yyyy"
                            runat="server" TargetControlID="txtDOB">
                        </asp:TextBoxWatermarkExtender>
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" CultureName="en-AU" runat="server"
                            TargetControlID="txtDOB" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="None" AcceptNegative="None"
                            ErrorTooltipEnabled="True" />
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDOB"
                            ErrorMessage="Date of Birth is required." ToolTip="Date of Birth is required."
                            ValidationGroup="CreateNewMember" CssClass="required">&nbsp;Required</asp:RequiredFieldValidator>
                        &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDOB"
                        Style="color: #FF0000" Text="Invalid date of birth" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="style16">
                        <asp:Label ID="lblEmail" runat="server" AssociatedControlID="Email">E-mail</asp:Label>
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="Email" Width="200px" MaxLength="50" runat="server" AutoPostBack="true" OnTextChanged="Email_TextChanged" CssClass="textbox"></asp:TextBox><span style="color: Red">
                                        *</span>
                                <asp:RequiredFieldValidator ID="ReqEmailAddress" runat="server" ControlToValidate="Email"
                                    ErrorMessage="E-mail is required." ToolTip="E-mail is required." CssClass="required" ValidationGroup="CreateNewMember">&nbsp;Required</asp:RequiredFieldValidator>
                                &nbsp;<asp:Label ID="lblEmailTaken" runat="server" Text="Email address has already been registered." Visible="false" ForeColor="Red"></asp:Label>
                                &nbsp;<asp:Label ID="lblInvalidEmail" runat="server" Text="Invalid email address." Visible="false" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Email" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="style16">Retype E-mail
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="txtEmailVerify" Width="200px" MaxLength="50" runat="server" CssClass="textbox"></asp:TextBox>
                        <span style="color: Red">*</span>
                        &nbsp;<asp:CompareValidator ID="EmailCompare" runat="server" ControlToCompare="Email" ControlToValidate="txtEmailVerify"
                            ErrorMessage="The email and confirmation email must match." Style="color: #FF0000"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style16">Postal Address
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="txtAddress1" Width="450px" MaxLength="50" runat="server" CssClass="textbox"></asp:TextBox><span style="color: Red">
                                        *</span>
                        <asp:TextBoxWatermarkExtender ID="txtAddress1_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtAddress1" WatermarkText="Street number and name">
                        </asp:TextBoxWatermarkExtender>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress1"
                            ErrorMessage="Address is required." ToolTip="Address is required." ValidationGroup="CreateNewMember" CssClass="required">&nbsp;Required</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style16">&nbsp;
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="txtSuburb" runat="server" MaxLength="50" Width="150px" CssClass="textbox"></asp:TextBox><span style="color: Red">
                                        *</span>
                        <asp:TextBoxWatermarkExtender ID="txtSuburb_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtSuburb" WatermarkText="Suburb">
                        </asp:TextBoxWatermarkExtender>
                        &nbsp;
                                    <asp:DropDownList ID="ddlState" runat="server">
                                    </asp:DropDownList>
                        <span style="color: Red">*</span> &nbsp;&nbsp;
                                    <asp:TextBox ID="txtPostCode" Width="80px" runat="server" MaxLength="4" onkeypress="return CheckNumber(event)" CssClass="textbox"></asp:TextBox><span style="color: Red">
                                        *</span>
                        <asp:TextBoxWatermarkExtender ID="txtPostCode_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtPostCode" WatermarkText="Postcode">
                        </asp:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style16">Contact Number
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="txtContactNumber" Width="150px" runat="server" CssClass="textbox"></asp:TextBox><span
                            style="color: Red"> *</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContactNumber"
                            ErrorMessage="Phone Number is required." ToolTip="Phone Number is required."
                            ValidationGroup="CreateNewMember" CssClass="required">&nbsp;Required</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style16">Alternative Number
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="txtMobileNumber" Width="150px" runat="server" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style16">How do you prefer to be contacted?&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style14">
                        <asp:RadioButton ID="radiobyEMail" runat="server" Checked="true" GroupName="Contact"
                            Text="Email" />
                        &nbsp;&nbsp;
                                    <asp:RadioButton ID="radiobyPhone" runat="server" GroupName="Contact" Text="Phone" />
                        &nbsp;&nbsp;
                                    <asp:RadioButton ID="radiobyMail" runat="server" GroupName="Contact" Text="Mail" />
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
                    <td colspan="3">To accept the terms of service click &quot;I accept.&quot; By clicking I accept
                                    you are agreeing to the above Terms and Conditions and Privacy Statement.&nbsp;
                                    If you do not agree with these terms, click &quot;Cancel&quot;<br />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr id="trCaptcha" runat="server">
                    <td colspan="3">Please enter the code below
                    <recaptcha:RecaptchaControl
                        ID="RecaptchaControl1" PrivateKey=" " PublicKey=" "
                        runat="server"
                        Theme="clean" />
                    </td>
                </tr>
                <tr align="right">
                    <td colspan="3">
                        <asp:Button ID="StepNextButton" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                            BorderStyle="Solid" BorderWidth="0px" CommandName="MoveNext" Font-Names="Verdana"
                            ForeColor="White" Height="28px" Text="I accept" ValidationGroup="CreateNewMember"
                            Width="80px" OnClick="StepNextButton_Click" />
                        &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="CancelButton" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                                    BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" CommandName="Cancel"
                                    Font-Names="Verdana" ForeColor="White" Height="28px" Text="Cancel" ValidationGroup="CreateNewMember"
                                    Width="80px" OnClientClick="history.back();" />
                    </td>
                </tr>

            </table>
            <br />
        </div>
    </fieldset>

</div>
<div id="completeRegistration" runat="server" visible="false">
    <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto">
        <tr>
            <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Confirm your account
            </td>
        </tr>
        <tr>
            <td>A confirmation email has been sent to you.&nbsp; Please confirm your account via the link in the email.</td>
        </tr>
        <tr>
            <td align="right">&nbsp;<asp:Button ID="Home" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                BorderStyle="Solid" BorderWidth="0px" CausesValidation="False" CommandName="Home" Width="80px"
                Text="Home" Font-Names="Verdana" ForeColor="White" Height="28px" ValidationGroup="CreateNewMember"
                OnClick="HomeButton_Click" />
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnEnableRecap" runat="server" />

