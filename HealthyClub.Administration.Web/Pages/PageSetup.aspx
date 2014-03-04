<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PageSetup.aspx.cs" Inherits="HealthyClub.Administration.Web.Pages.PageSetup" %>

<%@ Register Src="../UserControls/PageSetupUC.ascx" TagName="PageSetupUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Page Template</a>
                        <ul class="submenu">
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <uc1:PageSetupUC ID="PageSetupUC1" runat="server" />
</asp:Content>
