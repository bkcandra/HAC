<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuburbSetupUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.SuburbSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div class="grid_2">
    <div class="box sidemenu" style="height: 800px">
        <div class="block" id="section-menu">
            <ul class="section menu">
                <li><a class="menuitem">Suburb Setup</a>
                    <ul class="submenu">
                        <li><a href="javascript:history.back()">Back </a></li>
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="AddEditContent">

                        <table>
                            <tr>
                                <td>Name
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddEditName" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblAddEditName" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Post Code
                                </td>
                                <td>:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPostcode" runat="server"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="txtPostcode_MaskedEditExtender" runat="server"
                                        TargetControlID="txtPostcode" MaskType="Number" Mask="9999">
                                    </asp:MaskedEditExtender>
                                    <asp:Label ID="lblPostcode" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Council</td>
                                <td>&nbsp;</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCouncil" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCouncil_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblCouncil" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>State
                                </td>
                                <td>:
                                </td>
                                <td align="left">
                                    &nbsp;
                                    <asp:HiddenField ID="hdnStateID" runat="server" />
                                    <asp:Label ID="lblState" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td align="left">
                                    <asp:Label ID="lblerror" runat="server" Style="color: #FF0000" Text="Incorrect suburb, please try again" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td align="left">
                                    <asp:LinkButton ID="lnkOk1" runat="server" OnClick="lnkOk1_Click">Ok</asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="lnkEdit" runat="server" Visible="false" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                                    &nbsp;&nbsp;
                <asp:LinkButton ID="lnkCancel1" runat="server" OnClick="lnkCancel1_Click">Cancel</asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="lnkCreateAnother" runat="server" Visible="false" OnClick="lnkCreateAnother_Click">Create Another</asp:LinkButton>
                                    <asp:HiddenField ID="hdnSuburbID" runat="server" />
                                    <asp:HiddenField ID="hdnAddEditMode" runat="server" />
                                </td>
                            </tr>



                        </table>
                        <asp:LinkButton ID="lnkBackToList" runat="server" Visible="false" OnClick="lnkBackToList_Click">Back to category list</asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</div>


