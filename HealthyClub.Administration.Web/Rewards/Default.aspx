<%@ Page Title="All Rewards" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthyClub.Administration.Web.Rewards.Default" %>

<%@ Register Src="~/UserControls/RewardsManager.ascx" TagPrefix="uc1" TagName="RewardsManager" %>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem" href="Default.aspx">Rewards Management</a>
                        <ul class="submenu">
                               <li><a href="Dashboard.aspx">Dashboard</a></li>
                            <li><a href="Add.aspx">Add Reward</a> </li>

                            <li><a href="OCR.aspx">Add Activity Attendance</a></li>
                            <li><a href="Sponsor.aspx">Sponsors Page</a></li>
                     


                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Rewards</h2>
            <div class="block">
                <uc1:RewardsManager runat="server" ID="RewardsManager" />
            </div>
        </div>
    </div>
</asp:Content>

