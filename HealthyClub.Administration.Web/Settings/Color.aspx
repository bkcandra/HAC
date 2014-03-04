<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Color.aspx.cs" Inherits="HealthyClub.Administration.Web.Settings.Default" %>
<%@ Register src="../UserControls/ColorSettingUC.ascx" tagname="ColorSettingUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ColorSettingUC ID="ColorSettingUC1" runat="server" />
</asp:Content>
