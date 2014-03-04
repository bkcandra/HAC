<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HealthyClub.Provider.Web.Account.ChangePassword" %>

<%@ Register Src="~/UserControls/MenuAccountUC.ascx" TagPrefix="uc1" TagName="MenuAccountUC" %>
<%@ Register Src="~/UserControls/ProviderPassword.ascx" TagPrefix="uc1" TagName="ProviderPassword" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="vertical-align: top; width: 200px;">
                <uc1:MenuAccountUC runat="server" id="MenuAccount" />
            </td>
            <td style="vertical-align: top">
                <uc1:ProviderPassword runat="server" id="ProviderPassword" />
            </td>
        </tr>
    </table>
</asp:Content>
