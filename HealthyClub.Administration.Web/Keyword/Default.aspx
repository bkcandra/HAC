<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.Keyword.Default" %>

<%@ Register Src="../UserControls/KeywordManagementUC.ascx" TagName="KeywordManagementUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Keywords</a>
                        <ul class="submenu">
                            <li>
                                <asp:LinkButton ID="lnkNewKeyword" runat="server" OnClick="lnkNewKeyword_Click">New keyword</asp:LinkButton>
                            </li>
                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Keywords</h2>
            Manage activities's synonim and Thesaurus term
            <div class="block">
                <uc1:KeywordManagementUC ID="KeywordManagementUC1" runat="server" />
            </div>
        </div>
    </div>


</asp:Content>
