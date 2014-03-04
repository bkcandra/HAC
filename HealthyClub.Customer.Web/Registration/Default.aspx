<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Web.Registration.Default" %>

<%@ Register Src="../UserControls/CustomerRegistrationUC.ascx" TagName="CustomerRegistrationUC"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:CustomerRegistrationUC ID="CustomerRegistrationUC1" runat="server" />
</asp:Content>
