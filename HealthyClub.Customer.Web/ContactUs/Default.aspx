<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Web.ContactUs.Default" %>

<%@ Register Src="../UserControls/ContactUsUC.ascx" TagName="ContactUsUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc1:ContactUsUC ID="ContactUsUC1" runat="server" />
</asp:Content>
