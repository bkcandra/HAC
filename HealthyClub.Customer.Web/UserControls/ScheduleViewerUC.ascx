<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduleViewerUC.ascx.cs" Inherits="HealthyClub.Web.UserControls.TimeViewer" %>
<%@ Register src="ScheduleDetailViewerUC.ascx" tagname="ScheduleDetailViewerUC" tagprefix="uc1" %>
<asp:HiddenField ID="hdntimetable" runat="server" />
<asp:HiddenField ID="hdnActivityID" runat="server" />
<div id="divMain" runat="server" style="width: 100%">
    <asp:ListView ID="ListView1" runat="server"
        onitemdatabound="ListView1_ItemDataBound">
        <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <table width="100%">
                <tr>
                    <td style="width: 100px; vertical-align:top">
                        <asp:Label ID="lblDay" runat="server" CssClass="infoLabel" Text="Day"></asp:Label>
                    </td>
                    <td style="vertical-align:top">
                        <uc1:ScheduleDetailViewerUC ID="ScheduleDetailViewerUC" runat="server" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:ListView>
    <div id="divNoTimetable" runat="server" visible="false">
        <asp:Label ID="lblNoTimetable" runat="server"></asp:Label>
    </div>
</div>
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>


