<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityListingSidebar.ascx.cs"
    Inherits="HealthyClub.Web.UserControls.ActivityListSidebar" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<style>
    .multiSelectTextbox {
        background-color: #fff;
        color: #1B274F;
        font-weight: bold;
        padding: 3px;
        border: 1px solid #b7b7b7;
        width: 150px;
    }

    .multiSelectButton {
        display: block;
        background-image: url(../Content/StyleImages/multiselectarrow2.png);
        background-position: bottom;
        background-repeat: no-repeat;
        height: 24px;
        width: 21px;
    }

    .multiSelectTooltip {
        position: absolute;
        display: block;
        padding: 2px 12px 3px 7px;
        margin-left: 5px;
        background: #fff;
        color: #1B274F;
        border: 1px solid #b7b7b7;
        font-weight: bold;
    }

    .multiSelectDropdown {
        border: 1px solid #b7b7b7;
        min-width: 150px;
    }

        .multiSelectDropdown td {
            border: 1px dotted #bbbbbb;
            background-color: #fff;
            padding-left: 2px;
        }

    .dropdown {
        width: 280px;
        display: inline-block;
        margin-right: 2px;
    }

    .textbox {
        width: 200px;
        display: inline-block;
        margin-right: 5px;
    }

    .dropdown .textbox {
        vertical-align: bottom;
        padding: 2px;
        border: 1px solid #b7b7b7;
    }
    /*AJAX Calendar
    */
    .cal_Theme1 .ajax__calendar_container {
        background-color: #e2e2e2;
        border: solid 1px #cccccc;
    }

    .cal_Theme1 .ajax__calendar_header {
        background-color: #ffffff;
        margin-bottom: 4px;
    }

    .cal_Theme1 .ajax__calendar_title,
    .cal_Theme1 .ajax__calendar_next,
    .cal_Theme1 .ajax__calendar_prev {
        color: #004080;
        padding-top: 3px;
    }

    .cal_Theme1 .ajax__calendar_body {
        background-color: #e9e9e9;
        border: solid 1px #cccccc;
        z-index: 1003;
    }

    .cal_Theme1 .ajax__calendar_dayname {
        text-align: center;
        font-weight: bold;
        margin-bottom: 4px;
        margin-top: 2px;
    }

    .cal_Theme1 .ajax__calendar_day {
        text-align: center;
    }

    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
    .cal_Theme1 .ajax__calendar_active {
        color: #004080;
        font-weight: bold;
        background-color: #ffffff;
    }

    .cal_Theme1 .ajax__calendar_container {
        z-index: 1000;
    }

    .cal_Theme1 .ajax__calendar_today {
        font-weight: bold;
    }

    .cal_Theme1 .ajax__calendar_other,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
        color: #bbbbbb;
    }

    .loginHighlight {
        border: 1px solid black;
        background-image: url("gr.jpg" );
        color: #B24F04;
        font-family: serif;
        width: 80%;
    }

    .loginNormal {
        border: 2px solid #FF8326;
        background-image: url("gr.jpg" );
        width: 80%;
        color: #B24F04;
        font-family: serif;
        opacity: 0.6;
        filter: alpha(opacity=60);
    }

    .loginText {
        color: #FF8326;
    }

    .style2 {
        width: 55px;
    }
