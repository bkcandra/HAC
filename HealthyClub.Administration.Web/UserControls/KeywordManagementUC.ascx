<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KeywordManagementUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.KeywordManagementUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript">
    function Ok() {
        var btnSave = document.getElementById('<%= btnSave.ClientID %>');

        if (btnSave != null) {
            btnSave.click();
        }

    }
    function Cancel() {
        var btnCancel = document.getElementById('<%= btnCancel.ClientID %>');

        if (btnCancel != null) {
            btnCancel.click();
        }

    }
</script>
<style type="text/css">
    .style1
    {
        width: 4px;
        vertical-align: top;
    }
    .style2
    {
        width: 150px;
        vertical-align: top;
    }
    .style3
    {
        font-size: xx-small;
    }
    .style4
    {
        width: 150px;
        vertical-align: top;
        font-weight: bold;
        color: #000000;
    }
    .style5
    {
        width: 150px;
        vertical-align: top;
        color: #000000;
    }
    .style6
    {
        vertical-align: top;
        color: #000000;
    }
</style>
<div id="suburbGridview">
    <div id="divLnkMenu">
        <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click1">New</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('This action will DELETE ALL CONTACT INFORMATION With selected suburbs, Are you sure ?')">Delete</asp:LinkButton>
    </div>
    <asp:GridView ID="GridView1" runat="server" Width="80%" AutoGenerateColumns="False"
        AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"
        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" CellPadding="4"
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EmptyDataTemplate>
            No Data found, Insert new data.
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-Width="20px">
                <ItemTemplate>
                    <asp:CheckBox ID="chkboxSelected" runat="server"></asp:CheckBox>
                    <asp:HiddenField ID="hdnID" Value='<%#Eval("ID") %>' runat="server"></asp:HiddenField>
                </ItemTemplate>

<ItemStyle Width="20px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" ItemStyle-Width="65px">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditKey">Edit</asp:LinkButton>
                </ItemTemplate>

<ItemStyle Width="65px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Collection Name" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Label ID="LblCollectionName" runat="server" Text='<%#Eval("Name")%>' ToolTip='<%#Eval("Description")%>'></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Number of Keywords" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Label ID="lblKeyNumber" runat="server" Text=""></asp:Label>
                </ItemTemplate>

<ItemStyle Width="150px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Synonyms">
                <ItemTemplate>
                    <asp:Label ID="LblKeywords" runat="server" Text='<%#Eval("Keywords")%>'></asp:Label>
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
<div id="divThesaurusPopUp" runat="server" style="border-style: solid; border-width: thin; background-color:white"
    class="ModalWindow">
    <table width="600px">
        <tr>
            <td class="style4">
                Collection Name
            </td>
            <td class="style1">
                :
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                    ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateNewProvider"><label class="required">* Required</label></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style4">
                Description
            </td>
            <td class="style1">
                :
            </td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <b>Synonym(s)<br />
                </b><strong><span class="style3">Separate with semicolon(;)</span></strong>
                <asp:HiddenField ID="hdnThesaurusID" runat="server" />
                <asp:HiddenField ID="hdnThesaurusKeyID" runat="server" />
            </td>
            <td class="style1">
                :
            </td>
            <td class="style6">
                <asp:TextBox ID="txtSynonims" runat="server" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSynonims"
                    ErrorMessage="Synonym(s) is required." ToolTip="Synonym(s) is required." ValidationGroup="CreateNewProvider"><label Class="required">* Required</label></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td class="style1">
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnOk" runat="server" Text="Save" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
            </td>
        </tr>
    </table>
</div>
<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
    OkControlID="btnOk" OnOkScript="Ok()" PopupControlID="divThesaurusPopUp" TargetControlID="lnkNew"
    BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>
<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Style="display: none" />
<asp:HiddenField ID="hdnType" runat="server" />
