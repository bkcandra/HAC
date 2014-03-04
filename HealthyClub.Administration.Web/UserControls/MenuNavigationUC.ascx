<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuNavigationUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.MenuNavigationUC" %>
<%@ Register Src="~/UserControls/MenuItemTreeView.ascx" TagPrefix="uc2" TagName="MenuItemTreeView" %>
<style type="text/css">
    .style1 {
        width: 32px;
        height: 27px;
    }
</style>
<div class="grid_10">
    <div class="box sidebox" style="border:1px solid black">
        <h2>Club Member</h2>
        <div class="block">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="height: 255px; width: 849px">
                        <tr>
                            <td align="left" style="width: 300px" valign="top">
                                <uc2:MenuItemTreeView ID="TreeViewCustomer" runat="server" OnTreeNodeClicked="MenuTreeView1_OnTreeNodeClicked" />

                            </td>
                            <td align="left">
                                <div id="divAddEditpopUp" runat="server" style="border: thin ridge #000000; background-color: #FFFFFF">
                                    <div id="divAddEditTitle" style="font-size: 14px; font-weight: bold; vertical-align: middle">
                                        &nbsp;<asp:Label ID="lblAddEditTitle" runat="server" Text="Member Navigation"></asp:Label>
                                        &nbsp;&nbsp;
                                    </div>
                                    <div style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td class="style7" style="border-width: thin; border-color: #000000; border-right-style: solid; border-bottom-style: solid;">
                                                    <asp:LinkButton ID="lnkAddNewMenu" runat="server" CssClass="HeaderLink" OnClick="lnkAddNewMenu_Click">Add New</asp:LinkButton>
                                                    &nbsp; &nbsp;
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="HeaderLink" OnClick="lnkDelete_Click"
                                                        OnClientClick="return confirm('This action will DELETE SELECTED MENU ITEM AND ITS CHILD, Are You Sure ?');">Delete</asp:LinkButton>
                                                </td>
                                                <!--
                                    <td style="border-bottom-style: solid; border-width: thin; border-color: #000000;
                                        border-left-style: solid;">
                                        <asp:ImageButton ID="imgUp" runat="server" ImageUrl="~/Styles/appIcon/up-arrow.jpg"
                                            OnClick="imgUp_Click" Style="max-height: 20px; max-width: 20px" Visible="False" />
                                        &nbsp;
                                        <asp:ImageButton ID="imgDown" runat="server" CommandName="SortDown" ImageUrl="~/Styles/appIcon/down-arrow.jpg"
                                            OnClick="imgDown_Click" Style="max-height: 20px; max-width: 20px" Visible="False" />
                                        &nbsp;
                                        
                                    </td>-->
                                                <asp:Label ID="lblWarning" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"
                                                    Style="float: right; margin-right: 10px"></asp:Label>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="AddEditContent">
                                        <table>
                                            <tr>
                                                <td class="style13">Parent Menu
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:DropDownList ID="ddParentMenu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddParentMenu_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13">Text
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:TextBox ID="txtAddEditDescription" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblTextErr" runat="server" ForeColor="Red" Text="Menu Text Must Not Empty"
                                                        Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13">Menu Level
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:Label ID="lblmenuLvl" runat="server" Text="Root"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13">Target Type
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:DropDownList ID="ddTargetType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTargetType_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0">=Choose one=</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13">Target
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:TextBox ID="txtTarget" runat="server" Width="400px" Visible="false"></asp:TextBox>
                                                    <asp:DropDownList ID="ddTarget" runat="server" Width="400px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13"></td>
                                                <td></td>
                                                <td class="style12">&nbsp;<asp:LinkButton ID="lnkEdit" runat="server" Visible="False" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                                                    &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkOk" runat="server" OnClick="lnkOk_Click" Visible="False">Save</asp:LinkButton>
                                                    &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click" Visible="False">Cancel</asp:LinkButton>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="AddEditCommand">
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <%-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkOk" runat="server">Ok</asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkCancel" runat="server">Cancel</asp:LinkButton>--%>
                                                    <asp:HiddenField ID="hdAddEditMode" runat="server" />
                                                    <asp:HiddenField ID="hdnPopUpMenuItemID" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

    </div>
