<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityRegistrationTimetableUC.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ActivityRegistrationTimetableUC" %>

<%@ Register Src="~/UserControls/ScheduleViewerUC.ascx" TagName="ScheduleViewerUC"
    TagPrefix="uc1" %>

<style type="text/css">
    .style1 {
        padding-left: 15px;
    }

    .buttonAddTimetable {
        display: block;
        margin: 5px 3px 0 3px;
        padding: 5px;
        text-decoration: none;
        text-align: center;
        font: bold 13px Verdana, Arial, Helvetica, sans-serif;
        color: #FFF;
        outline-style: none;
        background-color: #499ECA;
        border: 1px solid #499ECA;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
    }
    /*AJAX Calendar
    */
    .cal_Theme1 .ajax__calendar_container {
        background-color: #e2e2e2;
        border: solid 1px #cccccc;
    }

    #main {
        height: auto !important;
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

    .cal_Theme1 .ajax__calendar_today {
        font-weight: bold;
    }

    .cal_Theme1 .ajax__calendar_other,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
        color: #bbbbbb;
    }
</style>

<asp:UpdatePanel ID="UpdatePanelTimetable" runat="server" EnableViewState="true">
    <ContentTemplate>

        <asp:HiddenField ID="hdnEdit" runat="server" />
        <asp:HiddenField ID="hdnActivityID" runat="server" />
        <asp:Label ID="Label1" runat="server" Text="In this section you can provide details to Club Members about when your activity is held.&nbsp;&nbsp;You can choose to add a timetable or have Club Members contact your directly for your activity details."></asp:Label>
        <hr />
        <asp:RadioButton ID="radEnableTimetable" runat="server" GroupName="useTimetable"
            AutoPostBack="true" Text="This activity has a set timetable" OnCheckedChanged="radEnableTimetable_CheckedChanged"
            Checked="true" />&nbsp;&nbsp;
        <asp:RadioButton AutoPostBack="true" ID="radNoTimetable" runat="server" GroupName="useTimetable"
            Text="This activity does not have a set timetable" OnCheckedChanged="radNoTimetable_CheckedChanged" />
        <div id="divTimetable" runat="server">
            <fieldset style="width: 80%">
                <legend>
                    <strong>Timetable details</strong>
                </legend>
                <table>
                    <tr>
                        <td>Please select the start and finish dates for your activity.
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:TextBox ID="txtCalendarFrom" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtCalendarFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCalendarFrom" CssClass="cal_Theme1">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtCalendarFrom_TextBoxWatermarkExtender" WatermarkText="Start Date"
                                WatermarkCssClass="bodyText2" runat="server" TargetControlID="txtCalendarFrom">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtEndDate" runat="server" ToolTip="You will receive notification to update you activity on this date" OnTextChanged="txtEndDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtEndDate_TextBoxWatermarkExtender" runat="server"
                                WatermarkCssClass="bodyText2" TargetControlID="txtEndDate" WatermarkText="Finish Date">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate" CssClass="cal_Theme1">
                            </ajaxToolkit:CalendarExtender>
                            <br />
                            <asp:Label ID="lblMaxDate" runat="server" Visible="false" ForeColor="Red" Text="Maximum finish date is 6 months or 150 days"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>Please tick what days your activity is held.
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <table style="width: 450px">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkMonday" runat="server" Text="Monday" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkTuesday" runat="server" Text="Tuesday" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkWebnesday" runat="server" Text="Wednesday" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkThursday" runat="server" Text="Thursday" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkFriday" runat="server" Text="Friday" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkSaturday" runat="server" Text="Saturday" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkSunday" runat="server" Text="Sunday" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>Please select your activity start and finish time.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlTimeStart" runat="server">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlTimeEnds" runat="server">
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
                    </tr>
                    <tr>
                        <td>How often is your activity held?
                        <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="radWeekly" runat="server" Text="Weekly" Checked="true" GroupName="Recurrence" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="radFortnightly" runat="server" Text="Fortnightly" GroupName="Recurrence" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="radMonthly" runat="server" Text="Monthly" GroupName="Recurrence" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="radNoRecurrence" runat="server" Text="No set recurrence" GroupName="Recurrence" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>Please click the button below to add your timetable.&nbsp;&nbsp; You can add multiple timetables for your activity by repeating the above steps and clicking add timetable again.</td>
                    </tr>
                    <tr>
                        <td>                          
                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Style="float: left" Text="Add Timetable" CssClass="buttonAddTimetable" />
                                <asp:Label ID="lblError" runat="server" Style="text-align: left; top: 25px; position:relative; color:red" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hr />
            <asp:HiddenField ID="hdnExceptionTimetable" runat="server" />
            <asp:HiddenField ID="hdnFrom" runat="server" />
            <asp:HiddenField ID="hdnTo" runat="server" />
            Below you can choose to delete an activity by clicking on "Select All" or selecting
            timetables individually.
            <div id="divtableMenu3">
                <asp:LinkButton ID="lnkSelectAll2" runat="server" OnClick="lnkSelect2All_Click" CssClass="linkBtn2">Select All</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkDelete2" runat="server" OnClick="lnkDelete2_Click" CssClass="linkBtn2">Delete</asp:LinkButton>
            </div>
            <div style="margin: 10px 0">
                <asp:GridView ID="gridviewTimetable" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    OnRowDataBound="gridviewTimetable_RowDataBound" Width="98%" CellPadding="4" ForeColor="#333333"
                    GridLines="None">
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                        No timetable listed.
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkboxSelected0" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activity Date and Time" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblTime" runat="server" Text="Time"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recurrence" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblRecurring" runat="server" Text="Recurring"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expiry Date" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblExpiry" runat="server" Text="Expiry"></asp:Label>
                                <asp:HiddenField ID="hdnStartDateTime" runat="server" Value='<%#Eval("ActivityStartDatetime") %>' />
                                <asp:HiddenField ID="hdnEndDateTime" runat="server" Value='<%#Eval("ActivityEndDatetime") %>' />
                                <asp:HiddenField ID="hdnExpiryDateTime" runat="server" Value='<%#Eval("ActivityExpiryDate") %>' />
                                <asp:HiddenField ID="hdnRecurrenceType" runat="server" Value='<%#Eval("RecurrenceType") %>' />
                                <asp:HiddenField ID="hdnRecurEvery" runat="server" Value='<%#Eval("RecurEvery") %>' />
                                <asp:HiddenField ID="hdnOnMonday" runat="server" Value='<%#Eval("OnMonday") %>' />
                                <asp:HiddenField ID="hdnOnTuesday" runat="server" Value='<%#Eval("OnTuesday") %>' />
                                <asp:HiddenField ID="hdnOnWednesday" runat="server" Value='<%#Eval("OnWednesday") %>' />
                                <asp:HiddenField ID="hdnOnThursday" runat="server" Value='<%#Eval("OnThursday") %>' />
                                <asp:HiddenField ID="hdnOnFriday" runat="server" Value='<%#Eval("OnFriday") %>' />
                                <asp:HiddenField ID="hdnOnSaturday" runat="server" Value='<%#Eval("OnSaturday") %>' />
                                <asp:HiddenField ID="hdnOnSunday" runat="server" Value='<%#Eval("OnSunday") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <EmptyDataRowStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <div style="clear: both; height: 200px"></div>
            <%--<div id="divFullTimetable" runat="server" visible="false">
                <strong>Timetable Preview</strong>
                <br />
                Here is a preview of your activity timetable. To view your full activity timetable
            click on the "See Full Timetable" box.
            <br />
                <div style="margin: 10px 0">
                    <uc1:ScheduleViewerUC ID="ScheduleViewerUC1" runat="server" />
                </div>
                <%--<asp:CheckBox ID="chkFullTimetable" runat="server" Text="See Full Timetable" AutoPostBack="true"
                    OnCheckedChanged="chkFullTimetable_CheckedChanged" Visible="false" />
                <br />
                <div style="margin: 10px 0">
                    <asp:GridView ID="gridviewPreview" Visible="False" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting"
                        Width="98%" PageSize="20" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <EditRowStyle BackColor="#999999" />
                        <EmptyDataRowStyle BackColor="#B2E1FF" />
                        <EmptyDataTemplate>
                            No timetable listed.
                        </EmptyDataTemplate>
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Activity Date" ItemStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Day" ItemStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDay" runat="server" Text="Day"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Time" ItemStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
                <br />
            </div>--%>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
