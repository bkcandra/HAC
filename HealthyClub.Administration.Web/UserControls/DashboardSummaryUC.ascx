<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardSummaryUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.DashboardSummaryUC" %>
<div class="box sidebox">
    <h2>Latest Accounts</h2>
    <div class="block">
        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Members</span>
            <p class="blue">
                <asp:Label ID="lblMember" runat="server"></asp:Label>
            </p>
        </div>
        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Providers</span>
            <p class="blue">
                <asp:Label ID="lblProviders" runat="server"></asp:Label>
            </p>
        </div>
        <div class="stat-col last" style="width: 150px; text-align: center">
            <span>Visitor this month</span>
            <p class="blue">
                <img src="img/icon-direction.png" alt="" />&nbsp;
        <asp:Label ID="lblstat" runat="server">-</asp:Label>
            </p>
        </div>
        <br />
        <asp:Label ID="lblerror" runat="server" Text="" ForeColor="Red"></asp:Label>
        <div class="clear">
        </div>
    </div>
</div>
<br />
<div class="box sidebox">
    <h2>Activities Stats</h2>
    <div class="block">
        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Total  Activities</span>
            <p class="blue">
                <asp:Label ID="lblTotalActivity" runat="server"></asp:Label>
            </p>
        </div>
        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Approved Activities</span>
            <p class="blue">
                <asp:Label ID="lblApprovedActivity" runat="server"></asp:Label>
            </p>
        </div>
        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Expired Activities</span>
            <p class="blue">
                <asp:Label ID="lblExpiredAct" runat="server"></asp:Label>
            </p>
        </div>
        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Delete queue</span>
            <p class="blue">
                <asp:Label ID="lblDeletedAct" runat="server"></asp:Label>
            </p>
        </div>

        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Waiting for approval</span>
            <p class="blue">
                <asp:Label ID="lblWaitingActivity" runat="server"></asp:Label>
            </p>
        </div>
        <div class="stat-col last" style="width: 150px; text-align: center">
            <span>Categories</span>
            <p class="blue">
                <asp:Label ID="lblCat" runat="server"></asp:Label>
            </p>
        </div>

        <div class="clear">
        </div>
    </div>
</div>
