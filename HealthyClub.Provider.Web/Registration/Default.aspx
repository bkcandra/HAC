<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Providers.Web.Registration.Default" %>
<%@ Register src="../UserControls/ProviderRegistrationUC.ascx" tagname="ProviderRegistrationUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ProviderRegistrationUC ID="ProviderRegistrationUC1" runat="server" />
</asp:Content>
