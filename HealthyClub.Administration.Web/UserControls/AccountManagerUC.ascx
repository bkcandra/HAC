<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountManagerUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.AccountManagerUC" %>
<%@ Register Src="~/UserControls/AdminList.ascx" TagPrefix="uc1" TagName="AdminList" %>
<%@ Register Src="~/UserControls/ProviderList.ascx" TagPrefix="uc1" TagName="ProviderList" %>
<%@ Register Src="~/UserControls/UserListUC.ascx" TagPrefix="uc1" TagName="UserListUC" %>

<ajaxToolkit:TabContainer ID="AdministratorTabContainer" runat="server" ActiveTabIndex="0">
    <ajaxToolkit:TabPanel runat="server" HeaderText="Administrator Accounts" ID="AdministratorTab">
        <ContentTemplate>
            
            <uc1:AdminList ID="AdminList1" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="ProviderTab" runat="server" HeaderText="Provider Accounts">
        <ContentTemplate>
            <uc1:ProviderList ID="ProviderList1" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
    <ajaxToolkit:TabPanel ID="CustomerTab" runat="server" HeaderText="Customer Accounts">
        <ContentTemplate>
            <uc1:UserListUC ID="UserListUC1" runat="server" />
        </ContentTemplate>
    </ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
