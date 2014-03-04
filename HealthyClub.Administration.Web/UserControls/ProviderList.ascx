<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderList.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.ProviderList" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        Search Provider:<asp:TextBox ID="txtSearch" runat="server" Width="250px"></asp:TextBox>
        <asp:ImageButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" />

        <br />
        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('This action will DELETE SELECTED Provider AND ITS related content, Are You Sure ?');">Delete</asp:LinkButton>
        &nbsp;
        <asp:HyperLink ID="lnkExport" runat="server" Target="_blank" NavigateUrl="~/Report/Provider.aspx">Export to csv</asp:HyperLink>
        <br />
        <div>
            <asp:GridView ID="gridview1" runat="server" AutoGenerateColumns="False" Width="100%"
                OnRowCommand="gridview1_RowCommand" DataSourceID="ods" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None"
                OnPageIndexChanged="gridview1_PageIndexChanged" OnRowDataBound="gridview1_RowDataBound">
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataTemplate>
                    Customer Data Not Found
                </EmptyDataTemplate>
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
                            <asp:LinkButton ID="lnkUserName" runat="server" CommandName="Details" Text='<%#Eval("Username") %>'></asp:LinkButton>
                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%#Eval("UserID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkName" runat="server" CommandName="Details" Text='<%#Eval("FirstName") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Image ID="imgEmailIcon" runat="server" />
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
        <div style="border: 1px solid #507CD1">
            <table>
                <thead>
                    <tr>
                        <td colspan="3" style="background-color: #507CD1; color: white; font-weight: bold">Provider Details</td>
                    </tr>
                </thead>
                <tr>
                    <td style="vertical-align: top">Member ID</td>
                    <td></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblID" runat="server" Text="n/a"></asp:Label>
                        <asp:HiddenField ID="hdnID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Member GUID</td>
                    <td></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblGUID" runat="server" Text="n/a"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Username</td>
                    <td></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblUsername" runat="server" Text="n/a"></asp:Label>
                        <asp:HiddenField ID="hdnUserID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Email Confirmed</td>
                    <td></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblConfirmed" runat="server" Text="n/a"></asp:Label>
                        <asp:HiddenField ID="hdnIsConfirmed" runat="server" />
                        <asp:HiddenField ID="hdnEmail" runat="server" />
                        <asp:HiddenField ID="hdnConfirmationToken" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Activity Count</td>
                    <td></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblActCount" runat="server" Text="n/a"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: right">
                        <asp:HyperLink ID="lnkEdit" runat="server">Edit</asp:HyperLink>&nbsp;&nbsp;
                <asp:LinkButton Enabled="false" ID="lnkConfirm" runat="server" ToolTip="Confirm this provider. This action will give this provider permission to log in without confirming their email address" OnClick="lnkConfirm_Click">Confirm</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton Enabled="false" ID="lnkResenConfirmation" runat="server" ToolTip="Resend confirmation email. Resend an email address containing email confirmation for this provider" OnClick="lnkResenConfirmation_Click">Resend Confirmation Email</asp:LinkButton>&nbsp;&nbsp;
                    </td>
                </tr>

            </table>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>


