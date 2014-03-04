<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityImageListView.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ActivityImageListView" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<link href="../Styles/prettyPhoto.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/jquery.prettyPhoto.js" type="text/javascript"></script>

<asp:HiddenField ID="hdnActivityID" runat="server" />
<asp:DataList ID="DataList1" runat="server" CellPadding="4" ForeColor="#333333" RepeatDirection="Horizontal"
    OnItemCommand="DataList1_ItemCommand" OnItemDataBound="DataList1_ItemDataBound"
    Width="100%" RepeatColumns="1" ItemStyle-VerticalAlign="Bottom">
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td rowspan="2" style="width: 120px; vertical-align: top">
                    <asp:UpdatePanel ID="updatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HyperLink ID="hlnkImage" runat="server" rel="Fullscreen[gal]">
                                <asp:Image ID="imgThumbnail" runat="server" Style="max-height: 100px; max-width: 100px;
                                    margin-right: 30px" /></asp:HyperLink>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 100px; vertical-align: top">
                    Image Title
                </td>
                
                <td style="vertical-align: top">
                    <asp:Label ID="lblImageTitle" runat="server" Text='<%#Eval("ImageTitle") %>'></asp:Label>
                    <asp:TextBox ID="txtImageTitle" runat="server" Width="300px" Text='<%#Eval("ImageTitle") %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 200px; vertical-align: top">
                    Image Description
                </td>
                
                <td colspan="2">
                    <asp:Label ID="lblImageDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                    <asp:TextBox ID="txtImageDescription" runat="server" TextMode="MultiLine" Rows="3" Text='<%#Eval("Description") %>'
                        Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:HiddenField ID="hdnImageID" runat="server" Value='<%#Eval("ID") %>' />
                    <asp:HiddenField ID="hdnMainImage" runat="server" Value='<%#Eval("IsPrimaryImage") %>' />
                    <asp:HiddenField ID="hdnImageInfoID" runat="server" Value='<%#Eval("ActivityImageID") %>' />
                    <asp:HiddenField ID="hdnFilesize" runat="server" Value='<%#Eval("Filesize") %>' />
                    <asp:HiddenField ID="hdnFilename" runat="server" Value='<%#Eval("Filename") %>'/>
                    <asp:LinkButton ID="lnkDeleteImage" runat="server" CommandName="DeleteImage" OnClientClick="return confirm('This action will DELETE THE IMAGE SELECTED, Are You Sure ?');">Remove</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lnkMainImage" runat="server" CommandName="SetAsMainImage">Set as Primary Image</asp:LinkButton>&nbsp;
                </td>
            </tr>
        </table>
        <hr />
    </ItemTemplate>
</asp:DataList>

<asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
<asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click">Save</asp:LinkButton>&nbsp;&nbsp;
<asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>&nbsp;
<asp:HiddenField ID="hdnMode" runat="server" />
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        $("a[rel^='Fullscreen']").prettyPhoto({ animation_speed: 'normal' });
    });
</script>
