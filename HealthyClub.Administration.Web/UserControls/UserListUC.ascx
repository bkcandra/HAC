<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserListUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.UserListUC" %>
<table>
    <tr>
        <td>
            Search Customer
        </td>
        <td>
            :
        </td>
        <td>
            <asp:TextBox ID="txtSearch" runat="server" Width="250px"></asp:TextBox>
            <asp:ImageButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
        </td>
    </tr>
</table>
<br />
<asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('This action will DELETE SELECTED CUSTOMER AND ITS HISTORY, Are You Sure ?');">Delete</asp:LinkButton>
<br />
<div>
    <asp:GridView ID="gridview1" runat="server" AutoGenerateColumns="False" Width="100%"
        OnRowCommand="gridview1_RowCommand" DataSourceID="ods" AllowPaging="True" 
        CellPadding="4" ForeColor="#333333" GridLines="None" 
        OnSelectedIndexChanged="gridview1_SelectedIndexChanged">
        <EditRowStyle BackColor="#2461BF" />
        <EmptyDataTemplate>
            Customer Data Not Found</EmptyDataTemplate>
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelected" runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Username">
                <ItemTemplate>
                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("UserID") %>' />
                    <asp:LinkButton ID="lnkUserName" runat="server" CommandName="Details" Text='<%#Eval("Username") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkName" runat="server" CommandName="Details" Text='<%#Eval("FirstName") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone">
                <ItemTemplate>
                    <asp:Label ID="lblJoinDate" runat="server" Text='<%#Eval("PhoneNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
</div>
<asp:HiddenField ID="hdnSearchString" runat="server" />
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
