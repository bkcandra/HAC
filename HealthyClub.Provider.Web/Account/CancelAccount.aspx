<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CancelAccount.aspx.cs" Inherits="HealthyClub.Provider.Web.Account.CancelAccount" %>

<%@ Register Src="../UserControls/AccountRemovalUC.ascx" TagName="AccountRemovalUC" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/MenuAccountUC.ascx" TagPrefix="uc1" TagName="MenuAccountUC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="vertical-align: top; width: 200px;">
                <uc1:MenuAccountUC runat="server" ID="MenuAccountUC" /> 
            </td>
            <td style="vertical-align: top">
                <uc1:AccountRemovalUC ID="AccountRemovalUC2" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
