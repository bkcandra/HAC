<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CouncilSetupUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.CouncilSetupUC" %>
<div class="grid_2">
    <div class="box sidemenu" style="height: 800px">
        <div class="block" id="section-menu">
            <ul class="section menu">
                <li><a class="menuitem">Council Setup</a>
                    <ul class="submenu">
                        <li><a>Back</a></li>

                    </ul>
                </li>

            </ul>
        </div>
    </div>
</div>
<div class="grid_10">
    <div class="box sidebox">
        <h2>
            <asp:Label ID="lblAddEditTitle" runat="server"></asp:Label></h2>
        <div class="block">
            <div id="AddEditContent">
                <table>
                    <tr>
                        <td>Council</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtAddEditName" runat="server"></asp:TextBox>
                            <asp:Label ID="lblAddEditName" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Description
                        </td>
                        <td>&nbsp;</td>
                        <td align="left">
                            <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
                            <asp:Label ID="lblDesc" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>State</td>
                        <td>&nbsp;</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList>
                            <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td align="left">
                            <asp:Label ID="lblerror" runat="server" Style="color: #FF0000" Text="Incorrect state, please try again" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td align="left">
                            <asp:LinkButton ID="lnkOk1" runat="server" OnClick="lnkOk1_Click">Ok</asp:LinkButton>
                            <asp:LinkButton ID="lnkEdit" runat="server" Visible="false" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                            &nbsp;&nbsp;
                <asp:LinkButton ID="lnkCancel1" runat="server" OnClick="lnkCancel1_Click">Cancel</asp:LinkButton>
                            <asp:LinkButton ID="lnkCreateAnother" runat="server" Visible="false" OnClick="lnkCreateAnother_Click">Create Another</asp:LinkButton>
                            <asp:HiddenField ID="hdnCouncilID" runat="server" />
                            <asp:HiddenField ID="hdnCreatedDate" runat="server" />
                            <asp:HiddenField ID="hdnCreatedBy" runat="server" />
                            <asp:HiddenField ID="hdnAddEditMode" runat="server" />
                        </td>
                    </tr>


                </table>
                <asp:LinkButton ID="lnkBackToList" runat="server" Visible="false" OnClick="lnkBackToList_Click">Back to council list</asp:LinkButton>
            </div>

        </div>
    </div>
</div>
