<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardsListing.ascx.cs" Inherits="HealthyClub.Web.UserControls.RewardsListing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="RewardSidebar.ascx" TagName="RewardSidebar" TagPrefix="uc1" %>



<link href="../Styles/BoroondaraClub_ListViewTab.css" rel="stylesheet" type="text/css" />
<div id="sidetext">
<span style="font-weight: bold; font-size: 24px; color: #17438C;">Find a Reward</span>
<div style="margin: 5px;">
</div>
<div style="font-size: 14px">
    Use the menu on the left or the search function to find a reward for your healthy activities.  Simply click on a reward and add it to your shopping cart
    
    <div style="margin: 5px;">
    </div>
</div>
<div class="SearchBox">
    <div class="Searchbar1">
        <asp:TextBox ID="txtSearch" runat="server" Width="80%" CssClass="SearchText"></asp:TextBox>
        <asp:TextBoxWatermarkExtender ID="txtCalendarFrom_TextBoxWatermarkExtender" runat="server"
            TargetControlID="txtSearch" WatermarkText="E.g. Discounts">
          
        </asp:TextBoxWatermarkExtender>
        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="14%" CssClass="SearchButton" OnClick="btnSearch_Click1" />
    </div>
    <div class="SearchbarFoot">
        
                    <asp:RadioButton ID="radTable" runat="server" Text="Tiles View" GroupName="SearchView"
                        AutoPostBack="true" OnCheckedChanged="radTable_CheckedChanged"
                        CssClass="bodyText1" />&nbsp;&nbsp;
        <asp:RadioButton ID="radDetailed" runat="server" Text="Detailed View" GroupName="SearchView"
            AutoPostBack="true" OnCheckedChanged="radDetailed_CheckedChanged"
            CssClass="bodyText1" />
    </div>
     </div>
    </div>
<div id="content_sidebar" style="float:none">
    <uc1:rewardsidebar ID="RewardSidebar1" runat="server" OnPushFilter="PushSidebarFilter" />
    </div>
<div id="content_main" style="margin-top:-490px">
    <div id="divSearchViewContent" runat="server" style="margin-left: 15px">
    </div>
</div>
<div style="clear:both"></div>
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
    <asp:HiddenField ID="hdnAgeTo" runat="server" />
    <asp:HiddenField ID="hdnRewardType" runat="server" />
</div>


     


