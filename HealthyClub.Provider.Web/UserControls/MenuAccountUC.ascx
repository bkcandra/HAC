<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuAccountUC.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.MenuAccountUC" %>
<asp:Menu ID="Menu1" runat="server" StaticDisplayLevels="3" BackColor="#F7F6F3" Width="100%"
    DynamicHorizontalOffset="2" Font-Names="arial" Font-Size="14px" ForeColor="#1B274F"
    StaticSubMenuIndent="10px">
    <DynamicHoverStyle BackColor="#1B274F" ForeColor="White" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicMenuStyle BackColor="#1B274F" />
    <DynamicSelectedStyle BackColor="#1B274F" />
    <Items>
        <asp:MenuItem Text="Account Information" Value="New" NavigateUrl="~/Account/"></asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/Account/ChangePassword.aspx" Text="Change Password" Value="Open"></asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/Account/ProfileImage.aspx" Text="Account Image" Value="Open"></asp:MenuItem>
        <asp:MenuItem NavigateUrl="~/Account/CancelAccount.aspx" Text="Cancel Account" Value="Open"></asp:MenuItem>
    </Items>
    <StaticHoverStyle BackColor="#1B274F" ForeColor="White" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticSelectedStyle BackColor="#1B274F" />
</asp:Menu>
