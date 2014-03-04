<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.Council.Default" %>

<%@ Register Src="~/UserControls/CouncilUC.ascx" TagPrefix="uc1" TagName="CouncilUC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Council</a>
                        <ul class="submenu">
                            <li><a>New Council</a> </li>
                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Council</h2>
            <div class="block">
                <uc1:CouncilUC runat="server" id="CouncilUC" />
            </div>
        </div>
    </div>

</asp:Content>
