<%@ Page Title="SPonsor Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SponsorTools.aspx.cs" Inherits="HealthyClub.Administration.Web.Rewards.SponsorTools" %>
<%@ Register Src="~/UserControls/SponsorManage.ascx" TagPrefix="uc1" TagName="SponsorManage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid_2">
        <div class="box sidemenu" style="height: 800px">
            <div class="block" id="section-menu">
                <ul class="section menu">
                    <li><a class="menuitem" href="Default.aspx">Sponsors Setup</a>
                        <ul class="submenu">
                            <li><a href="Sponsor.aspx">Sponsors Manager</a> </li>
                            <li><a href="Default.aspx">Rewards Manager</a> </li>
                        </ul>
                    </li>
                  
                </ul>
            </div>
        </div>
    </div>
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Sponsor Setup</h2>
            <div class="block">
                <uc1:SponsorManage ID="SponsorManage1" runat="server" />
            </div>
        </div>
    </div>
    </asp:Content>