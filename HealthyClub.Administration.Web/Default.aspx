<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web._Default" %>

<%@ Register Src="UserControls/DashboardSummaryUC.ascx" TagName="DashboardSummaryUC" TagPrefix="uc1" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!--[if IE 6]><link rel="stylesheet" type="text/css" href="Content/ie6.css" media="screen" /><![endif]-->
    <!--[if IE 7]><link rel="stylesheet" type="text/css" href="Content/ie.css" media="screen" /><![endif]-->

    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Menu</a>
                        <ul class="submenu">
                            <li>
                                <asp:HyperLink ID="hlnkSum" runat="server" NavigateUrl="~">Summary</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink ID="hlnkAnalytics" runat="server" NavigateUrl="https://www.google.com/analytics/web/?hl=en#report/visitors-overview/a43779221w73886885p76315108/" Target="_blank">Analytics</asp:HyperLink></li>
                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        


            <uc1:DashboardSummaryUC ID="DashboardSummaryUC1" runat="server" />


        
    </div>


</asp:Content>
