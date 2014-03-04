<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SuburbSetup.aspx.cs" Inherits="HealthyClub.Administration.Web.Suburb.SuburbSetup" %>

<%@ Register Src="../UserControls/SuburbSetupUC.ascx" TagName="SuburbSetupUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <uc1:SuburbSetupUC ID="SuburbSetupUC1" runat="server" />

</asp:Content>
