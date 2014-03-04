<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Web.Account.Default" %>

<%@ Register Src="../UserControls/CustomerAccountUC.ascx" TagName="CustomerAccountUC"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/CustomerPasswordUC.ascx" TagName="CustomerPasswordUC"
    TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/MenuAccount.ascx" TagPrefix="uc1" TagName="MenuAccount" %>
<%@ Register Src="~/UserControls/Assess.ascx" TagPrefix="uc1" TagName="Assess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/themes/redmond/jquery-ui.css" rel="stylesheet" />
    <table style="width: 100%">
        <tr>
            <td style="width: 200px; text-align: left; vertical-align: top;">
                <uc1:MenuAccount runat="server" ID="MenuAccount" />
            </td>
            <td style="vertical-align: top">
                <div id="tabs">
                    <ul>
                        <li><a href="#tabs-1">My Account</a></li>

                        <li><a href="#tabs-3">Rewards</a></li>
                    </ul>


                    <div id="tabs-1" class="tabs3">
                        <p>
                            <uc1:CustomerAccountUC ID="CustomerAccountUC1" runat="server" Visible="true" />
                    </div>
                    <div id="tabs-3" class="tabs3">
                        <p>
                            <uc1:Assess runat="server" ID="Assess" />
                        </p>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <script>
        $(document).ready(function () {
            $("#tabs").tabs();
        });
    </script>
</asp:Content>
