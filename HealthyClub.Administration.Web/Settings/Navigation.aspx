<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Navigation.aspx.cs" Inherits="HealthyClub.Administration.Web.Settings.Navigation" %>

<%@ Register Src="../UserControls/MenuNavigationUC.ascx" TagName="MenuNavigationUC"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Management</a>
                        <ul class="submenu">
                            <li><a>Categories</a> </li>
                            <li><a>Suburb </a></li>
                            <li><a>State </a></li>
                            <li><a>Keyword </a></li>
                            <li><a>Navigation </a></li>
                            <li><a>Mail Template </a></li>
                            <li><a>Mailer Setting </a></li>
                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox" style="height: 1000px">
            <h2>Top Menu</h2>
            <div class="block">
                <uc2:MenuNavigationUC ID="MenuNavigationUC1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
