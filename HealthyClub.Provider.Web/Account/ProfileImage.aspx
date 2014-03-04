<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileImage.aspx.cs" Inherits="HealthyClub.Provider.Web.Account.ProfileImage" %>

<%@ Register Src="~/UserControls/MenuAccountUC.ascx" TagPrefix="uc1" TagName="MenuAccountUC" %>
<%@ Register Src="~/UserControls/ProfileImageUC.ascx" TagPrefix="uc1" TagName="ProfileImageUC" %>


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
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <table id="bodyTable" width="100%">
        <tr>
            <td class="bodySidebarLeft">
                <uc1:MenuAccountUC runat="server" ID="MenuAccountUC" />                
            </td>
            <td class="bodySidebarRight">
                

                    <uc1:ProfileImageUC runat="server" ID="ProfileImageUC" />
             
            </td>
        </tr>
    </table>
</asp:Content>
