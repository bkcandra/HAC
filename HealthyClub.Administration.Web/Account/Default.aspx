<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.UserAccount.Default" %>

<%@ Register Src="../UserControls/AccountManagerUC.ascx" TagName="AccountManagerUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Content</a>
                        <ul class="submenu">
                            <li>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account">Account</asp:HyperLink>
                            </li>

                            <li>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Activities">Activities</asp:HyperLink>
                            </li>
                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">

            <h2>User Account</h2>

            <div class="block">
                <uc1:AccountManagerUC ID="AccountManagerUC1" runat="server" />
            </div>
        </div>
    </div>


</asp:Content>
