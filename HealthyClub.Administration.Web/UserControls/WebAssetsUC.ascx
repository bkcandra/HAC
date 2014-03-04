<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebAssetsUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.WebAssetsUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style>
    .asset_box
    {
        float: left;
        width: 220px;
        text-align: center;
        margin: 0 10px 30px 0;
        padding-bottom: 20px;
        border-bottom: 1px dotted #ccc;
    }
    
    .asset_box img
    {
        margin-bottom: 5px;
    }

    .asset_box h3
    {
        font-size: 11px;
        color: #000;
        font-weight: 700;
        margin-bottom: 10px;
    }

    .asset_box .asset_url
    {
        color: #11bdd1;
        font-size: 9px;
        font-weight: 700;
        margin-bottom: 20px;
    }

    .asset_box .deleteAsset
    {
        float: left;
        display: block;
    }

    .asset_box .detail
    {
        float: right;
        display: block;
    }
    
    a, a:link, a:visited
    {
        font-weight: normal;
        text-decoration: none;
    }

    a:hover
    {
        text-decoration: underline;
    }
    
    a.deleteAsset
    {
        display: inline-block;
        width: 80px;
        height: 21px;
        line-height: 21px;
        text-align: center;
        font-size: 10px;
        font-weight: bold;
        color: #333;
        background: url(../Content/StyleImages/delete.png) no-repeat;
    }

    a.detail
    {
        display: inline-block;
        width: 64px;
        height: 20px;
        line-height: 20px;
        text-align: center;
        font-size: 10px;
        font-weight: bold;
        color: #333;
        background: url(../Content/StyleImages/detail.png) no-repeat;
    }
    .style5
    {
        width: 175px;
    }
    .style6
    {
        width: 4px;
    }
</style>
<div style="width:100%">
    <div id="Upload" style="border: thin solid #C0C0C0; text-align: center; width: 500px;
        height: 120px; margin: 20px auto;">
        <div style="text-align: left; background-color: #E0E0E0; padding: 5px">
            <asp:Label ID="Label2" runat="server" Text="Image Uploader"></asp:Label>
        </div>
        <br />
        <asp:FileUpload ID="fileUpload1" multiple="true" runat="server" />&nbsp;
        <asp:Button ID="imgBtnUpload" runat="server" Text="Upload" OnClick="imgBtnUpload_Click"
            Height="35px" Width="75px" style="position:relative; top:-1px" />&nbsp;&nbsp;
        <br />
        <asp:Label ID="lblUploadStatus" runat="server" Text="Image uploaded successfully."
            Style="margin-top: 5px;" Visible="false"></asp:Label></div>
</div>
<hr />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="text-align: center">
            <asp:ListView ID="ListViewAsset" runat="server" DataKeyNames="ID" GroupItemCount="3"
                OnItemCommand="ListViewAsset_ItemCommand" OnItemDataBound="ListViewAsset_ItemDataBound">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
                </LayoutTemplate>
                <GroupTemplate>
                    <table>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </GroupTemplate>
                <EmptyDataTemplate>
                    <p>
                        No File(s) found.</p>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <td>
                        <div class="asset_box">
                            <h3>
                                <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Filetitle") %>'></asp:Label>
                            </h3>
                            <asp:HiddenField ID="hdnAssetID" runat="server" Value='<%#Eval("ID") %>' />
                            <asp:HiddenField ID="hdnFilename" runat="server" Value='<%#Eval("Filename") %>' />
                            <asp:HiddenField ID="hdnFileext" runat="server" Value='<%#Eval("FileType") %>' />
                            <asp:HiddenField ID="hdnFilesize" runat="server" Value='<%#Eval("FileSize") %>' />
                            <asp:HyperLink ID="hlnkProduct" runat="server">
                                <asp:Image ID="imgAsset" runat="server" Width="100px" Height="100px" />
                            </asp:HyperLink>
                            <p class="asset_url">
                               <asp:Label ID="lblUrl" runat="server"></asp:Label>
                            </p>
                            <asp:LinkButton ID="lnkDelete" runat="server" class="deleteAsset" CommandName="DeleteAsset"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1" runat="server" class="detail" CommandName="viewAssetDetail"></asp:LinkButton>
                        </div>
                    </td>
                </ItemTemplate>
            </asp:ListView>
            <asp:HiddenField ID="hdnImgUploaded" runat="server" />
            <asp:HiddenField ID="hdnSizeUploaded" runat="server" />
            <asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
        </div>
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnMove"
            PopupControlID="divPopMoveDestination" CancelControlID="lnkCancel" OkControlID="lnkOk"
            OnOkScript="Ok()">
        </asp:ModalPopupExtender>
        <asp:Button ID="btnMove" runat="server" Style="display: none">
        </asp:Button>
        <div id="divPopMoveDestination" runat="server" style="border: medium ridge #000000;
            background-color: #FFFFFF; width: 450px;">
            <div id="divMoveDestinationTitle" style="border-color: #000000; width: 100%; border-bottom-style: solid;
                border-bottom-width: thin; font-size: 14px; font-weight: bold;">
                <asp:Label ID="lblAssetInformation" runat="server" Text="Asset Information"></asp:Label>
            </div>
            <div id="divMoveDestinationContent">
                <table style="width:100%">
                    <tr>
                        <td class="style5">
                            Filename
                        </td>
                        <td class="style6">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            File extension \ type
                        </td>
                        <td class="style6">
                        </td>
                        <td>
                            <asp:Label ID="lblExtension" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            File size
                        </td>
                        <td class="style6">
                        </td>
                        <td>
                            <asp:Label ID="lblFilesize" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            Url
                        </td>
                        <td class="style6">
                        </td>
                        <td align="left">
                            <asp:Label ID="lblUrl" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            &nbsp;
                        </td>
                        <td class="style6">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:LinkButton ID="lnkOk" runat="server">Ok</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkCancel" runat="server"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
