<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Providers.Web.Pages.Default" %>
<%@ Register src="../UserControls/PagesUC.ascx" tagname="PagesUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:PagesUC ID="PagesUC1" runat="server" />
</asp:Content>
