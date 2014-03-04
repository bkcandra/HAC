<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="HealthyClub.Administration.Web.User.Member" %>
<%@ Register src="../UserControls/CustomerAccount.ascx" tagname="CustomerAccount" tagprefix="uc1" %>
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
                                <asp:HyperLink ID="hlnkSum" runat="server" NavigateUrl="~">Home</asp:HyperLink></li>

                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Member Details</h2>
            <div class="block">
                <uc1:CustomerAccount ID="CustomerAccount1" runat="server" />
            </div>
        </div>
    </div>
    
</asp:Content>
