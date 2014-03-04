<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="HealthyClub.Provider.Web.Activities.New" %>

<%@ Register Src="~/UserControls/ActivityRegistrationTimetableUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationTimetableUC" %>
<%@ Register Src="~/UserControls/ActivityRegistrationDescriptionUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationDescriptionUC" %>
<%@ Register Src="~/UserControls/ActivityRegistrationDetailUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationDetailUC" %>
<%@ Register Src="~/UserControls/ActivityRegistrationGroup.ascx" TagPrefix="uc1" TagName="ActivityRegistrationGroup" %>
<%@ Register Src="~/UserControls/ActivityRegistrationImageUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationImageUC" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="../Scripts/smartWizard/jquery-1.4.2.min.js"></script>
    <script src="../Scripts/smartWizard/jquery.smartWizard-2.0.min.js"></script>
    <link href="../Content/SmartWizard/smart_wizard.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            // Smart Wizard 	
            $('#wizard').smartWizard();

            function onFinishCallback() {
                $('#wizard').smartWizard('showMessage', 'Finish Clicked');
                //alert('Finish Clicked');
            }
        });
    </script>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="body_wrapper">
            <div id="wrapper">
                <div id="header">
                    <div class="header_logo">
                        <asp:HyperLink ID="HyperLink1" NavigateUrl="~" runat="server">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/StyleImages/hbc_logo.png" Style="position: absolute; height: 100px;" />
                        </asp:HyperLink>
                    </div>
                    <div id="header_left">
                        <label class="MasterTitle">
                            Healthy Australia Club:</label>
                        <label class="MasterTitle2">
                            &nbsp;Healthy Boroondara Club
                        </label>
                    </div>
                    <div id="header_right">
                        <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                            <AnonymousTemplate>
                                &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Account/login.aspx">Log in</asp:HyperLink>&nbsp;|&nbsp;<asp:HyperLink
                                    ID="HyperLink3" runat="server" NavigateUrl="~/Registration/">Join now</asp:HyperLink>
                            </AnonymousTemplate>
                            <LoggedInTemplate>
                                <span class="bold">Hi</span>, <a href="~/Account/" id="A2" runat="server">
                                    <asp:LoginName ID="HeadLoginName" runat="server" />
                                </a></span>[
                            <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out"
                                LogoutPageUrl="~/" />
                                ]
                            </LoggedInTemplate>
                        </asp:LoginView>
                    </div>
                    <div class="cleaner">
                    </div>
                </div>
                <!-- END of header -->
                <div id="menubar">
                    <div id="top_nav" class="ddsmoothmenu">
                        <asp:Menu ID="MenuNavigation" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
                            StaticSubMenuIndent="16px">
                            <DynamicItemTemplate>
                                <%# Eval("Text") %>
                            </DynamicItemTemplate>
                            <Items>
                                <asp:MenuItem NavigateUrl="~" Text="Home" Value="Home"></asp:MenuItem>
                                <asp:MenuItem NavigateUrl="~/Activities/Default.aspx" Text="My Activities" Value="My Activities"></asp:MenuItem>
                            </Items>
                            <StaticMenuStyle />

                        </asp:Menu>

                        <br style="clear: left" />
                    </div>
                    <!-- end of ddsmoothmenu -->

                </div>
                <!-- END of menubar -->
                <div id="main">
                    <div id="content">
                        <div>
                            <div id="divError" runat="server" class="errorBox" visible="false">
                                <span id="Title">Oops! You haven't finished entering in your activity details. </span>
                                <span>
                                    <asp:Label ID="lblError" runat="server" Text="" CssClass="error"></asp:Label></span>
                            </div>
                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <!-- Smart Wizard -->
                                        <div id="wizard" class="swMain">
                                            <ul>
                                                <li><a href="#step-1"><span class="stepDesc">Step 1<br />
                                                    <small>Activity Details</small> </span></a></li>
                                                <li><a href="#step-2"><span class="stepDesc">Step 2<br />
                                                    <small>Activity Description</small> </span></a></li>
                                                <li><a href="#step-3"><span class="stepDesc">Step 3<br />
                                                    <small>Activity Timetable</small> </span></a></li>
                                                <li><a href="#step-4"><span class="stepDesc">Step 4<br />
                                                    <small>Activity Grouping</small> </span></a></li>
                                            </ul>
                                            <div id="step-1"><uc1:ActivityRegistrationDetailUC runat="server" ID="ActivityRegistrationDetailUC" /></div>
                                            <div id="step-2"></div>
                                            <div id="step-3"></div>
                                            <div id="step-4"></div>
                                        </div>
                                        <!-- End SmartWizard Content-->
                                        
                                        <asp:HiddenField ID="hdnActionCode" runat="server" />
                                        <asp:HiddenField ID="hdnProviderID" runat="server" />
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                    <div class="cleaner">
                    </div>
                </div>
                <!-- END of main -->
                <div id="footer">
                    <div id="master_footer">
                        <ul class="footer_menu">
                            <li><a href="../Pages/20">Privacy</a></li>
                            <li class="last_menu"><a href="../Pages/21" target="_parent">Terms
                        and Conditions</a></li>
                        </ul>
                        Site maintained by <a href="http://www.iechs.com.au">Inner East Community Health</a>
                        © 2013&nbsp;&nbsp;
                    </div>
                </div>
                <!-- END of footer -->
            </div>
        </div>
    </form>
</body>
</html>
