<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardDash.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.RewardDash" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 <div class="grid_10">
        <div class="box sidebox">
            <h2>Rewards Stats</h2>
            <div class="block">
   <p>Go through each of the Dashboard Functions and view the data present as required</p>

    
        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Active Rewards</span>
            <p class="blue">
                <asp:Label ID="lblActiveRewards" runat="server"></asp:Label>
            </p>
        </div>
        
        
        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Total Rewards</span>
            <p class="blue">
                <asp:Label ID="lblTotalRewards" runat="server"></asp:Label>
            </p>
        </div>
        <div class="stat-col" style="width: 150px; text-align: center">
            <span>Total Sponsors</span>
            <p class="blue">
                <asp:Label ID="lblSponsors" runat="server"></asp:Label>
            </p>
        </div>
                <div class="stat-col" style="width: 150px; text-align: center">
            <span>Total Redemption</span>
            <p class="blue">
                <asp:Label ID="lblredempt" runat="server"></asp:Label>
            </p>
        </div>
            <div class="stat-col" style="width: 150px; text-align: center">
            <span>Never Redempted</span>
            <p class="blue">
                <asp:Label ID="lblnever" runat="server"></asp:Label>
            </p>
        </div>
        <div class="stat-col last" style="width: 150px; text-align: center">
            <span>Total Visits</span>
            <p class="blue">
                <asp:Label ID="lblVisits" runat="server"></asp:Label>

            </p>
            <br /> <asp:Label ID="lblerror" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>

        <div class="clear">
        </div>
        </div>
    </div>
</div>
<div class="grid_10">
        <div class="box sidebox">
            <h2>Reward Redemption Chart</h2>
            <div class="block">

                   <asp:label ID="header" runat="server" Text="Choose Year"></asp:label>&nbsp;
                    <asp:DropDownList ID="ddlYears" runat="server" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>
                    <hr />
                    <cc1:BarChart ID="BarChart1" runat="server" ChartHeight="300" ChartWidth = "800" style="width:810px" ChartType="Column" ChartTitleColor="#0E426C" Visible = "false" CategoryAxisLineColor="#22536e " ValueAxisLineColor="#22536e " BaseLineColor="#3a8bb8 ">
</cc1:BarChart>
   </div>
            </div></div>