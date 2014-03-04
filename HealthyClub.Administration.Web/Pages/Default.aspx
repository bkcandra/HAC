<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.Pages.Default" %>

<%@ Register Src="../UserControls/PagesUC.ascx" TagName="PagesUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Pages</a>
                        <ul class="submenu">
                            <li>
                                <asp:HyperLink ID="hlnkNewPage" runat="server" Text="New Page"></asp:HyperLink></li>

                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Pages</h2>            
            <div class="block">
                <uc1:PagesUC ID="PagesUC1" runat="server" />
            </div>
        </div>
    </div>

</asp:Content>
