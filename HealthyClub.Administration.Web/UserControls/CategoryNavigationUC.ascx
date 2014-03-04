<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryNavigationUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.CategoryNavigationUC" %>

<div style="background-color: #FFFBD6">
    <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%#Eval("ID") %>' />
    <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
        <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:LinkButton ID="lnkNavigation" runat="server" Text='<%#Eval("Name")%>' CommandName="FindFolder"></asp:LinkButton>
            <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %>' />
        </ItemTemplate>
        <ItemSeparatorTemplate>
            >>
        </ItemSeparatorTemplate>
    </asp:ListView>
</div>
