<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailTemplate.aspx.cs" Inherits="HealthyClub.Administration.Web.Mail.MeailTemplate" %>

<%@ Register Src="../UserControls/MailTemplateDetailUC.ascx" TagName="MailTemplateDetailUC" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Mail Template</a>
                        <ul class="submenu">
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>


    <uc1:MailTemplateDetailUC ID="MailTemplateDetailUC1" runat="server" />

</asp:Content>
