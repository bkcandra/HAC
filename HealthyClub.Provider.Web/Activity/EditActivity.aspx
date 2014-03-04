<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditActivity.aspx.cs" Inherits="HealthyClub.Providers.Web.Activity.EditActivity"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/ActivityRegistrationDetailUC.ascx" TagName="ActivityRegistrationDetailUC"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ActivityRegistrationDescriptionUC.ascx" TagName="ActivityRegistrationDescriptionUC"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ActivityRegistrationTimetableUC.ascx" TagName="ActivityRegistrationTimetableUC"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControls/ActivityRegistrationGroup.ascx" TagName="ActivityRegistrationGroup"
    TagPrefix="uc4" %>
<%@ Register Src="../UserControls/AcitivityScheduleDetailUC.ascx" TagName="acitivityscheduledetailuc"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ActivityImageUC.ascx" TagName="ActivityImageUC"
    TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>

    </style>
    <asp:Label ID="lblPageTitle" runat="server" Text="Edit Activity Information" CssClass="pageTitle"> </asp:Label>
    <br />
    <div id="divError" runat="server" class="errorBox" visible="false" style="width: 800px">
        <span id="Title">Oops! You haven't finished entering in your activity details. </span>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>

    </div>
    <fieldset style="width: 350px;">
        <legend>Activity Status</legend>
        <asp:RadioButton ID="radActive" runat="server" Text="Active" Checked="true" GroupName="radDelete" />&nbsp;&nbsp;
    <asp:RadioButton ID="radInactive" runat="server" Text="Inactive" GroupName="radDelete"/>&nbsp;&nbsp;<br />
        <div style="padding-left: 5px; width: 100%">
            <asp:Label ID="lblStatus" runat="server" Text="Deleted" Visible="false" ForeColor="Red"></asp:Label>
        </div>

    </fieldset>

    <asp:TabContainer ID="DescriptionTabContainer" runat="server" ActiveTabIndex="0" Width="920px">
        <asp:TabPanel runat="server" HeaderText=">Activity Details" ID="DescriptionTab">
            <HeaderTemplate>
                Activity Details
            
            
</HeaderTemplate>

            
<ContentTemplate>
                <uc1:ActivityRegistrationDetailUC ID="ActivityRegistrationDetailUC1" runat="server" />



            
</ContentTemplate>

        
</asp:TabPanel>
        <asp:TabPanel ID="FeesTab" runat="server" HeaderText="Category & Description">
            <ContentTemplate>
                <uc2:ActivityRegistrationDescriptionUC ID="ActivityRegistrationDescriptionUC1" runat="server" />

            
</ContentTemplate>

        
</asp:TabPanel>
        <asp:TabPanel ID="TimetableTab" runat="server" HeaderText="Timetable">
            <ContentTemplate>
                <uc3:ActivityRegistrationTimetableUC ID="ActivityRegistrationTimetableUC1" runat="server" />

            
</ContentTemplate>
        
</asp:TabPanel>
        <asp:TabPanel ID="GroupingTab" runat="server" HeaderText="Grouping">
            <ContentTemplate>
                <uc4:ActivityRegistrationGroup ID="activityregistrationgroup1" runat="server" />

            
</ContentTemplate>
        
</asp:TabPanel>
        <asp:TabPanel ID="ActivityImageTab" runat="server" HeaderText="Activity Image">
            <ContentTemplate>
                <uc5:ActivityImageUC ID="ActivityImageUC1" runat="server"></uc5:ActivityImageUC>
            
</ContentTemplate>
        
</asp:TabPanel>
    </asp:TabContainer>

    <div style="text-align: right; width: 920px;">
        <asp:Button ID="BtnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="buttonCreateAct" OnClientClick="return confirm('Are you sure you want to delete this activity?  Once you have deleted an activity it will be removed in three days and cannot be recovered.')" />
        <asp:Button ID="btnRestore" runat="server" Text="Restore" OnClick="btnRestore_Click" CssClass="buttonCreateAct" OnClientClick="return confirm('Cancel deleting activity?  Activity status will be changed to inactive')" />&nbsp;&nbsp;
        <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="buttonCreateAct" />&nbsp;&nbsp;
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="buttonCreateAct" />
    </div>
    <asp:HiddenField ID="hdnProviderID" runat="server" />
    <asp:HiddenField ID="hdnReferrer" runat="server" />
    <asp:HiddenField ID="hdnActivityID" runat="server" />


</asp:Content>
