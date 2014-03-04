<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CouncilUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.CouncilUC" %>
<div id="suburbGridview">
    <div id="divSuccess" runat="server" visible="false" class="message success">
        <h5>Success</h5>
        <p>
            <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div id="divError" runat="server" visible="false" class="message error">
        <h5>Error</h5>
        <p>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div id="divLnkMenu">
        <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click1">New</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('This action will DELETE ALL SUBURBS Within selected Council, Are you sure ?')">Delete</asp:LinkButton>
    </div>
    <asp:GridView ID="GridView1" runat="server" Width="400px" AutoGenerateColumns="False"
        AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"
        OnRowCommand="GridView1_RowCommand"
        OnRowDataBound="GridView1_RowDataBound" CellPadding="4" ForeColor="#333333"
        GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:CheckBox ID="chkboxSelected" runat="server"></asp:CheckBox>
                    <asp:HiddenField ID="hdnID" Value='<%#Eval("ID") %>' runat="server"></asp:HiddenField>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkRename" runat="server" CommandName="RenameCouncil">Rename</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="LblCouncilName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:HiddenField ID="hdnCouncilValue" runat="server" />
                    <asp:Label ID="LblCouncilFullName" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
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
</div>
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
<asp:HiddenField ID="hdnSortParameter" runat="server" />

