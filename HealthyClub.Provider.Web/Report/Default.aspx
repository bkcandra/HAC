<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" ValidateRequest="false" Inherits="HealthyClub.Providers.Web.Report.Default" %>
<%@ Register src="../UserControls/ReportUC.ascx" tagname="ReportUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ReportUC ID="ReportUC1" runat="server" />
</asp:Content>
