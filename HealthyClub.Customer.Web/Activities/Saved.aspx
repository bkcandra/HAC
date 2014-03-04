<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Saved.aspx.cs" Inherits="HealthyClub.Customer.Web.Activities.Saved" %>
<%@ Register src="../UserControls/SavedListUC.ascx" tagname="SavedListUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:SavedListUC ID="SavedListUC1" runat="server" />
</asp:Content>
