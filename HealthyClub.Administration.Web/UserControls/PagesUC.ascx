<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PagesUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.PagesUC" %>

<asp:GridView ID="gridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="gridView1_RowCommand"
    Width="100%" OnRowDataBound="gridView1_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
    <EditRowStyle BackColor="#999999" />
    <EmptyDataTemplate>
        No Dynamic Page Available</EmptyDataTemplate>
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditDynamicPage" Text="Edit"></asp:LinkButton>
                <asp:HiddenField ID="hdnPage" runat="server" Value='<%#Eval("ID") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkDelete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete Selected Page?');"
                    CommandName="DeleteDynamicPage" Text="Delete"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Page Name">
            <ItemTemplate>
                <asp:Label ID="lblPageName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Page Title">
            <ItemTemplate>
                <asp:Label ID="lblPageTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Link To Page">
            <ItemTemplate>
                <asp:Label ID="lblPagelink" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#E9E7E2" />
    <SortedAscendingHeaderStyle BackColor="#506C8C" />
    <SortedDescendingCellStyle BackColor="#FFFDF8" />
    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView>
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
<asp:HiddenField ID="hdnPageType" runat="server" />