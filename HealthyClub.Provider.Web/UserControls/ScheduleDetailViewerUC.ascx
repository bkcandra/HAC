<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduleDetailViewerUC.ascx.cs" Inherits="HealthyClub.Providers.Web.UserControls.ScheduleDetailViewerUC" %>
<asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
    <LayoutTemplate>
        <div id="ItemPlaceHolder" runat="server">
        </div>
    </LayoutTemplate>
    <ItemTemplate>
    <div class="infoLabel">
        <asp:Label ID="lblStartTime" runat="server" CssClass="infoLabel" Text="Time"></asp:Label>&nbsp;-
        <asp:Label ID="lblEndTime" runat="server" CssClass="infoLabel" Text="Time"></asp:Label>
    </div>
    </ItemTemplate>
</asp:ListView>