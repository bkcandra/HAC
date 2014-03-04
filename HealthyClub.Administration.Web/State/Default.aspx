<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.State.Default" %>

<%@ Register Src="../UserControls/StateUC.ascx" TagName="StateUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Links</a>
                        <ul class="submenu">
                            <li><a>Suburb </a></li>
                            <li><a>State </a></li>
                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>State</h2>
            <div class="block">
                <uc1:StateUC ID="StateUC1" runat="server" />
            </div>
        </div>
    </div>

</asp:Content>
