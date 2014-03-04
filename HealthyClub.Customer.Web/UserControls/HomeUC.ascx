<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeUC.ascx.cs" Inherits="HealthyClub.Web.UserControls.HomeUC" %>
<link href="../Themes/light/light.css" rel="stylesheet" type="text/css" />
<link href="../Themes/default/default.css" rel="stylesheet" type="text/css" />
<link href="../Themes/dark/dark.css" rel="stylesheet" type="text/css" />
<link href="../Themes/bar/light.css" rel="stylesheet" type="text/css" />
<link href="../Styles/nivo-slider.css" rel="stylesheet" type="text/css" />
<!-- start of Customer_banner -->
<div id="templatemo_banner_wrapper_outter">
    <div id="templatemo_banner_wrapper">
        <div id="templatemo_banner">
            <div id="templatemo_banner_slider">
                <div class="slider-wrapper theme-default">
                    <div class="img_frame">
                        <div id="slider" class="nivoSlider">
                            <img src="../Images/toystory.jpg" data-thumb="../Images/toystory.jpg" alt="" />
                            <a href="http://www.iechs.com.au">
                                <img src="../Images/up.jpg" data-thumb="../Images/up.jpg" alt="" title="Healthy Acitivities for everyone" /></a>
                            <img src="../Images/walle.jpg" data-thumb="../Images/walle.jpg" alt="" data-transition="slideInLeft" />
                            <img src="../Images/nemo.jpg" data-thumb="../Images/nemo.jpg" alt="" title="#htmlcaption" />
                        </div>
                        <div id="htmlcaption" class="nivo-html-caption">
                            <a href="http://www.iechs.com.au/">Click here</a>to <strong>Visit</strong> our Website.
                        </div>
                    </div>
                </div>
                <!-- end of the slider -->
            </div>
            <!-- end of templatemo_popular_posts -->
        </div>
    </div>
</div>
<!-- end of Customer_banner -->
<div class="content_wrapper">
    <table style="width: 90%; height: 100px; vertical-align: bottom; text-align: center">
        <tr style="padding: 20px 0px 20px 0px">
            <td style="background-color: Gray">
                <label style="vertical-align: bottom; text-align: center">
                    Widget</label>
            </td>
            <td style="background-color: Gray">
                <div>
                    <label style="vertical-align: bottom; text-align: center">
                        Widget</label></div>
            </td>
            <td style="background-color: Gray">
                <div>
                    <label style="vertical-align: bottom; text-align: center">
                        Widget</label></div>
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 90%; height: 100px; vertical-align: bottom; text-align: center;">
        <tr style="margin: 20px 0px 20px 0px;">
            <td style="background-color: Gray">
                <div>
                    <label style="vertical-align: bottom; text-align: center">
                        Widget</label></div>
            </td>
            <td style="background-color: Gray">
                <div>
                    <label style="vertical-align: bottom; text-align: center">
                        Widget</label></div>
            </td>
            <td style="background-color: Gray">
                <div>
                    <label style="vertical-align: bottom; text-align: center">
                        Widget</label></div>
            </td>
        </tr>
    </table>
</div>
<script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.nivo.slider.js" type="text/javascript"></script>
<script type="text/javascript">
    $(window).load(function () {
        $('#slider').nivoSlider();
    });
</script>
