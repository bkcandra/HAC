<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="HealthyClub.Administration.Web.UserAccount.UserList" %>
<%@ Register src="../UserControls/UserListUC.ascx" tagname="UserListUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:UserListUC ID="UserListUC1" runat="server" />
</asp:Content>
