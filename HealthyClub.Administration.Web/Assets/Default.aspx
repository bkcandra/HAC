<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.Assets.Default" %>

<%@ Register Src="../UserControls/WebAssetsUC.ascx" TagName="WebAssetsUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Menu</a>
                        <ul class="submenu">
                            
                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="sidebar box">
            <h2>Web Assets</h2>
            <div class="block">
                <uc1:WebAssetsUC ID="WebAssetsUC1" runat="server" />
            </div>
        </div>
    </div>


</asp:Content>
