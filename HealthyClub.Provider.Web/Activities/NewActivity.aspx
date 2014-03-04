<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewActivity.aspx.cs" Inherits="HealthyClub.Provider.Web.Activities.NewActivity" %>

<%@ Register Src="~/UserControls/ActivityRegistrationDetailUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationDetailUC" %>
<%@ Register Src="~/UserControls/ActivityRegistrationDescriptionUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationDescriptionUC" %>
<%@ Register Src="~/UserControls/ActivityRegistrationTimetableUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationTimetableUC" %>
<%@ Register Src="~/UserControls/ActivityRegistrationGroup.ascx" TagPrefix="uc1" TagName="ActivityRegistrationGroup" %>
<%@ Register Src="~/UserControls/ActivityRegistrationImageUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationImageUC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="../Content/SmartWizard/smart_wizard.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/smartWizard/jquery-1.4.2.min.js"></script>
    <script src="../Scripts/smartWizard/jquery.smartWizard-2.0.min.js"></script>
    <script>
        $("div.swMain").hide;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div id="divError" runat="server" class="errorBox" visible="false">
        <span id="Title">Oops! You haven't finished entering in your activity details. </span>
        <span>
            <asp:Label ID="lblError" runat="server" Text="" CssClass="error"></asp:Label></span>
    </div>

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
        <div id="step-1">
            <h2 class="StepTitle">Activity Details</h2>

            <uc1:ActivityRegistrationDetailUC runat="server" ID="ActivityRegistrationDetailUC" />
            <br />
            <uc1:ActivityRegistrationImageUC runat="server" ID="ActivityRegistrationImageUC" />
        </div>
        <div id="step-2">
            <h2 class="StepTitle">Activity Description</h2>
            <uc1:ActivityRegistrationDescriptionUC runat="server" ID="ActivityRegistrationDescriptionUC" />
        </div>
        <div id="step-3">
            <h2 class="StepTitle">Activity Timetable</h2>

            <uc1:ActivityRegistrationTimetableUC runat="server" ID="ActivityRegistrationTimetableUC" />
        </div>
        <div id="step-4">
            <h2 class="StepTitle">Activity Grouping</h2>
            <uc1:ActivityRegistrationGroup runat="server" ID="ActivityRegistrationGroup" />
        </div>
    </div>
    <!-- End SmartWizard Content -->

    <asp:Button ID="btnSubmit" runat="server" Text="asd" Style="display: none" OnClick="btnSubmit_Click"
        AutoPostBack="true" />
    <asp:HiddenField ID="hdnActionCode" runat="server" />
    <asp:HiddenField ID="hdnProviderID" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            // Smart Wizard    	
            $("div.swMain").show;
            $('#wizard').smartWizard({
                transitionEffect: 'slideleft',
                onLeaveStep: leaveAStepCallback,
                onFinish: onFinishCallback,
                enableAllSteps: true,
                enableFinishButton: true,
                keyNavigation: false
            });

            function leaveAStepCallback(obj, context) {
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                return true; // return false to stay on step and true to continue navigation 
            }

            function onFinishCallback() {
                var btnsubmit = document.getElementById('<%= btnSubmit.ClientID %>');
                if (btnsubmit != null) {
                    btnsubmit.click();
                }
            }
        });
    </script>





</asp:Content>
