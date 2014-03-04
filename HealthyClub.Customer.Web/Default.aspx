<%@ Page Title="Healthy Australia Club" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Customer.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
        ga('create', 'UA-43779221-2', 'healthyaustraliaclub.com.au');
        ga('send', 'pageview');
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <p>
        &nbsp;
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/Banner/Themes/default/default.css" rel="stylesheet" />
    <div id="banner_wrapper_outter">
        <div id="banner_wrapper">
            <div id="banner">
                <div id="banner_text">
                    <div id="banner_text_title">
                        <h1>Welcome to the
                            <br />
                            Healthy Australia Club!
                        </h1>
                    </div>
                    <p>
                        Use our <span style="font-style: italic">Find an Activity</span> page to search for local activities that are healthy, fun and make you feel good.  
                    </p>
                    <a href="Pages/16" class="btn-learnmore" style="color: white">Learn More</a>
                </div>
                <div id="banner_slider">
                    <!-- start of the slider -->
                    <div class="slider_wrapper theme-default">
                        <div class="img_frame">
                            <div id="slider" class="nivoSlider">
                                <img src="Content/Banner/Images/banner1.jpg" data-thumb="Content/BannerImages/banner1.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
                                <img src="Content/Banner/Images/banner2.jpg" data-thumb="Content/BannerImages/banner2.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
                                <img src="Content/Banner/Images/banner3.jpg" data-thumb="Content/BannerImages/banner3.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
                                <img src="Content/Banner/Images/banner4.jpg" data-thumb="Content/BannerImages/banner4.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
                                <img src="Content/Banner/Images/banner5.jpg" data-thumb="Content/BannerImages/banner5.jpg" alt="" data-transition="slideInLeft" style="width: 535px; height: 248px" />
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
                <div class="cleaner"></div>
            </div>
        </div>
    </div>
    <div class="page">
        <div id="quote_Section">
            <p>
                "We all want good health but sometimes things get in the way&quot;
            </p>
            <span>Currently we're a pilot scheme in Boroondara and are aiming to go national in the future.&nbsp; 
               The Club's philosophy is that good health can be achieved by participating in activities that look after our physical, 
               mental and social health and well-being.&nbsp; We list healthy activities on our website by building relationships with 
               organisations in the local community.  </span>
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
