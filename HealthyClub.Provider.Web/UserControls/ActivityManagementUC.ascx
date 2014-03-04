<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityManagementUC.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ActivityManagementUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="ActivityManagementSidebarUC.ascx" TagName="ActivityManagementSidebarUC"
    TagPrefix="uc1" %>
<style type="text/css">
    .bodySidebarLeft
    {
        width: 240px;
        vertical-align:top;
    }
    .bodySidebarRight
    {
        vertical-align: top;
    }
</style>
<br />
<div class="SearchBox">
    <table width="100%">
        <tr>
            <td>
                <div class="Searchbar">
                    <asp:TextBox ID="txtSearch" runat="server" Width="85%" CssClass="SearchText"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="txtCalendarFrom_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtSearch" WatermarkText="e.g Tennis">
                    </asp:TextBoxWatermarkExtender>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="10%" CssClass="SearchButton"
                        OnClick="btnSearch_Click" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="SearchbarFoot">
                    <asp:RadioButton ID="radDetailed" runat="server" Text="Detailed View" GroupName="SearchView"
                         AutoPostBack="true" OnCheckedChanged="radDetailed_CheckedChanged" Checked="true" />&nbsp;&nbsp;
                    <asp:RadioButton ID="radTable" runat="server" Text="Table View" GroupName="SearchView"
                        AutoPostBack="true" OnCheckedChanged="radTable_CheckedChanged" />
                </div>
            </td>
        </tr>
    </table>
</div>
<table style="width:100%" style="margin-top: 10px">
    <tr>
        <td style="width: 240px" valign="top">
            <uc1:ActivityManagementSidebarUC ID="ActivityManagementSidebarUC2" runat="server" />
        </td>
        <td style="vertical-align: top">
            <div id="divSearchViewContent" runat="server" style="margin-left: 25px;">
            </div>
        </td>
    </tr>
</table>
<br />
<div id="divHiddenContent">
    <asp:HiddenField ID="hdnSearchKey" runat="server" />
    <asp:HiddenField ID="hdnSortValue" runat="server" />
    <asp:HiddenField ID="hdnStartRow" runat="server" />
    <asp:HiddenField ID="hdnProviderID" runat="server" />
    <asp:HiddenField ID="hdnCategoryID" runat="server" />
    <asp:HiddenField ID="hdnPageSize" runat="server" />
    <asp:HiddenField ID="hdnViewType" runat="server" />
    <asp:HiddenField ID="hdnPage" runat="server" />
    <asp:HiddenField ID="hdnAdvSearch" runat="server" />
</div>