</style>
<div style="height: auto">
    <div class="sidebar_box">
        <div class="sb_title">
            Categories
        </div>
        <div class="sb_content">
            <div style="max-width: 210px; overflow: hidden">
                <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                    NodeIndent="5" Width="215px" Style="overflow: auto;">
                    <NodeStyle CssClass="treeNode" />
                    <LeafNodeStyle CssClass="leafNode" />
                    <RootNodeStyle CssClass="rootNode" />
                    <SelectedNodeStyle CssClass="sideBarNodeSelected"></SelectedNodeStyle>
                </asp:TreeView>
            </div>
        </div>
        <div class="sb_bottom">
        </div>
    </div>
    <div class="sidebar_box">
        <div class="sb_title">
            <div style="float: left">
                Location
            </div>
            <div style="float: right">
                <asp:ImageButton ID="imgbtnLocation" runat="server" Style="max-height: 14px; max-width: 14px;" ImageUrl="~/Content/StyleImages/ResFilter.png" OnClick="imgbtnResetSuburb_Click" />
            </div>
            <div style="clear: both"></div>
        </div>
        <div class="sb_content">
            <table style="padding-top: 10px">
                <tr>
                    <td>
                        <cc1:DropDownCheckBoxes ID="DropDownCheckBoxes1" runat="server" Width="100">
                            <Style SelectBoxWidth="160" DropDownBoxBoxWidth="180" />
                            <Texts SelectBoxCaption="Suburb" />

                        </cc1:DropDownCheckBoxes>
                    </td>
                </tr>
               
            </table>
        </div>
        <div class="sb_bottom">
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sidebar_box">
                <div class="sb_title">
                    <div style="float: left">
                        Date, Day & Time
                    </div>
                    <div style="float: right">
                        <asp:ImageButton ID="imgbtnResetDate" runat="server" Style="max-height: 14px; max-width: 14px;" ImageUrl="~/Content/StyleImages/ResFilter.png" OnClick="imgbtnResetDate_Click" Visible="false" />
                    </div>
                    <div style="clear: both"></div>
                </div>


                <div class="sb_content">
                    <div style="float: left; padding: 5px 0 3px 3px;display:none">
                        <asp:CheckBox ID="chkAnyday" runat="server" Text="Any day" AutoPostBack="true" OnCheckedChanged="chkAnyday_CheckedChanged" />
                    </div>
                    <div class="cleaner" style="display:none">
                    </div>
                    <table id="tabDays" runat="server" style="width: 200px;">
                        <tr>
                            <td class="style2">
                                <asp:CheckBox ID="chkMonday" runat="server" Text="Mon" AutoPostBack="true" OnCheckedChanged="chkMonday_CheckedChanged" />
                            </td>
                            <td class="style2">
                                <asp:CheckBox ID="chkTuesday" runat="server" Text="Tue" AutoPostBack="true" OnCheckedChanged="chkTuesday_CheckedChanged" />
                            </td>
                            <td class="style2">
                                <asp:CheckBox ID="chkWebnesday" runat="server" Text="Wed" AutoPostBack="true" OnCheckedChanged="chkWebnesday_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:CheckBox ID="chkThursday" runat="server" AutoPostBack="true" OnCheckedChanged="chkThursday_CheckedChanged"
                                    Text="Thur" />
                            </td>
                            <td class="style2">
                                <asp:CheckBox ID="chkFriday" runat="server" Text="Fri" AutoPostBack="true" OnCheckedChanged="chkFriday_CheckedChanged" />
                            </td>
                            <td class="style2">
                                <asp:CheckBox ID="chkSaturday" runat="server" AutoPostBack="true" OnCheckedChanged="chkSaturday_CheckedChanged"
                                    Text="Sat" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:CheckBox ID="chkSunday" runat="server" AutoPostBack="true" OnCheckedChanged="chkSunday_CheckedChanged"
                                    Text="Sun" />
                            </td>
                            <td class="style2">&nbsp;
                            </td>
                            <td class="style2">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <span style="font-weight: bold">Date</span></span>
                <br />
                    <div style="float: left">

                        <asp:TextBox ID="txtCalendarFrom" runat="server" Width="80px" CssClass="textbox"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtCalendarFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCalendarFrom">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtCalendarFrom_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtCalendarFrom" WatermarkText="From">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" PromptCharacter=" "
                            MaskType="Date" TargetControlID="txtCalendarFrom">
                        </ajaxToolkit:MaskedEditExtender>

                    </div>
                    <div style="float: right">
                        <asp:TextBox ID="txtCalendarTo" runat="server" Width="80px" CssClass="textbox"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCalendarTo">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtCalendarTo_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtCalendarTo" WatermarkText="To">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:MaskedEditExtender ID="txtCalendarTo_MaskedEditExtender" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="txtCalendarTo">
                        </ajaxToolkit:MaskedEditExtender>

                    </div>
                    <div class="cleaner">
                    </div>
                    <hr />
                    <span style="font-weight: bold">Time</span>
                    <br />
                    <div>
                        <asp:DropDownList ID="ddlTimeStart" runat="server" CssClass="bodyText1 dropdown" Width="180px">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlTimeEnds" runat="server" Width="180px" CssClass="bodyText1 dropdown">
                        </asp:DropDownList>
                    </div>
                    <div class="cleaner">
                    </div>


                </div>
                <div class="sb_bottom">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--<div class="sidebar_box">
        <div class="sb_title">
            <div style="float: left">
                Age
            </div>
            <div style="float: right">
                <asp:ImageButton ID="imgbtnResetAge" runat="server" Style="max-height: 14px; max-width: 14px;" ImageUrl="~/Content/StyleImages/ResFilter.png" OnClick="imgbtnResetAge_Click" />
            </div>
            <div style="clear: both"></div>
        </div>
        <div class="sb_content">
            <script>

                function CheckNumber(evt) {
                    evt = (evt) ? evt : window.event;
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    return true;
                }

            </script>
            <table style="width: 100%; padding-top: 10px">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="From"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAgeFrom" runat="server" Width="80px" onkeypress="return CheckNumber(event)" MaxLength="3"></asp:TextBox>

                        years
                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtAgeFrom_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtAgeFrom" WatermarkText="Age" WatermarkCssClass="bodyText2">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAgeTo" runat="server" Width="80px" onkeypress="return CheckNumber(event)" MaxLength="3"></asp:TextBox>

                        years
                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtAgeTo_TextBoxWatermarkExtender" runat="server"
                        WatermarkCssClass="bodyText2" TargetControlID="txtAgeTo" WatermarkText="Age">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sb_bottom">
        </div>
    </div>--%>
    
    <div style="float: right;margin:5px">
        <asp:Button ID="Apply" runat="server" Text="Apply" OnClick="Apply_Click" Width="90px"
            CssClass="button button-submit" />
    </div>
    <asp:HiddenField ID="hdnCategoryID" runat="server" />
    <asp:HiddenField ID="hdnProvider" runat="server" />
    <asp:HiddenField ID="hdnErrorText" runat="server" />
    <asp:HiddenField ID="hdnTagID" runat="server" />
    <asp:HiddenField ID="hdnFilterError" runat="server" />
    <asp:HiddenField ID="hdnShowLevel" runat="server" />
    <asp:HiddenField ID="hdnDTFrom" runat="server" />
    <asp:HiddenField ID="hdnDTTo" runat="server" />
    <asp:HiddenField ID="hdnTmFrom" runat="server" />
    <asp:HiddenField ID="hdnTmTo" runat="server" />
    <asp:HiddenField ID="hdnSectionID" runat="server" />
    <asp:HiddenField ID="hdnSuburbID" runat="server" />
    <asp:HiddenField ID="hdnAgeFrom" runat="server" />
    <asp:HiddenField ID="hdnAgeTo" runat="server" />
    <asp:HiddenField ID="hdnSectionItemNo" runat="server" />
    <asp:HiddenField ID="hdnShowListingAllCategory" runat="server" />
    <asp:HiddenField ID="hdnShowListingTitle" runat="server" />
    <asp:HiddenField ID="hdnShowTitle" runat="server" />
    <asp:HiddenField ID="hdnGroupingType" runat="server" />
    <asp:HiddenField ID="hdnStartingRef" runat="server" />
    <asp:HiddenField ID="hdnFiltered" runat="server" />
</div>
