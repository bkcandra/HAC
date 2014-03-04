<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HealthyClub.Provider.Web.Account.Register" %>

<%@ Register Src="../UserControls/ProviderRegistrationUC.ascx" TagName="ProviderRegistrationUC" TagPrefix="uc1" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<uc1:ProviderRegistrationUC ID="ProviderRegistrationUC1" runat="server" />

</asp:Content>
