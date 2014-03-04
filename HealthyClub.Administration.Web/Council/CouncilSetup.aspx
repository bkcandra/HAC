<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CouncilSetup.aspx.cs" Inherits="HealthyClub.Administration.Web.Council.CouncilSetup" %>

<%@ Register Src="~/UserControls/CouncilSetupUC.ascx" TagPrefix="uc1" TagName="CouncilSetupUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:CouncilSetupUC runat="server" id="CouncilSetupUC" />
</asp:Content>
