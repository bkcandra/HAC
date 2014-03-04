<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityRegistrationCategory.ascx.cs" Inherits="HealthyClub.Provider.Web.UserControls.ActivityRegistrationCategory" %>
<style>
     ul { padding: 0px; margin: 0 0 0 18px; }
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div id="divRootCat" runat="server" style="float: left; margin: 5px; padding-right: 10px;">
            Main Category<ul>
                <asp:ListView ID="listViewCatRoot" runat="server" OnItemCommand="listViewCatRoot_ItemCommand" ClientIDMode="AutoID" OnItemDataBound="listViewCatRoot_ItemDataBound">

                    <ItemTemplate>
                        <div id="divCat" runat="server">


                            <li>
                                <asp:LinkButton ID="lnkCategoryName" runat="server" Text='<%#Eval("Name") %>' CommandName="CheckLvl1Cat"></asp:LinkButton>
                                <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%#Eval("ID") %>' />
                            </li>


                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </ul>
        </div>
        <div id="divSecCat" runat="server" style="float: left; margin: 15px; padding-left: 15px; padding-right: 15px; border-left: 1px solid black">
            Sub Category
             <asp:ListView ID="listViewCatLvl1" runat="server" OnItemCommand="listViewCatLvl1_ItemCommand" ClientIDMode="AutoID" OnItemDataBound="listViewCatLvl1_ItemDataBound">
                 <ItemTemplate>
                     <div>
                         <asp:CheckBox ID="chkLvl1IsChecked" runat="server" OnCheckedChanged="chkIsLvl1Checked_CheckedChanged" />&nbsp;&nbsp;&nbsp;
                        <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%#Eval("ID") %>' />
                         <asp:Label ID="lnkCategoryName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                     </div>
                 </ItemTemplate>
             </asp:ListView>
        </div>
        <div id="divL2SecCat" runat="server" style="float: left; margin: 5px; padding-left: 10px; padding-right: 10px; border-right: 1px solid black">
            &nbsp;Sub Category
            <asp:ListView ID="listViewCatLvl2" runat="server" OnItemCommand="listViewCatLvl2_ItemCommand" ClientIDMode="AutoID" OnItemDataBound="listViewCatLvl2_ItemDataBound">
                <ItemTemplate>
                    <div>
                        <asp:CheckBox ID="chkLvl2IsChecked" runat="server" />&nbsp;&nbsp;&nbsp;
                        <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%#Eval("ID") %>' />
                        <asp:Label ID="lnkCategoryName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div style="clear: both">
            <asp:Label ID="lblError" runat="server" Text="Error" Visible="false" ForeColor="Red" Style="margin: 10px;"></asp:Label>
        </div>
        <div>
            <div style="float: left; width: 250px;">
                <span class="buttonAdd">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Content/StyleImages/add_limit.png"
                        CausesValidation="false" OnClick="btnAddCat_Click" Style="padding-top: 5px"></asp:ImageButton>
                    <span>
                        <asp:LinkButton ID="lblAddCat" runat="server" OnClick="btnAddCat_Click">Add Category</asp:LinkButton></span>
                </span>
            </div>
            <div style="float: left; margin: 5px; padding-left: 50px; vertical-align: bottom; position: relative; bottom: 0">
                <asp:ListView ID="ListviewSelectedCat" runat="server" OnItemCommand="ListviewSelectedCat_ItemCommand" ClientIDMode="AutoID">
                    <EmptyItemTemplate>
                        <div>Please select a category for your activity</div>
                    </EmptyItemTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:LinkButton ID="lnkRemoveCategory" runat="server" CommandName="RemoveCat">Remove</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Text") %>'></asp:Label>
                            <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%#Eval("Value") %>' />
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <div style="clear: both"></div>
        <asp:HiddenField ID="hdnEdit" runat="server" />
        <asp:HiddenField ID="hdnActivityID" runat="server" />
        <asp:HiddenField ID="hdnSelectedCat2" runat="server" />
        <asp:HiddenField ID="hdnSelectedCat3" runat="server" />
        <asp:HiddenField ID="hdnSelectedCat1" runat="server" />
        <asp:HiddenField ID="hdnSelectedCount" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>








