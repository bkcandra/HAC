<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Providers.Web.Account.Default" %>

<%@ Register Src="../UserControls/ProviderAcountSettingUC.ascx" TagName="ProviderAcountSettingUC"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MenuAccountUC.ascx" TagName="MenuAccountUC" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <style type="text/css">
        .bodySidebarLeft {
            width: 200px;
            vertical-align: top;
            text-align: left;
        }

        .bodySidebarRight {
            vertical-align: top;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="bodyTable" width="100%">
        <tr>
            <td class="bodySidebarLeft">
                <uc2:MenuAccountUC ID="MenuAccountUC1" runat="server" />
            </td>
            <td class="bodySidebarRight">
                
                <div style="margin: 0px 0px 30px 25px">
                    
                    <uc1:ProviderAcountSettingUC ID="ProviderAcountSettingUC1" runat="server" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
