<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityNavigationUC.ascx.cs" Inherits="HealthyClub.Customer.Web.UserControls.ActivityNavigationUC" %>
<style>
    .nav .separator {
        color: #479ECA;
    }
    .nav .Act {
        color: #1B274F;
    }
    
</style>
<div class="nav">
    <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
        <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:HyperLink ID="hlnkNavigation" runat="server" Text='<%#Eval("Name")%>'
                Font-Bold="True"></asp:HyperLink>
            <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %>' />
        </ItemTemplate>
        <ItemSeparatorTemplate>
            <span class="separator">>></span>
        </ItemSeparatorTemplate>
    </asp:ListView>
</div>
<asp:HiddenField ID="hdnCategoryID" runat="server" />


