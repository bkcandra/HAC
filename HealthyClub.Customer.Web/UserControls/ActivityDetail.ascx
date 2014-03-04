<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityDetail.ascx.cs"
    Inherits="HealthyClub.Web.UserControls.ActivityDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="ScheduleViewerUC.ascx" TagName="ScheduleViewerUC" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/GMapUC.ascx" TagName="GMapUC" TagPrefix="uc2" %>
<%@ Register Src="ImagesListViewUC.ascx" TagName="ImagesListViewUC" TagPrefix="uc3" %>
<%@ Register Src="ActivityNavigationUC.ascx" TagName="ActivityNavigationUC" TagPrefix="uc4" %>
<link href="../../Styles/tabStyle.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>

<script>

    var map;
    var geocoder;
    function InitializeMap() {

        var latlng = new google.maps.LatLng(-34.397, 150.644);
        var myOptions =
        {
            zoom: 15,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            disableDefaultUI: true
        };
        map = new google.maps.Map(document.getElementById("map"), myOptions);

    }

    function FindLocation(lat, lng, message) {
        geocoder = new google.maps.Geocoder();
        InitializeMap();
        var latlng = new google.maps.LatLng(lat, lng);
        map.setCenter(latlng);
        var marker = new google.maps.Marker({
            map: map,
            position: latlng
        });

        var infowindow = new google.maps.InfoWindow({
            content: message
        });

        google.maps.event.addListener(marker, 'click', function () {
            // Calling the open method of the infoWindow 
            infowindow.open(map, marker);
        });

    }
</script>

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
</style>

<div id="divNavigation">
    <uc4:ActivityNavigationUC ID="ActivityNavigationUC1" runat="server" />
</div>
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
<hr />


<table>
    <tr>
        <td style="width: 700px; vertical-align: top">
            <table style="width: 100%">
                <tr id="trAddress" runat="server">
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
                <tr id="trWhen" runat="server">
                    <td class="style2">When?
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td>
                        <div id="divSchedule" runat="server">
                            <uc1:ScheduleViewerUC ID="ScheduleViewerUC1" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr id="trSuitability" runat="server">
                    <td class="style2">Suitability</td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style3">
                        <div id="divEligivibility" runat="server">
                        </div>
                    </td>
                </tr>
                <tr id="trCost" runat="server">
                    <td class="style2">Cost?
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style3">
                        <div id="divPriceDescription" runat="server">
                        </div>
                    </td>
                </tr>
                <tr id="trCP" runat="server">
                    <td class="style2">Contact
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblContactName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trPhone" runat="server">
                    <td class="style2">Phone Number
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblContact" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trEmail" runat="server">
                    <td class="style2">Email Address
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblEmailAddress" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trWebsite" runat="server">
                    <td class="style2">Website
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style3">
                        <asp:HyperLink ID="hlnkWebsite" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="style2">Last updated
                    </td>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style3">
                        <asp:Label ID="lblUpdate" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr id="trDescription" runat="server">
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
        <td rowspan="1" style="width: 324px; vertical-align: top; height: 100%">
            <div class="wrapper">
                <div id="gallery" runat="server">
                    <uc3:ImagesListViewUC ID="ImagesListViewUC1" runat="server" />
                    <br />
                </div>

                <div id="mapWrapper">
                    <div id="map">
                    </div>
                </div>
            </div>
        </td>
    </tr>
</table>

<asp:HiddenField ID="hdnActivityID" runat="server" />
<asp:HiddenField ID="hdnAddress" runat="server" />
