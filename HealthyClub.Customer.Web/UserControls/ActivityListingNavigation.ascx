<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityListingNavigation.ascx.cs" Inherits="HealthyClub.Customer.Web.UserControls.ActivityListingNavigation" %>

<div class="nav_listing">
    <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound" OnItemCommand="ListView1_ItemCommand">
        <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <span class="nav_item">
                <asp:Label ID="lblNav" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                <asp:LinkButton ID="lnkCloseNav" runat="server" CssClass="remove btn-navclose" CommandName="CloseNav">&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>
                <asp:HiddenField ID="hdnType" runat="server" Value='<%#Eval("type") %>' />
            </span>
        </ItemTemplate>
        <ItemSeparatorTemplate>
            <a class="separator">&nbsp;></a>
        </ItemSeparatorTemplate>
    </asp:ListView>
</div>
<asp:HiddenField ID="hdnFiltered" runat="server" />
<asp:HiddenField ID="hdnSearchKey" runat="server" />


