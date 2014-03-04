<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.Suburb.Default" %>

<%@ Register Src="../UserControls/SuburbUC.ascx" TagName="SuburbUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Management</a>
                        <ul class="submenu">
                            <li>
                                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Categories" runat="server">Categories</asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Keyword" runat="server">Keyword</asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Suburb" runat="server">Suburb</asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink ID="HyperLink2" NavigateUrl="~/State" runat="server">State</asp:HyperLink>
                            </li>


                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Suburb</h2>
            <div class="block">
                <uc1:SuburbUC ID="SuburbUC1" runat="server" />
            </div>
        </div>
    </div>



</asp:Content>
