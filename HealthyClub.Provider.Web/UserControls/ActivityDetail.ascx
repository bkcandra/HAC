<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityDetail.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ActivityDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="AcitivityScheduleDetailUC.ascx" TagName="AcitivityScheduleDetailUC"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ScheduleViewerUC.ascx" TagName="ScheduleViewerUC"
    TagPrefix="uc3" %>
<%@ Register Src="ImagesListViewUC.ascx" TagName="ImagesListViewUC" TagPrefix="uc2" %>
<style type="text/css">
    #mapWrapper {
        padding: 5px;
        background: #e1eef5;
    }

    #map {
        height: 300px;
        width: 300px;
    }

    .style1 {
        width: 5px;
        vertical-align: top;
    }

    .style2 {
        width: 150px;
        vertical-align: top;
        color: #4679A3;
        font-weight: bold;
    }

    .wrapper {
        margin-left: 20px;
    }

    .style3 {
        width: 500px;
        padding-left: 4px;
    }

    .actDesc {
        text-align: justify;
    }

    .auto-style1 {
        width: 700px;
        height: 100%;
    }

    .auto-style2 {
        width: 324px;
        height: 100%;
    }
</style>

<div id="divMain" runat="server">
    <div id="divWithImage" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Image ID="ProviderImage" runat="server" Style="max-height: 70px;" />
                </td>
                <td style="vertical-align: bottom; padding-bottom: 10px">
                    <h1>
                        <div style="padding-left: 15px;">
                            <asp:Label ID="lblTitleWImage" runat="server" class="pageTitle" Text="[Org. Name]"></asp:Label>
                            &nbsp;-&nbsp;
    <asp:Label ID="lblProviderWImage" runat="server" class="pageTitle2" Text="[Act. Name]"></asp:Label>
                        </div>
                    </h1>
                </td>
            </tr>
        </table>
    </div>
    <div id="divNoImage" runat="server">
        <h1>
            <asp:Label ID="lblTitle" runat="server" class="pageTitle"></asp:Label>&nbsp;-&nbsp;
       <asp:Label ID="lblProvider" runat="server" class="pageTitle2"></asp:Label></h1>
    </div>
    <asp:LinkButton ID="lnkEditActivity" runat="server" OnClick="lnkEditActivity_Click"
        Style="margin: 0px;">Edit Activity</asp:LinkButton>
    <hr />

    <table>
        <tr>
            <td style="vertical-align: top" class="auto-style1">
                <table width="100%">
                    <tr>
                        <td class="style2">Where?
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblAddress" runat="server"></asp:Label><br />
                            <asp:Label ID="lblSub" runat="server"></asp:Label>
                            <asp:Label ID="lblState" runat="server"></asp:Label>
                            <asp:Label ID="lblPostCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">When?
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <div id="divSchedule" runat="server" style="position: relative; left: -3px">
                                <uc3:ScheduleViewerUC ID="ScheduleViewerUC1" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr id="trSuitability" runat="server">
                        <td class="style2">Activity suitability</td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <div id="divEligivibility" runat="server" style="word-break: break-all;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Cost?
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <div id="divPriceDescription" runat="server" style="word-break: break-all;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Contact Name
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblContactName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Phone Number
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblContact" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Email Address
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblEmailAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Website
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblWebsite" runat="server" Visible="false"></asp:Label>
                            <asp:HyperLink ID="hlnkWebsite" runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Keywords
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblKeyword" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Updated on
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <asp:Label ID="lblUpdate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">Description
                        </td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">
                            <div id="divProductDesc" runat="server" class="actDesc">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">&nbsp;</td>
                        <td class="style1">&nbsp;
                        </td>
                        <td class="style3">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td rowspan="1" style="vertical-align: top;" class="auto-style2">
                <div class="wrapper" style="height: 800px">
                    <div id="gallery">
                        <uc2:ImagesListViewUC ID="ImagesListViewUC2" runat="server" />
                    </div>
                    <div id="gallery1">
                        <asp:Image ID="ImageProvider" runat="server" Style="max-height: 300px; max-width: 300px" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>

<asp:HiddenField ID="hdnActivityID" runat="server" />
<asp:HiddenField ID="hdnAddress" runat="server" />

<div id="divUnauth" runat="server">
</div>
