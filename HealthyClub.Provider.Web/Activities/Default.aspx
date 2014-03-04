<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Providers.Web.Activities.Default" %>

<%@ Register Src="../UserControls/ActivityManagementUC.ascx" TagName="ActivityManagementUC"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ActivityManagementSidebarUC.ascx" TagName="ActivityManagementSidebarUC"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:ActivityManagementUC ID="ActivityManagementUC1" runat="server" />
</asp:Content>
