<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityRegistrationDetailUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.ActivityRegistrationDetailUC" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="../Scripts/controlHelper.js"></script>
<style type="text/css">
    .style1 {
        width: 150px;
        vertical-align: top;
        text-align: left;
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
    Please compl<span style="text-align: justify; width: 100%">ete the following steps to
        list an activity.&nbsp; If you are unsure if your activity is suitable, please view
        our activity guidelines or contact us.&nbsp; The Club reviews all activities prior
        to being visible to the public.&nbsp; This can take up to two working days.&nbsp;
        If we require further information or are unable to list your activity we will contact
        you via the details you have provided below. </span>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Activity Name" CssClass="labelTitle"></asp:Label>&nbsp;(<span
        style="color: Red;">*</span>
    indicates required field)
    <hr />
    <table style="width: 100%">
        <tr>
            <td class="style1">Activity Name
            </td>
            <td class="style2">&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtActivityName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                <span class="style6">*</span>
            </td>
        </tr>
        <tr>
            <td class="style1">Activity/Organisation Website
            </td>
            <td class="style2">http://
            </td>
            <td>
                <asp:TextBox ID="txtWebsite" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<br />
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div>
            <span class="labelTitle">What are your activity location and contact details?</span><br />
            Your primary contact details will be available to Club Members and the Healthy Australia
            Club team.<br />
            <br />
            To use the same contact details as when you registered your organisation, please
            check the box below.&nbsp; If you have different contact details for this activity,
            please complete the section below.
            <hr />
            <table width="100%" id="tabProvider" runat="server">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkUseProvider" runat="server" Text=" I want to use my organisations contact details"
                            OnCheckedChanged="chkUseProvider_CheckedChanged" Checked="false" AutoPostBack="true" />
                        <asp:HiddenField ID="hdnProviderUsername" runat="server" />
                    </td>
                </tr>
            </table>
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
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtLastName" Width="150px" runat="server"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtLastName" WatermarkText="Surname" WatermarkCssClass="bodyText2">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <span class="style6">*</span>
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
                <tr>
                    <td class="style5">&nbsp;</td>
                    <td class="style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="style5">Address of Activity
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress1" Width="450px" runat="server" OnClick="this.value=''"></asp:TextBox>
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
                        <asp:TextBox ID="txtPostCode" Width="75px" Text="Postcode" runat="server"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="txtPostCode_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" MaskType="Number" TargetControlID="txtPostCode" Mask="9999">
                        </ajaxToolkit:MaskedEditExtender>
                        <span class="style6">*</span>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <span class="labelTitle">Who is your secondary contact person?</span><br />
            These contact details are <span class="style10">only</span> visible to the Healthy
            Australia Club team in case we can't reach you through your primary contact details.&nbsp;
            These contact details will not be visible to the public.
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
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
                        <asp:TextBox ID="txtMiddleName0" Width="150px" runat="server" OnClick="this.value=''"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="txtMiddleName0_TextBoxWatermarkExtender" runat="server"
                                    TargetControlID="txtMiddleName0" WatermarkText="Middle Name" WatermarkCssClass="bodyText2">
                                </ajaxToolkit:TextBoxWatermarkExtender>
                                &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtLastName0" Width="150px" runat="server" OnClick="this.value=''"></asp:TextBox>
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
                            Style="color: #FF0000" Text="Invalid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="chkUseSecondaryContact" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnEdit" runat="server" />
<asp:HiddenField ID="hdnACtivityID" runat="server" />
