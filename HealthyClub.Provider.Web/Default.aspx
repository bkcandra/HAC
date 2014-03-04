<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Provider.Web._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <link href="Content/Banner/Themes/default/default.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.js"></script>
    <section class="featured">
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="banner_wrapper_outter">
        <div id="banner_wrapper">
            <div id="banner">
                <div id="banner_slider">
                    <!-- start of the slider -->
                    <div class="slider_wrapper theme-default">
                        <div class="img_frame">
                            <div id="slider" class="nivoSlider">
                                <img src="Content/Banner/Images/banner6.jpg" data-thumb="Content/BannerImages/banner6.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
                                <img src="Content/Banner/Images/banner7.jpg" data-thumb="Content/BannerImages/banner7.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
                                <img src="Content/Banner/Images/banner8.jpg" data-thumb="Content/BannerImages/banner8.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
                                <img src="Content/Banner/Images/banner9.jpg" data-thumb="Content/BannerImages/banner9.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
                                <img src="Content/Banner/Images/banner10.jpg" data-thumb="Content/BannerImages/banner10.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
                            </div>
                            <!--   title="#htmlcaption"<div id="htmlcaption" class="nivo-html-caption">
                                <strong>Visit</strong> our <a href="Content/BannerImages/banner5.jpg">Website</a>.
                            </div>-->
                        </div>
                    </div>
                    <!-- End of homeslider -->
                    <div class="cleaner">
                    </div>
                </div>
                <div id="banner_text">
                    <h1>
                        <div id="banner_text_title">
                            Welcome to the<br />

                            Healthy Australia Club!
                          
                        </div>
                    </h1>
                    <p>This page is for organisations who are interested in being part of the Club or are already registered as an Activity Provider.</p>
                    <a href="../Pages/16" class="btn-learnmore" style="color: white">Learn More</a>
                </div>
                <div class="cleaner"></div>
            </div>
        </div>
    </div>

    <div class="page">
        <div id="quote_Section">
            <p>
                "We all want good health but sometimes things get in the way&quot;
            </p>
            <span>Currently we&#39;re a pilot scheme in Boroondara and are aiming to go national in the future. The Club&#39;s philosophy is that good health can be achieved by participating in activities that look after our physical, mental and social health and well-being.<br />
                We want organisations who run healthy activities in Boroondara to list them with with on our website.&nbsp; It is free to be part of the Club and you can promote your activity, increase your member enrolments and produced customised reports and timetables.
            </span>
        </div>
        <div class="clear">
        </div>
    </div>
    <script src="Scripts/jquery.nivo.slider.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $('#slider').nivoSlider();
            effect: 'slideInLeft'

        });
    </script>
</asp:Content>
