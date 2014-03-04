<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Providers.Web.Activity.Default" %>

<%@ Register Src="../UserControls/ActivityDetail.ascx" TagName="ActivityDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:ActivityDetail ID="ActivityDetail1" runat="server" />

    <p>
        <div class="backArticles" style="width: 95%">
            <a href="javascript: history.go(-1)">Back </a>
        </div>
    </p>
</asp:Content>
