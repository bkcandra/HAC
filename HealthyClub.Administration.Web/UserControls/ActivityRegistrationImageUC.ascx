<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityRegistrationImageUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.ActivityRegistrationImageUC" %>
<style>
    div.fileinputs {
        position: relative;
    }

    div.fakefile {
        position: absolute;
        top: 0px;
        left: 0px;
        z-index: 1;
    }

    input.file {
        position: relative;
        text-align: right;
        -moz-opacity: 0;
        filter: alpha(opacity: 0);
        opacity: 0;
        z-index: 2;
    }
</style>
<span class="labelTitle">Activity Image</span><br />
To upload photos such as your organisation or activity logo, please click &quot;browse.&quot;&nbsp;
Once you have selected your image, click &quot;Upload.&quot; You can upload multiple
photos at once.<br />
<hr /><asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div style="width: 100%">
    <div id="Upload" style="border: thin solid #C0C0C0; text-align: center; width: 500px; height: 120px; margin: 20px auto;">
        <div style="text-align: left; background-color: #E0E0E0; padding: 5px">
            <asp:Label ID="Label2" runat="server" Text="Image Uploader"></asp:Label>
        </div>
        <br />
        <asp:FileUpload ID="fileUpload1" multiple="true" runat="server" />&nbsp;
        <asp:Button ID="imgBtnUpload" runat="server" Text="Upload" OnClick="imgBtnUpload_Click"
            Height="24px" Width="75px" />&nbsp;&nbsp;
        <br />
        <asp:Label ID="lblUploadStatus" runat="server" Text="Image uploaded successfully."
            Style="margin-top: 5px;" Visible="false"></asp:Label>
    </div>
</div>
<br />

        <div id="divUploadSuccessfull" visible="false" runat="server">
            <b>Please view the below image uploaded</b><br />
            <asp:DataList ID="dtImageLib" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                OnItemCommand="dtImageLib_ItemCommand" OnItemDataBound="dtImageLib_ItemDataBound">
                <ItemTemplate>
                    <div style="margin: 10px; text-align: center">
                        <asp:Image ID="imgUpload" runat="server" Width="100px" Height="100px" multiple="true" /><br />
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Filename").ToString().Length > 20 ? Eval("Filename").ToString().Substring(0,20) : Eval("Filename") %>'></asp:Label>
                        <br />
                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %>' />
                        <asp:HiddenField ID="hdnImageStream" Value='<%#Eval("ActivityImageID") %>' runat="server" />
                        <asp:HiddenField ID="hdnImageSize" runat="server" Value='<%#Eval("Filesize") %>' />
                        <asp:LinkButton ID="lnkRemoveImage" runat="server" CommandName="RemoveImage">Remove</asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <asp:HiddenField ID="hdnActionCode" runat="server" />
            <asp:HiddenField ID="hdnProviderID" runat="server" />
            <asp:HiddenField ID="hdnImgUploaded" runat="server" />
            <asp:HiddenField ID="hdnSizeUploaded" runat="server" />
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imgBtnUpload" />
    </Triggers>
</asp:UpdatePanel>