</div>
<div class="grid_10">
    <div class="box sidebox" style="border:1px solid #E6F0F3">
        <h2>Activity Provider</h2>
        <div class="block">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table style="height: 255px; width: 849px">
                        <tr>
                            <td align="left" style="width: 300px" valign="top">
                                <uc2:MenuItemTreeView ID="TreeViewProvider" runat="server" OnTreeNodeClicked="TreeViewProvider_OnTreeNodeClicked" />
                            </td>
                            <td align="left">
                                <div id="div1" runat="server" style="border: thin ridge #000000; background-color: #FFFFFF">
                                    <div id="div2" style="font-size: 14px; font-weight: bold; vertical-align: middle">
                                        &nbsp;<asp:Label ID="lblAddEditTitleProvider" runat="server" Text="Administrator Navigation"></asp:Label>
                                        &nbsp;&nbsp;
                                    </div>
                                    <div style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td class="style7" style="border-width: thin; border-color: #000000; border-right-style: solid; border-bottom-style: solid;">
                                                    <asp:LinkButton ID="lnkAddNewMenuProvider" runat="server" CssClass="HeaderLink" OnClick="lnkAddNewMenuProvider_Click">Add New</asp:LinkButton>
                                                    &nbsp; &nbsp;
                                                    <asp:LinkButton ID="lnkDeleteProvider" runat="server" CssClass="HeaderLink" OnClick="lnkDeleteProvider_Click"
                                                        OnClientClick="return confirm('This action will DELETE SELECTED MENU ITEM AND ITS CHILD, Are You Sure ?');">Delete</asp:LinkButton>
                                                </td>
                                                <!--
                                    <td style="border-bottom-style: solid; border-width: thin; border-color: #000000;
                                        border-left-style: solid;">
                                        <asp:ImageButton ID="imgUpProv" runat="server" ImageUrl="~/Styles/appIcon/up-arrow.jpg"
                                            OnClick="imgUpProv_Click" Style="max-height: 20px; max-width: 20px" Visible="False" />
                                        &nbsp;
                                        <asp:ImageButton ID="imgDownProv" runat="server" CommandName="SortDown" ImageUrl="~/Styles/appIcon/down-arrow.jpg"
                                            OnClick="imgDownProv_Click" Style="max-height: 20px; max-width: 20px" Visible="False" />
                                        &nbsp;
                                        
                                    </td>-->
                                                <asp:Label ID="lblWarningProvider" runat="server" Font-Bold="True" ForeColor="Red"
                                                    Visible="False" Style="float: right; margin-right: 10px"></asp:Label>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="Div3">
                                        <table>
                                            <tr>
                                                <td class="style13">Parent Menu
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:DropDownList ID="ddProviderMenu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProviderMenu_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13">Text
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:TextBox ID="txtAddEditDescriptionProvider" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblTextErrProvider" runat="server" ForeColor="Red" Text="Menu Text Must Not Empty"
                                                        Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13">Menu Level
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:Label ID="lblmenuLvlProvider" runat="server" Text="Root"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13">Target Type
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:DropDownList ID="ddTargetTypeProvider" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTargetTypeProvider_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0">=Choose one=</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13">Target
                                                </td>
                                                <td>:
                                                </td>
                                                <td class="style12">
                                                    <asp:TextBox ID="txtTargetProvider" runat="server" Width="400px" Visible="false"></asp:TextBox>
                                                    <asp:DropDownList ID="ddTargetProvider" runat="server" Width="400px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style13"></td>
                                                <td></td>
                                                <td class="style12">&nbsp;<asp:LinkButton ID="lnkEditProvider" runat="server" Visible="False" OnClick="lnkEditProvider_Click">Edit</asp:LinkButton>
                                                    &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkOkProvider" runat="server" OnClick="lnkOkProvider_Click" Visible="False">Save</asp:LinkButton>
                                                    &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkCancelProvider" runat="server" OnClick="lnkCancelProvider_Click"
                                                        Visible="False">Cancel</asp:LinkButton>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="Div4">
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <%-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkOk" runat="server">Ok</asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkCancel" runat="server">Cancel</asp:LinkButton>--%>
                                                    <asp:HiddenField ID="hdAddEditModeProvider" runat="server" />
                                                    <asp:HiddenField ID="hdnPopUpMenuItemIDProvider" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<asp:HiddenField ID="hdnModeMember" runat="server" />
<asp:HiddenField ID="hdnModeProvider" runat="server" />
