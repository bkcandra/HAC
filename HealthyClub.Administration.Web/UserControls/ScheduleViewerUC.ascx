<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduleViewerUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.ScheduleViewerUC" %>
<%@ Register Src="ScheduleDetailViewerUC.ascx" TagName="ScheduleDetailViewerUC" TagPrefix="uc1" %>
<asp:HiddenField ID="hdntimetable" runat="server" />
<asp:HiddenField ID="hdnActivityID" runat="server" />
<div id="divMain" runat="server" style="width: 100%">
    <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
        <EmptyDataTemplate>
            No Data Found.
        </EmptyDataTemplate>
        <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <table width="100%">
                <tr>
                    <td style="width: 15%; vertical-align: top">
                        <asp:Label ID="lblDay" runat="server" CssClass="infoLabel" Text="Day"></asp:Label>
                    </td>
                    <td style="width: 85%; vertical-align: top">
                        <uc1:ScheduleDetailViewerUC ID="ScheduleDetailViewerUC1" runat="server" />
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
