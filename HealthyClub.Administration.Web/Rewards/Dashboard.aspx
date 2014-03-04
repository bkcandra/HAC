<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HealthyClub.Administration.Web.Rewards.Dashboard" %>
<%@ Register Src="~/UserControls/RewardDash.ascx" TagPrefix="uc1" TagName="RewardDash" %>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
        <!-- Bootstrap core CSS -->
 
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
  
    <uc1:RewardDash runat="server" ID="RewardDash" />
                
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
