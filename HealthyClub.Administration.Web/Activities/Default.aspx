<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.Activities.Default" %>

<%@ Register Src="~/UserControls/ActivitiesManagerUC.ascx" TagPrefix="uc1" TagName="ActivitiesManagerUC" %>
<%@ Register Src="~/UserControls/ActivitiesManagerListViewUC.ascx" TagPrefix="uc1" TagName="ActivitiesManagerListViewUC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
      <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Activity Setting</a>
                        <ul class="submenu">
                            <li><a>Categories</a> </li>
                            <li><a>Suburb </a></li>
                            <li><a>State </a></li>
                            <li><a>Keyword </a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Activities</h2>
            <div class="block">
                <uc1:ActivitiesManagerListViewUC runat="server" id="ActivitiesManagerListViewUC" />

            </div>
        </div>
    </div>
    
</asp:Content>
