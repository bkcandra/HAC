<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Performance.aspx.cs" Inherits="HealthyClub.Customer.Web.Rewards.Performance" %>

<%@ Register Src="../Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <div id="billboard-weekly-wrapper">
        <div id="slider-wrap">
            <div class="royalSlider rsDefault">
                <img class="rsImg" src="../Content/StyleImages/header.jpg" alt="Buy One Get One 50 Percent Off Mytrition">
                <img class="rsImg" src="../Content/StyleImages/header1.jpg" alt="Buy One Get One Free Vitamin D3">
                <img class="rsImg" src="../Content/StyleImages/header2.jpg" alt="DoublePointsJointSupport">
                <img class="rsImg" src="../Content/StyleImages/header3.jpg" alt="DoublePointsJointSupport">
                <img class="rsImg" src="../Content/StyleImages/header4.jpg" alt="DoublePointsJointSupport">
            </div>
            <div id="carousel-heading-wrapper" class="bottom medium">
                <div id="carousel-heading-position">
                    <h4>Rewards Program</h4>
                </div>
            </div>
        </div>
        <!-- /slider-wrap -->
    </div>
    <script src="../Scripts/jquery.royalslider.min.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".royalSlider").royalSlider({ keyboardNavEnabled: true, autoScaleSlider: false, controlNavigation: "bullets", arrowsNav: true, arrowsNavAutoHide: true, imgWidth: 760, imgHeight: 310, loop: true, loopRewind: false, randomizeSlides: false, numImagesToPreload: 4, usePreloader: true, transitionType: "fade", transitionSpeed: 500, imageScalePadding: 0, slidesSpacing: 0, minSlideOffset: 0, navigateByClick: false, autoPlay: { enabled: true, pauseOnHover: true, delay: 5000, stopAtAction: false } });
        });
    </script>
    <div id="RewardsMenu">
        <div id="RewardsMenuNav">
            <ul>
                <li id="HouseMenuNav2Link1"><a href="RewardsHome" title="Rewards Home" tabindex="1">Rewards Home</a></li>
                <li id="HouseMenuNav2Link2"><a href="How It Works" title="How it works" tabindex="1">How it works</a></li>
                <li id="HouseMenuNav2Link3" class="CurrentItem"><a href="Performance" title="Your Activities" tabindex="1">Your Activities</a></li>
                <li id="HouseMenuNav2Link4"><a href="Rewardshop" title="Rewards Shop" tabindex="1">Rewards Shop</a></li>
                <li id="HouseMenuNav2Link5"><a href="Redeem" title="Redeem Points" tabindex="1">Redeem Points</a></li>
                <li id="HouseMenuNav2Link6"><a href="Rewards Terms and Conditions" title="Terms and Conditions" tabindex="1">Terms and Conditions</a></li>
            </ul>
        </div>
    </div>
    <div id="sidetext">
        <h3><strong>Please Sign in to be a Part of our Rewards Program</strong></h3>
        <asp:Login ID="Login1" runat="server" ViewStateMode="Disabled" RenderOuterTable="false" OnAuthenticate="Login1_Authenticate" OnLoggedIn="Login1_LoggedIn">
            <LayoutTemplate>
                <asp:Panel ID="pnlDefaultButton" runat="server" DefaultButton="LoginButton" Width="434px">
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <br />
                    <asp:Label ID="linkCOnfirmError" runat="server" class="failureNotification" Visible="false">You have not confirmed your account. Click <a href="Confirm.aspx">here</a> to resend confirmation email.</asp:Label>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                        ValidationGroup="LoginUserValidationGroup" />
                    <div class="accountInfo">
                        <fieldset class="login">
                            <legend><a style="color: #102463;">Login Information</a></legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username</asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                    ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:CheckBox ID="RememberMe" runat="server" />
                                <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                            </p>
                        </fieldset>
                    </div>
                    <div class="submitButton">
                        <div style="float: left">
                            <p>
                                <asp:HyperLink ID="hlnkforgot" runat="server" NavigateUrl="~/Account/ForgotPassword.aspx"
                                    Text="Forgot Your Password?" /><br />
                                Don't have an account?
                               <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Join now</asp:HyperLink>
                                &nbsp;
                            </p>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" CssClass="button button-login"
                                ValidationGroup="LoginUserValidationGroup" />
                        </div>
                    </div>
                </asp:Panel>
            </LayoutTemplate>
        </asp:Login>



    </div>

</asp:Content>

