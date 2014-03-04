<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.ascx.cs" Inherits="HealthyClub.Providers.Web.UserControls.ContactUs" %>
<style type="text/css">
    .styleContactUs {
        width: 132px;
        vertical-align: top;
    }

    .style1 {
        font-size: 24px;
        text-align: left;
        color: #17438C;
        font-weight: bold;
        padding-top: 10px;
    }
</style>
<div id="divContactUs" runat="server">
   
        <div class="style1">
             <h1>Contact Us</h1>
        </div>
    
    <br />
    <table>
        <tr>
            <td>If you have any queries, compliments, complaints or issues relating
            to the Club or its listings please contact us by email, phone or by completing our
            online form below.</td>
        </tr>
        <tr>
            <td>(<span style="color: Red">* </span>indicates required field)</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Email: <a href="mailto:healthyclub@iechs.org.au">healthyclub@iechs.org.au</a></td>
        </tr>
        <tr>
            <td>Phone: 0408 259 537</td>
        </tr>
    </table>
    <br />
    <table>
        <tr>

            <td class="styleContactUs">Name
            </td>
            <td></td>
            <td>
                <asp:TextBox ID="txtContactPerson" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>

            <td class="styleContactUs">Phone Number
            </td>
            <td></td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>

            <td class="styleContactUs">Email
            </td>
            <td></td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                &nbsp;
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                    runat="server" ErrorMessage="Please enter a valid email address" ForeColor="Red"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id="trRemarks" runat="server">

            <td class="styleContactUs">Message
            </td>
            <td style="vertical-align: top"></td>
            <td>
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="105px" Width="515px"></asp:TextBox><span style="color: Red; vertical-align: top">*</span>
            </td>
        </tr>
        <tr>

            <td>How do you prefer to be contacted?
            </td>
            <td></td>
            <td>
                <asp:RadioButton ID="radiobyEMail" runat="server" Checked="true" GroupName="Contact"
                    Text="Email" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="radiobyPhone" runat="server" GroupName="Contact" Text="Phone" />
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>

            <td>&nbsp;
            </td>
            <td></td>
            <td style="color: #FF0000">
                <br />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>

            <td style="color: #FF0000">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRemarks"
                    ErrorMessage="Message must not be empty"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>

            <td style="color: #FF0000">
                <asp:Button ID="btnNextStep" runat="server" CssClass="button button-login" OnClick="btnNextStep_Click" Text="Submit" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnIsEnquiry" runat="server" />
</div>
<div id="CompleteContactUs" runat="server" visible="false">
    <table style="font-family: Verdana; font-size: 100%; width: 800px; margin: 20px auto;">
        <tr>
            <td align="center" style="color: White; background-color: #499ECA; font-weight: bold;">Message Sent
            </td>
        </tr>
        <tr>
            <td>Thank you for contacting us.  If you left your details we will be in contact shortly.</td>
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
