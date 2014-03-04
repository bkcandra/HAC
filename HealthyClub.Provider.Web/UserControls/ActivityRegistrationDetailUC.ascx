<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityRegistrationDetailUC.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ActivityRegistrationDetailUC" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="../Scripts/controlHelper.js"></script>

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
        width: 125px;
        vertical-align: top;
    }

    .style2 {
        width: 5px;
        vertical-align: top;
    }

    .style5 {
        width: 200px;
    }

    .labelTitle {
        font-size: large;
        font-weight: bold;
    }

    .style6 {
        color: #FF0000;
        vertical-align: top;
    }
</style>
<div>
    Please compl<span style="text-align: justify; width: 100%">ete the following steps to list an activity.</span><br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Activity Name" CssClass="labelTitle"></asp:Label>&nbsp;(<span
        style="color: Red;">*</span>
    indicates required field)
    <hr />
    <table>
        <tr>
            <td style="width: 200px;">Activity Name
            </td>
            <td class="style2"></td>
            <td>
                <asp:TextBox ID="txtActivityName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                <span class="style6">*</span>
            </td>
        </tr>
        <tr>
            <td>Activity/Organisation Website
            </td>
            <td>http://
            </td>
            <td>
                <asp:TextBox ID="txtWebsite" runat="server" Width="200px" MaxLength="100" ToolTip="Please enter your website url without 'http://' prefix"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<br />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div>
            <span class="labelTitle">What are your contact details?</span><br />
            These contact details will be available to Club Members and the Healthy Australia Club team.<br />
            <br />
            To use the same contact details as when you registered your organisation, check the box below.&nbsp; Otherwise, please complete the section below.
            <hr />
            <table width="100%" id="tabProvider" runat="server">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkUseProvider" runat="server" Text=" I want to use my organisations contact details"
                            OnCheckedChanged="chkUseProvider_CheckedChanged" Checked="false" AutoPostBack="true" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnProviderUsername" runat="server" />
            <table style="width: 100%">
                <tr>
                    <td class="style5">Name
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
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
                        <asp:TextBox ID="txtFirstName" Width="300px" runat="server"></asp:TextBox><span class="style6">*</span>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFirstName" WatermarkText="Given Name" WatermarkCssClass="bodyText2">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtLastName" Width="150px" Visible="false" runat="server"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtLastName" WatermarkText="Surname" WatermarkCssClass="bodyText2">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style5">Email Address
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" Width="225px" runat="server"></asp:TextBox>
                        <span class="style6">*</span>&nbsp;&nbsp;
                        <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmail"
                            Style="color: #FF0000" Text="Invalid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </td>
                </tr>
                <tr>
                    <td class="style5">Retype Email Address
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmEmailAddress" Width="225px" runat="server"></asp:TextBox>
                        <span class="style6">*</span>&nbsp;&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                            Style="color: #FF0000" Text="Invalid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </td>
                </tr>
                <tr>
                    <td class="style5">Primary Contact Number
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtContactNumber" Width="200px" runat="server"></asp:TextBox>
                        <span class="style6">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="style5">Alternative Contact Number
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile" Width="200px" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="Label2" runat="server" Text="What is the address of your activity?" CssClass="labelTitle"></asp:Label>&nbsp;(<span
                style="color: Red;">*</span>
            indicates required field)
    <hr />
            <table>
                <tr>
                    <td class="style5">Address of Activity
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress1" Width="450px" runat="server"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtAddress1_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtAddress1" WatermarkText="Street Number and Name" WatermarkCssClass="bodyText2">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <span class="style6">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="style5">&nbsp;
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSuburbs" runat="server">
                        </asp:DropDownList>
                        <span class="style6">*</span> &nbsp;
                        <asp:DropDownList ID="ddlState" runat="server">
                        </asp:DropDownList>
                        <span class="style6">*</span> &nbsp;
                        <asp:TextBox ID="txtPostCode" Width="75px" runat="server" MaxLength="4" onkeypress="return CheckNumber(event)"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtPostCode_TextBoxWatermarkExtender" runat="server" WatermarkText="Postcode" TargetControlID="txtPostCode">
                        </ajaxToolkit:TextBoxWatermarkExtender>



                        <span class="style6">*</span>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <span class="labelTitle">Who is your secondary contact person?</span><br />
            These contact details are <span class="style10">only</span> visible to the Club in case we can&#39;t reach you through your primary contact details.&nbsp;&nbsp;
            <hr />

            <table width="100%" id="Table1" runat="server">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkUseSecondaryContact" runat="server" Text=" I don't have any secondary contact details"
                            Checked="false" AutoPostBack="true" OnCheckedChanged="chkUseSecondaryContact_CheckedChanged" />
                        <asp:HiddenField ID="hdnUsingSecondary" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tabSecondaryContact" runat="server" width="100%">
                <tr>
                    <td class="style5">Name
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTitle0" runat="server" TabIndex="0">
                            <asp:ListItem Value="0">Title</asp:ListItem>
                            <asp:ListItem Value="1">Mr</asp:ListItem>
                            <asp:ListItem Value="2">Mrs</asp:ListItem>
                            <asp:ListItem Value="3">Ms</asp:ListItem>
                            <asp:ListItem Value="4">Miss</asp:ListItem>
                            <asp:ListItem Value="5">Dr</asp:ListItem>
                            <asp:ListItem Value="6">Rev</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFirstName0" Width="150px" runat="server"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtFirstName0_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFirstName0" WatermarkText="Given Name" WatermarkCssClass="bodyText2">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtLastName0" Width="150px" runat="server"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtLastName0_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtLastName0" WatermarkText="Surname" WatermarkCssClass="bodyText2">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style5">Email Address
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailalt" Width="225px" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:RegularExpressionValidator ID="regEmail0" runat="server" ControlToValidate="txtEmailalt"
                            Style="color: #FF0000" Text="Invalid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </td>
                </tr>
                <tr>
                    <td class="style5">Retype Email Address
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmEmailalt" Width="225px" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtConfirmEmailalt"
                            Style="color: #FF0000" Text="Invalid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </td>
                </tr>
                <tr>
                    <td class="style5">Primary Contact Number
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtContactNumber0" Width="200px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style5">Alternative Contact Number
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile0" Width="200px" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>

        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlTitle" />
        <asp:AsyncPostBackTrigger ControlID="chkUseProvider" />
        <asp:AsyncPostBackTrigger ControlID="chkUseSecondaryContact" />
    </Triggers>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnEdit" runat="server" />
<asp:HiddenField ID="hdnACtivityID" runat="server" />
