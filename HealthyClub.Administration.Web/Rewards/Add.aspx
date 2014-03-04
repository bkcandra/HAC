<%@ Page Title="Add Reward" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="HealthyClub.Administration.Web.Rewards.Add" %>
<%@ Register Src="~/UserControls/RewardsAddition.ascx" TagPrefix="uc1" TagName="RewardsAddition" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem" href="Default.aspx">Rewards Management</a>
                        <ul class="submenu">
                            <li><a href ="Dashboard.aspx">Dashboard</a></li>
                            <li><a href="Add.aspx">Add Reward</a> </li>
                            <li><a href="OCR.aspx">Add Activity Attendance</a></li>
                            <li><a href ="Sponsor.aspx">Sponsors Page</a></li>
                            
                            
                        </ul>
                    </li>

                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Add New Reward</h2>
            <div class="block">
    <uc1:RewardsAddition runat="server" ID="RewardsAddition" />
                </div></div></div>
</asp:Content>
