<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Web.Activity.Default" %>

<%@ Register Src="../UserControls/ActivityDetail.ascx" TagName="ActivityDetail" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/jquery-1.8.2.js"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ActivityDetail ID="ActivityDetail1" runat="server" />
</asp:Content>
