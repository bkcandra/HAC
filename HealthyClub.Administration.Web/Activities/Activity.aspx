<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="HealthyClub.Administration.Web.Activities.Activity" %>

<%@ Register Src="~/UserControls/ActivityRegistrationDetailUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationDetailUC" %>
<%@ Register Src="~/UserControls/ActivityRegistrationDescriptionUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationDescriptionUC" %>
<%@ Register Src="~/UserControls/ActivityRegistrationTimetableUC.ascx" TagPrefix="uc1" TagName="ActivityRegistrationTimetableUC" %>
<%@ Register Src="~/UserControls/ActivityRegistrationGroup.ascx" TagPrefix="uc1" TagName="ActivityRegistrationGroup" %>
<%@ Register Src="~/UserControls/ActivityImageUC.ascx" TagPrefix="uc1" TagName="ActivityImageUC" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/themes/redmond/jquery-ui.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .errorBox P {
            padding: 10px 0 0 10px;
        }

        .errorBox {
            background: #DA3E44;
            padding: 0 0 2px;
            border: solid #DA3E44;
            border-width: 0 1px;
            width: 920px;
            margin-bottom: 10px;
        }

            .errorBox #Title {
                color: #FFF;
                background: #DA3E44;
                font-size: 1.154em;
                padding-left: 10px;
                line-height: 26px;
                font-weight: 700;
            }

            .errorBox span {
                color: #6C2F37;
                display: block;
                padding-left: 10px;
                line-height: 20px;
                margin: 3px 0;
                background: #fff url(../img/au/error_ico.png) no-repeat 10px 0;
            }

        .smallCase {
            font-size: 11px;
            color: #669;
        }
    </style>
    <script>
        $(document).ready(function () {
            $("#EditActtabs").tabs();
        });
    </script>
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem">Action</a>
                        <ul class="submenu">
                            <li>
                                <asp:LinkButton ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="buttonCreateAct" />
                            </li>
                            <li>
                                <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="buttonCreateAct" />
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>
                <asp:Label ID="lblPageTitle" runat="server" Text="Edit Activity Information" CssClass="pageTitle"> </asp:Label></h2>
            <div class="block">
                <div>
                    <br />
                    <div id="divActStatus">
                        <fieldset style="width: 250px;">
                            <legend>Status</legend>
                            <span style="float: left; font-weight: bold; padding-top: 3px;" class="bodyText1">Approved</span>
                            <div style="float: right; width: 100px">
                                <div style="padding-top: 3px;">
                                    <span style="float: left;">
                                        <asp:RadioButton ID="RadNo" GroupName="Approve" runat="server" Text="No" Checked="true" /></span>
                                    <span style="float: right;">
                                        <asp:RadioButton ID="RadYes" GroupName="Approve" runat="server" Text="Yes" /></span>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnIsApproved" runat="server" ClientIDMode="AutoID" />
                        </fieldset>

                    </div>
                    <div style="clear: both"></div>
                    <br />
                    <div id="divError" runat="server" class="errorBox" visible="false" style="width: 800px">
                        <span id="Title">Oops! You haven't finished entering in your activity details. </span>
                        <asp:Label ID="lblError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    </div>
                    <div id="EditActtabs">
                        <ul>
                            <li><a href="#tabs-1">Activity Details</a></li>
                            <li><a href="#tabs-2">Category & Description</a></li>
                            <li><a href="#tabs-3">Timetable</a></li>
                            <li><a href="#tabs-4">Grouping</a></li>
                            <li><a href="#tabs-5">Images</a></li>
                        </ul>
                        <div id="tabs-1" class="tabs3">
                            <p>
                                <uc1:ActivityRegistrationDetailUC runat="server" ID="ActivityRegistrationDetailUC" />
                        </div>
                        <div id="tabs-2" class="tabs3">
                            <p>
                                <uc1:ActivityRegistrationDescriptionUC runat="server" ID="ActivityRegistrationDescriptionUC" />
                        </div>
                        <div id="tabs-3" class="tabs3">
                            <p>
                                <uc1:ActivityRegistrationTimetableUC runat="server" ID="ActivityRegistrationTimetableUC" />
                        </div>
                        <div id="tabs-4" class="tabs3">
                            <p>
                                <uc1:ActivityRegistrationGroup runat="server" ID="ActivityRegistrationGroup" />
                        </div>
                        <div id="tabs-5" class="tabs3">
                            <p>
                                <uc1:ActivityImageUC runat="server" ID="ActivityImageUC" />

                        </div>
                    </div>
                    <asp:HiddenField ID="hdnProviderID" runat="server" />
                    <asp:HiddenField ID="hdnReferrer" runat="server" />
                    <asp:HiddenField ID="hdnActivityID" runat="server" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
