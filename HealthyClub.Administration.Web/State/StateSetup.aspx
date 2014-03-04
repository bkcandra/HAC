<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StateSetup.aspx.cs" Inherits="HealthyClub.Administration.Web.State.StateSetup" %>

<%@ Register Src="../UserControls/StateSetupUC.ascx" TagName="StateSetupUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:StateSetupUC ID="StateSetupUC1" runat="server" />


</asp:Content>
