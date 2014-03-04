<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoriesUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.CategoriesUC" %>
<%@ Register Src="~/UserControls/CategoryNavigationUC.ascx" TagPrefix="uc1" TagName="CategoryNavigationUC" %>



<script type="text/javascript">
    function Ok() {
        //alert('Move all item in this folder to ----------------');
        var btnMove = document.getElementById('<%= btnMove.ClientID %>');

        if (btnMove != null) {
            btnMove.click();
        }

    }

    function Ok1() {
        //alert('Move all item in this folder to ----------------');
        var btnMove1 = document.getElementById('<%= btnMove1.ClientID %>');

        if (btnMove1 != null) {
            btnMove1.click();
        }

    }
    function Cancel() {
        //alert('Move all item in this folder to ----------------');
        var btnCancelPopUp = document.getElementById('<%= btnCancelPopUp.ClientID %>');

        if (btnCancelPopUp != null) {
            btnCancelPopUp.click();
        }

    }
</script>
<style type="text/css">
    .style1 {
        width: 75px;
    }

    .style2 {
        width: 5px;
    }

    .style3 {
        width: 3px;
    }
</style>
<div id="divNavigation" runat="server">
    <uc1:CategoryNavigationUC ID="NavigationUC1" runat="server" OnRefreshNavigation="NavigationUC1_RefreshNavigation" />
</div>

<div id="divDataView" runat="server" visible="false">


    <div class="block">
        <h5>Category information</h5>
        <table>
            <tr>
                <td>Name</td>
                <td>&nbsp;&nbsp;</td>
                <td>
                    <asp:Label ID="lblNameCategory" runat="server"></asp:Label>
                    <asp:TextBox ID="txtNameCategory" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>&nbsp;&nbsp;</td>
                <td>
                    <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lnkSaveCategory" runat="server" Visible="false" OnClick="lnkSaveCategory_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="lnkCancelCategory" runat="server" Visible="false" OnClick="lnkCancelCategory_Click">Cancel</asp:LinkButton>
                </td>
            </tr>
        </table>

    </div>


</div>
<asp:Label ID="lblSubCategory" runat="server" Text="Sub Categories" Font-Bold="True"></asp:Label>
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

<div id="divLnkMenu" runat="server">
    <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click">New</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkMove" runat="server">Move</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('This action will DELETE ALL PRODUCT AND CATEGORY under selected Category, Are you sure ?')">Delete</asp:LinkButton>
</div>
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkMove"
    PopupControlID="divPopMoveDestination" CancelControlID="lnkCancel" OkControlID="lnkOk"
    OnOkScript="Ok()">
</ajaxToolkit:ModalPopupExtender>
<asp:Button ID="btnMove" runat="server" Style="display: none" OnClick="btnMove_Click"></asp:Button>
<div id="divPopMoveDestination" runat="server" style="border: medium ridge #000000; background-color: #FFFFFF; width: 450px;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divMoveDestinationTitle" style="border-color: #000000; width: 100%; border-bottom-style: solid; border-bottom-width: thin; font-size: 14px; font-weight: bold;">
                <asp:Label ID="lblMoveDestinationTitle" runat="server" Text=" Move Data"></asp:Label>
            </div>
            <div id="divMoveDestinationContent">
                <table width="100%">
                    <tr>
                        <td class="style1">Destination
                        </td>
                        <td class="style2">:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Selected="True" Value="0">=Choose Destination=</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1"></td>
                        <td class="style2"></td>
                        <td align="left">
                            <asp:LinkButton ID="lnkOk" runat="server">Ok</asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkCancel" runat="server">Cancel</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div id="BrandsGridView">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="90%"
        AllowSorting="True" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
        AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25"
        OnSorting="GridView1_Sorting" CellPadding="4" ForeColor="#333333" GridLines="None">
        <EditRowStyle BackColor="#999999" />
        <EmptyDataTemplate>
            No Data To Display !!
        </EmptyDataTemplate>
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                    <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID")%>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkRename" runat="server" Text="Rename" CommandName="RenameCategory"></asp:LinkButton>
                    <asp:LinkButton ID="lnkSave" runat="server" Text="Save" CommandName="SaveData" Visible="false"></asp:LinkButton>
                    <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="CancelData"
                        Visible="false"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkName" runat="server" Text='<%#Eval("Name")%>' CommandName="ViewSubCategories"></asp:LinkButton>
                    <asp:TextBox ID="txtName" runat="server" Text='<%#Eval("Name")%>' Visible="false"></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Listed Activities" SortExpression="Activities">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkActivities" runat="server" Text='<%#Eval("Activities")%>'></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created By" SortExpression="CreatedBy">
                <ItemTemplate>
                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created Date" SortExpression="CreatedDate">
                <ItemTemplate>
                    <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDateTime","{0:d}")%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Modified By" SortExpression="ModifiedBy">
                <ItemTemplate>
                    <asp:Label ID="lblModifiedBy" runat="server" Text='<%#Eval("ModifiedBy")%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Modified Date" SortExpression="ModifiedDate">
                <ItemTemplate>
                    <asp:Label ID="lblModifiedDate" runat="server" Text='<%#Eval("ModifiedDateTime")%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Center" />
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
</div>
<asp:Button ID="btnMove1" runat="server" Style="display: none" OnClick="btnMove1_Click"></asp:Button>
<asp:Button ID="btnCancelPopUp" runat="server" Style="display: none" OnClick="btnCancelPopUp_Click"></asp:Button>
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
<asp:HiddenField ID="hdnCurrentCategoryID" runat="server" />
<div>
    <asp:HiddenField ID="hdnSortParameter" runat="server" />
</div>
