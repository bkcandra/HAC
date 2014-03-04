<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityManagementSidebarUC.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ActivityManagementSidebarUC" %>
<style type="text/css">
    ul.navigation
    {
        list-style: none;
        margin: 0;
        padding: 0;
        text-align: left;
    }
    
    ul.navigation li
    {
        display: inline-block;
        margin: 0px;
        padding: 0;
    }
    
    ul.navigation a
    {
        display: block;
        width: 190px;
        height: 33px;
        padding: 12px 0 0 20px;
        margin-bottom: 5px;
  
        font-size: 16px;
        font-weight: normal;
        text-decoration: none;
        position: relative;
    }

    ul.navigation a:hover, ul.navigation a.selected
    {
    font-weight:bold;
        background: url(../Styles/StyleImages/navMenu_hover.png) no-repeat left;
    }
    
    ul.navigation a:focus
    {
        outline: none;
    }
</style>
<div class="sidebar_box">
    <div class="sb_title">
        Categories</div>
    <div class="sb_content">
        <ul class="navigation">
            <li>
                <asp:HyperLink ID="hlnkMenu1" runat="server" NavigateUrl="~/Activities/">View my activities</asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="lnkProductName" runat="server" EnableTheming="true" Text="Add new activity"
                    NavigateUrl="~/Activities/NewActivity.aspx"></asp:HyperLink></li>
        </ul>
    </div>
    <div class="sb_bottom">
    </div>
</div>
