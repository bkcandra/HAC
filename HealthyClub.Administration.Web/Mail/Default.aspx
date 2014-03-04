<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.Mail.Default" %>

<%@ Register Src="~/UserControls/MailTemplateListUC.ascx" TagName="MailTemplateListUC"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MailTemplateSettingUC.ascx" TagName="MailTemplateSettingUC"
    TagPrefix="uc2" %>
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
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Mail Setting</h2>

            <div class="block">
                <uc2:MailTemplateSettingUC ID="MailTemplateSettingUC1" runat="server" />
            </div>
        </div>
        <div class="box sidebox">
            <h2>Mail Template</h2>
            <div class="block">
                <uc1:MailTemplateListUC ID="MailTemplateListUC1" runat="server" />
            </div>
        </div>
    </div>

    
    <br />
    
</asp:Content>
