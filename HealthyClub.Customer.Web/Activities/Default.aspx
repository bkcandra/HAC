<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Customer.Web.Activities.Default" %>

<%@ Register Src="../UserControls/ActivityListing.ascx" TagName="ActivityListing"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:ActivityListing ID="ActivityListing1" runat="server" />
</asp:Content>
