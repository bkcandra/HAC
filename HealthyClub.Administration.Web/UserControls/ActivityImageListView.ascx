<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityImageListView.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.ActivityImageListView" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<link href="../Styles/prettyPhoto.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/jquery.prettyPhoto.js" type="text/javascript"></script>
<style type="text/css" media="screen">
    *
    {
        margin: 0;
        padding: 0;
    }
    h1
    {
        font-family: Georgia;
        font-style: italic;
        margin-bottom: 10px;
    }
    h2
    {
        font-family: Georgia;
        font-style: italic;
        margin: 25px 0 5px 0;
    }
    p
    {
        font-size: 1.2em;
    }
    ul li
    {
        display: inline;
    }
    .wide
    {
        border-bottom: 1px #000 solid;
        width: 4000px;
    }
    .fleft
    {
        float: left;
        margin: 0 20px 0 0;
    }
    .cboth
    {
        clear: both;
    }

</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div id="divImageListview" runat="server">
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
                <td style="width: 5px; vertical-align: top">
                    :
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
                <td style="width: 5px; vertical-align: top">
                    :
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
<asp:HiddenField ID="hdnActivityID" runat="server" />
        </div>
        <div id="divNoImageListview" runat="server" visible="false">
            <span style="text-align:center"> No Images found!</span>
        </div>


    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        $("a[rel^='Fullscreen']").prettyPhoto({ animation_speed: 'normal' });
    });
</script>
