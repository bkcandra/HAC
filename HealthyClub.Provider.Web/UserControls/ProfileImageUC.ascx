<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileImageUC.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ProfileImageUC" %>

<span class="MasterTitle" style="margin-top: 10px;">Organisation logo</span>
<br />
<div style="width: 100%">
    <div id="Upload" style="border: thin solid #C0C0C0; text-align: center; width: 500px; height: 120px;">
        <div style="text-align: left; background-color: #E0E0E0; padding: 5px">
            <asp:Label ID="Label2" runat="server" Text="Choose image"></asp:Label>
        </div>
        <asp:Label ID="lblWarningUpload" runat="server" Style="color: #FF0000; background-color: #FFFFFF"
            Visible="false"></asp:Label>
        <br />
        <asp:FileUpload ID="FileUpload1" multiple="true" runat="server" />&nbsp;
        <asp:Button ID="imgBtnUpload" runat="server" Text="Upload" OnClick="imgBtnUpload_Click"
            Height="24px" Width="75px" />&nbsp;&nbsp;
        <br />
        <asp:Label ID="lblImageName" runat="server" Text="Image uploaded successfully."
            Style="margin-top: 5px;" Visible="false"></asp:Label>
    </div>
    <asp:HiddenField ID="hdnProviderID" runat="server" />
</div>
<hr />
<br />
<asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging"
    GroupItemCount="4" OnItemCommand="ListView1_ItemCommand"
    OnItemDataBound="ListView1_ItemDataBound">
    <LayoutTemplate>
        <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
    </LayoutTemplate>
    <GroupTemplate>
        <table width="100%">
            <tr id="itemPlaceholder" runat="server">
            </tr>
        </table>
    </GroupTemplate>
    <ItemTemplate>
        <td style="padding: 0px 5px">
            <table>
                <tr>
                    <td style="text-align: center">
                        <asp:Image ID="imgProfileImage" runat="server" Style="max-height: 75px; max-width: 75px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnImageID" runat="server" Value='<%#Eval("ID") %>' />
                        <asp:HiddenField ID="hdnMainImage" runat="server" Value='<%#Eval("IsPrimaryImage") %>' />
                        <asp:HiddenField ID="hdnImageName" runat="server" Value='<%#Eval("Filename") %>' />
                        <asp:HiddenField ID="hdnFilesize" runat="server" Value='<%#Eval("FileSize") %>' />
                        <asp:LinkButton ID="lnkDeleteImage" runat="server" CommandName="DeleteImage" OnClientClick="return confirm('Are you sure you want to delete this image?');">Remove</asp:LinkButton>
                        |&nbsp;<asp:LinkButton ID="lnkSetPrimary" CommandName="SetAsPrimaryImage" runat="server">Set as Primary</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </td>
    </ItemTemplate>
</asp:ListView>
<asp:HiddenField ID="hdnID" runat="server" />
<asp:HiddenField ID="hdnUserID" runat="server" />
<asp:HiddenField ID="hdnStorageUsed" runat="server" />
<asp:HiddenField ID="hdnFreeStorage" runat="server" />
<asp:HiddenField ID="hdnImageAmount" runat="server" />

