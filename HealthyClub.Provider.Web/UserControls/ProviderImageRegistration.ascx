<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderImageRegistration.ascx.cs" Inherits="HealthyClub.Provider.Web.UserControls.ProviderImageRegistration" %>
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

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div id="Upload" style="border: thin solid #C0C0C0; text-align: center; width: 500px;">
            <div style="text-align: left; background-color: #E0E0E0; padding: 3px;">
                <asp:Label ID="lblName" runat="server" Text="Image Uploader"></asp:Label>
            </div>
            <span style="color: red">Files accepted: .Jpeg, .png, .gif</span><br />
            &nbsp;<asp:FileUpload ID="fileUpload1" runat="server" Height="25px" />&nbsp;
        <asp:Button ID="imgBtnUpload" runat="server" Text="Upload" OnClick="imgBtnUpload_Click" CausesValidation="false"
            Height="25px" Width="75px" />&nbsp;&nbsp;
        <br />
            <asp:Label ID="lblUploadStatus" runat="server" Text="Image uploaded successfully."
                Style="margin-top: 5px; font-weight: bold" Visible="false"></asp:Label>

            <div id="divUploadSuccessfull" visible="false" runat="server" style="padding: 5px; text-align: left">
                <br />
                <strong>
                    <asp:Label ID="UploadedTitle" runat="server" Text="" Visible="false"></asp:Label></strong>
                <hr />
                <asp:Label ID="lblimageTitle" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Image ID="ProviderImagePreview" runat="server" Style="height: 120px;" />

                <asp:HiddenField ID="hdnActionCode" runat="server" />
                <asp:HiddenField ID="hdnProviderID" runat="server" />
                <asp:HiddenField ID="hdnImgUploaded" runat="server" />
                <asp:HiddenField ID="hdnSizeUploaded" runat="server" />
                <asp:HiddenField ID="hdnisSupported" runat="server" />
                <asp:HiddenField ID="hdnFileName" runat="server" />
                <asp:HiddenField ID="hdnImageStream" runat="server" />
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imgBtnUpload" />
    </Triggers>
</asp:UpdatePanel>
