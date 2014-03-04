<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategorySetup.aspx.cs" Inherits="HealthyClub.Administration.Web.Category.CategoriesSetup" %><%@ Register src="../UserControls/CategoriesSetupUC.ascx" tagname="CategoriesSetupUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:CategoriesSetupUC ID="CategoriesSetupUC1" runat="server" />
</asp:Content>
