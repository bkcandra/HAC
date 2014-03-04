<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityListing.ascx.cs"
    Inherits="HealthyClub.Web.UserControls.ActivityListing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="ActivityListingSidebar.ascx" TagName="ActivityListingSidebar" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ActivityListingNavigation.ascx" TagPrefix="uc1" TagName="ActivityListingNavigation" %>
<%@ Register Src="~/UserControls/ActivitiesListView.ascx" TagPrefix="uc1" TagName="ActivitiesListView" %>



<div style="font-weight: bold; font-size: 24px; color: #17438C; padding: 10px 0 5px">
    <h1>Find an Activity</h1>
</div>

<div style="font-size: 14px">
    Search for healthy activities by typing a keyword into the search bar,
    browsing by category or scrolling through our listed activities. To
    read about the benefits of becoming a Club Member
    <asp:HyperLink ID="hlnk" runat="server" Style="font-size: 14px" NavigateUrl="~/Pages/19">click here</asp:HyperLink>.
    <div style="margin: 5px;">
    </div>
</div>
<div class="SearchBox">
    <div class="Searchbar">
        <asp:TextBox ID="txtSearch" runat="server" Width="85%" CssClass="SearchText"></asp:TextBox>
        <asp:TextBoxWatermarkExtender ID="txtCalendarFrom_TextBoxWatermarkExtender" runat="server"
            TargetControlID="txtSearch" WatermarkText="E.g. Tennis">
        </asp:TextBoxWatermarkExtender>
        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="10%" CssClass="SearchButton"
            OnClick="btnSearch_Click" />
    </div>
    <%--<div class="SearchbarFoot">
        <asp:RadioButton ID="radDetailed" runat="server" Text="Detailed View" GroupName="SearchView"
            AutoPostBack="true" OnCheckedChanged="radDetailed_CheckedChanged"
            CssClass="bodyText1" />&nbsp;&nbsp;
                    <asp:RadioButton ID="radTable" runat="server" Text="Table View" GroupName="SearchView"
                        AutoPostBack="true" OnCheckedChanged="radTable_CheckedChanged"
                        CssClass="bodyText1" />
    </div>--%>
</div>
<br />
<div id="content_sidebar">
    <uc1:ActivityListingSidebar ID="ActivityListingSidebar1" runat="server" OnApplyFilter="ApplySidebarFilter" />
</div>
<div id="content_main">
    <div style="margin: 6px 0 0 25px">
        <uc1:ActivityListingNavigation runat="server" ID="ActivityListingNavigation" OnCloseNavEvent="CloseListingNav" />
    </div>
    <div id="divActivitiesList" runat="server" style="margin-left: 25px">
        <uc1:ActivitiesListView runat="server" ID="ActivitiesListView" OnRefreshActivitiesSection="RefreshActivitiesSection" />
    </div>
</div>
<div style="clear: both"></div>
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
    <asp:HiddenField ID="hdnAgeFrom" runat="server" />
    <asp:HiddenField ID="hdnFiltered" runat="server" />
    <asp:HiddenField ID="hdnFirstStart" runat="server" />
    <asp:HiddenField ID="hdnDateFrom" runat="server" />
    <asp:HiddenField ID="hdnDateTo" runat="server" />
    <asp:HiddenField ID="hdnTmTo" runat="server" />
    <asp:HiddenField ID="hdnTmFrom" runat="server" />
    <asp:HiddenField ID="hdnAgeTo" runat="server" />
    <asp:HiddenField ID="hdnSuburbID" runat="server" />
</div>
