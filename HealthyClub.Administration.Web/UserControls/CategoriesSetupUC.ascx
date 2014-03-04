<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoriesSetupUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.CategoriesSetupUC" %>
<div id="AddEditContent">
    <h2>
        <asp:Label ID="lblAddEditTitle" runat="server"></asp:Label></h2>
    <hr />
    <br />
    <table>
        <tr>
            <td>
                Name
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox ID="txtAddEditName" runat="server"></asp:TextBox>
                <asp:Label ID="lblAddEditName" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Parent Categories
            </td>
            <td>
                :
            </td>
            <td>
                <asp:DropDownList ID="dropDownParent" runat="server">
                </asp:DropDownList>
                <asp:Label ID="lblParent" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td align="left">
                <asp:LinkButton ID="lnkOk1" runat="server" OnClick="lnkOk1_Click">Ok</asp:LinkButton>
                <asp:LinkButton ID="lnkEdit" runat="server" Visible="false" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="lnkCancel1" runat="server" OnClick="lnkCancel1_Click">Cancel</asp:LinkButton>
                <asp:LinkButton ID="lnkCreateAnother" runat="server" Visible="false" OnClick="lnkCreateAnother_Click">Create Another</asp:LinkButton>
                <asp:HiddenField ID="hdnCategoryID" runat="server" />
                <asp:HiddenField ID="hdnAddEditMode" runat="server" />
            </td>
        </tr>
    </table>
    <asp:LinkButton ID="lnkBackToList" runat="server" Visible="false" OnClick="lnkBackToList_Click">Back to category list</asp:LinkButton>
</div>