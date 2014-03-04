<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportUC.ascx.cs" Inherits="HealthyClub.Providers.Web.UserControls.ReportUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>

<style type="text/css">
    .style1 {
        color: Black;
        width: 5px;
        vertical-align: top;
        font-size: 13px;
    }

    .style3 {
        width: 180px;
        vertical-align: top;
    }
    .auto-style1 {
        width: 180px;
    }
    .auto-style2 {
        width: 181px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
            <label class="pageTitle">
                My Reports
            <br />
            </label>
            <label class="bodyText1">
                This page allows you to produce customised reports of the activities you 
        have listed. These reports are only for your activities. If you require other 
        customised reports please 
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ContactUs/"> contact us</asp:HyperLink>

            </label>
            <hr />
            <br />
            <table style="width: 720px">
                <tr>
                    <td class="style3">Page Orientation
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:RadioButton ID="radPotrait" Text="Portrait" runat="server" GroupName="radOrientation"
                            Checked="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="radLandscape" Text="Landscape" runat="server" GroupName="radOrientation" />
                    </td>
                </tr>
                <tr>
                    <td class="style3">Report Type
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:RadioButton ID="radWithTimetable" Text="Activities with a timetable" runat="server" GroupName="radListing"
                            Checked="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="radNoTimetable" Text="Activities with no timetable" runat="server"
                        GroupName="radListing" />
                    </td>
                </tr>
                <tr>
                    <td class="style3">Categories
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategories" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="ddlCategory2" runat="server" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">Suburb
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="ddlSuburbs" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">Age
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td>from
                            <asp:TextBox ID="txtAgeFrom" runat="server" Width="100px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99"
                            MaskType="Number" TargetControlID="txtAgeFrom">
                        </asp:MaskedEditExtender>
                        &nbsp;&nbsp;&nbsp; to
                            <asp:TextBox ID="txtAgeTo" runat="server" Width="100px"></asp:TextBox>
                        <asp:MaskedEditExtender ID="TextBox3_MaskedEditExtender" runat="server"
                            Mask="99" MaskType="Number" TargetControlID="txtAgeTo">
                        </asp:MaskedEditExtender>
                        &nbsp;year olds
                    </td>
                </tr>
                <tr>
                    <td class="style3">Title of Report</td>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">Options
                    </td>
                    <td class="style1">&nbsp;</td>
                    <td>
                        <table>
                            <tr>
                                <td class="auto-style1">
                                    <asp:CheckBox ID="chkName" runat="server" Text="Activity Name" Checked="true" />
                                </td>
                                <td class="auto-style2">&nbsp;
                                <asp:CheckBox ID="chkShortDescription" runat="server" Text="Activity Description" Checked="true" />
                                </td>
                                <td>&nbsp;
                                <asp:CheckBox ID="chkPrice" runat="server" Text="Activity Cost" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    <asp:CheckBox ID="chkWebsite" runat="server" Text="Organisation Website" Checked="true" />
                                </td>
                                <td class="auto-style2">&nbsp;
                                <asp:CheckBox ID="chkAddress" runat="server" Text="Activity Address" Checked="true" />
                                </td>
                                <td>&nbsp;
                                <asp:CheckBox ID="chkEligibility" runat="server" Text="Eligibility" Checked="true" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>                
                <tr>
                    <td class="style3"></td>
                    <td class="style1"></td>
                    <td>
                        <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" OnClick="btnGenerateReport_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hdnProviderID" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
