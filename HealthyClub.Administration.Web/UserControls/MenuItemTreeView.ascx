<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuItemTreeView.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.MenuItemTreeView" %>
<asp:TreeView ID="TreeView1" runat="server" 
     ImageSet="Simple" onselectednodechanged="TreeView1_SelectedNodeChanged">
    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
        HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
    <ParentNodeStyle Font-Bold="False" />
    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
        HorizontalPadding="0px" VerticalPadding="0px" />
</asp:TreeView>
