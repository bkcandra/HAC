<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HealthyClub.Administration.Web.Account.Register" %>
<%@ Register src="../UserControls/AdminRegistrationUC.ascx" tagname="AdminRegistrationUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:AdminRegistrationUC ID="AdminRegistrationUC1" runat="server" />
</asp:Content>
