<%@ Page Title="Modify Reward" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="HealthyClub.Administration.Web.Rewards.Modify" EnableViewState="true" %>

<%@ Register Src="~/UserControls/RewardModify.ascx" TagPrefix="uc1" TagName="RewardModify" %>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem" href="Default.aspx">Rewards Setup</a>
                        <ul class="submenu">
                            <li><a href="javascript:history.back()">Back</a> </li>
                        </ul>
                    </li>
                  
                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Rewards Setup</h2>
            <div class="block">
                <uc1:RewardModify ID="RewardModify1" runat="server" />
            </div>
        </div>
    </div>

</asp:Content>

