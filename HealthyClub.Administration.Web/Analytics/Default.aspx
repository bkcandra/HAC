<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.Analytics.Default" %>

<%@ Register Src="~/UserControls/AnalyticsUC.ascx" TagPrefix="uc1" TagName="AnalyticsUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Menu</a>
                        <ul class="submenu">
                            <li>
                                <asp:HyperLink ID="hlnkSum" runat="server" NavigateUrl="~">Summary</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="hlnkAnalytics" runat="server" NavigateUrl="~/Analytics/">Analytics</asp:HyperLink></li>

                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Analytics</h2>
            <div class="block">
                <uc1:AnalyticsUC runat="server" ID="AnalyticsUC1" />
            </div>
        </div>
    </div>
    
</asp:Content>
