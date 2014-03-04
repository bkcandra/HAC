<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CancelAccount.aspx.cs" Inherits="HealthyClub.Customer.Web.Account.CancelAccount" %>

<%@ Register Src="../UserControls/AccountRemovalUC.ascx" TagName="AccountRemovalUC" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/MenuAccount.ascx" TagPrefix="uc1" TagName="MenuAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="vertical-align: top;width: 200px;">
                <uc1:MenuAccount runat="server" ID="MenuAccount" />
            </td>
            <td style="vertical-align: top">
                <uc1:AccountRemovalUC ID="AccountRemovalUC1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
