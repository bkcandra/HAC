<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityRegistrationDescriptionUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.ActivityRegistrationDescription" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="ActivityRegistrationCategory.ascx" TagName="ActivityRegistrationCategory" TagPrefix="uc1" %>
<script src="../Scripts/controlHelper.js" type="text/javascript"></script>
<style type="text/css">
    .style1 {
        width: 300px;
        vertical-align: top;
        text-align: justify;
    }

    .style2 {
        width: 5px;
        vertical-align: top;
    }

    #TextArea1 {
        width: 280px;
    }

    .style3 {
        text-align: justify;
        width: 100%;
    }

    .style4 {
        width: 550px;
        vertical-align: text-top;
    }

    .auto-style1 {
        width: 250px;
        vertical-align: top;
        text-align: justify;
    }
</style>
<div>
    <span class="labelTitle">How do you categorise your activity and what is the cost?</span><br />
    Selecting a category for your activity means it will be listed in the search results
    when Club Members search for an activity by category.<br />
    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td class="style1">Activity Category
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <uc1:ActivityRegistrationCategory ID="ActivityRegistrationCategory1" runat="server" />
                    </td>
                </tr>


                <tr>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">Activity Type
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFeesCategory" runat="server" OnSelectedIndexChanged="ddlFeesCategory_SelectedIndexChanged"
                            AutoPostBack="true" Width="200px">
                            <asp:ListItem Value="0">Select one</asp:ListItem>
                            <asp:ListItem Value="1">Public, Free</asp:ListItem>
                            <asp:ListItem Value="2">Public, Fee Based</asp:ListItem>
                            <asp:ListItem Value="3">Private, Free</asp:ListItem>
                            <asp:ListItem Value="4">Private,  Fee Based</asp:ListItem>
                        </asp:DropDownList>
                        <span style="color: Red">*</span>
                    </td>
                </tr>
                <tr id="trEligibility" runat="server" visible="false">
                    <td class="style1" style="vertical-align: top">Activity Eligibility
                    </td>
                    <td class="style2" style="vertical-align: top">&nbsp;
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="physiotherapist Assessment" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Specific Resident Location" />
                                    &nbsp;Type
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Age" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:CheckBox ID="CheckBox4" runat="server" Text="Specific Sex" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trEligibility1" runat="server" visible="false">
                    <td class="style1">Requirements to Participate
                    </td>
                    <td class="style2">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtEligibility" runat="server" TextMode="MultiLine" Height="150px"
                            onkeyup="return validateLimit(this, 'lblEligibility', 500)" Width="600px"></asp:TextBox><br />
                        <div id="lblEligibility">
                            500 characters left
                        </div>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtEligibility"
                            WatermarkText="E.g. To participate in this activity you are required to have a medical clearance from your GP."
                            WatermarkCssClass="bodyText2">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </td>
                </tr>
            </table>
            <br />
            <div id="tableFees" runat="server">
                <span class="labelTitle">What is the cost of your activity?</span><br />
                Provide details on the prices for your activity including the cost of individual
                sessions, package deals and special promotions.
                <hr />
                <table>
                    <tr style="vertical-align: top;">
                        <td class="style1">Fee Information
                        </td>
                        <td class="style2">&nbsp;
                        </td>
                        <td class="style4">
                            <asp:TextBox ID="txtFee" runat="server" Rows="3" TextMode="MultiLine" Width="500px"
                                MaxLength="500" ToolTip="Summary:(500 characters)" onkeyup="return validateLimit(this, 'lblFee', 500)"
                                Height="100px"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtFee_TextBoxWatermarkExtender" runat="server"
                                TargetControlID="txtFee" WatermarkCssClass="bodyText2" WatermarkText="E.g. Individual sessions cost $20. We have packages of 10 sessions for $80 or you can pay $170 a month for unlimited sessions.">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <span style="color: Red;">*</span>
                            <br />
                            <div id="lblFee">
                                500 characters left
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<br />
<div style="width: 100%">
    <span class="labelTitle">How would you describe your activity?</span><br />
    <span class="style3">Club Members will see this description when they first view your
        activity.&nbsp; This is where you can promote your activity and include background
        information on your organisation, who it&#39;s suitable for, why it&#39;s a good
        activity to participate in and how to get involved.</span><br />
    <hr />
    <br />
    <table style="width: 100%">
        <tr>
            <td class="style1" style="vertical-align: top">Description
            </td>
            <td class="style2">&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtFullDesc" runat="server" TextMode="MultiLine" Height="150px"
                    Width="600px"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="txtFullDesc_TextBoxWatermarkExtender" runat="server"
                    TargetControlID="txtFullDesc" WatermarkText="&nbsp;" WatermarkCssClass="bodyText2">
                </ajaxToolkit:TextBoxWatermarkExtender>
            </td>
        </tr>
    </table>
    <br />
</div>
<br />
<asp:HiddenField ID="hdnEdit" runat="server" />
<asp:HiddenField ID="hdnActivityID" runat="server" />
<br />
<div style="width: 100%">
    <span class="labelTitle">Other Information</span><br />
    <hr />
    <br />
    <table style="width: 100%">
        <tr>
            <td class="auto-style1" style="vertical-align: top">Are individuals required to be a Member of your organisation in order to participate in this activity?
            </td>
            <td class="style2">&nbsp;
            </td>
            <td style="vertical-align: top">
                <asp:RadioButton ID="radIsMemberReqYes" GroupName="RadIsMemberReq" runat="server" Text="Yes, individuals are required to hold a membership with our organisation in order to participate" />
                <br />
                <asp:RadioButton ID="radIsMemberReqNo" GroupName="RadIsMemberReq" runat="server" Text="No, individuals are not required to hold a membership with our organisation in order to participate." Checked="true" />
            </td>
        </tr>
        <tr>
            <td class="auto-style1" style="vertical-align: top">Can individuals start this activity at any time?
            </td>
            <td class="style2">&nbsp;
            </td>
            <td style="vertical-align: top">
                <asp:RadioButton ID="radCommenceYes" GroupName="radCommence" runat="server" Text="Yes, individuals can start this activity at any time" />
                <br />
                <asp:RadioButton ID="radCommenceNo" GroupName="radCommence" runat="server" Text="No, individuals must start this activity on the listed commencement date." Checked="true" />
            </td>
        </tr>
    </table>
    <br />
</div>
