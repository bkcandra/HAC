<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityImageUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.ActivityImageUC" %>
<%@ Register Src="ActivityImageListView.ascx" TagName="ActivityImageListView" TagPrefix="uc1" %>
<link href="../Content/prettyPhoto.css" rel="stylesheet" />
<script src="../Scripts/pretty-photo/jquery.prettyPhoto.js"></script>
<style type="text/css" media="screen">
    * {
        margin: 0;
        padding: 0;
    }

    h1 {
        font-family: Georgia;
        font-style: italic;
        margin-bottom: 10px;
    }

    h2 {
        font-family: Georgia;
        font-style: italic;
        margin: 25px 0 5px 0;
    }

    p {
        font-size: 1.2em;
    }

    ul li {
        display: inline;
    }

    .wide {
        border-bottom: 1px #000 solid;
        width: 4000px;
    }

    .fleft {
        float: left;
        margin: 0 20px 0 0;
    }

    .cboth {
        clear: both;
    }

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
<asp:HiddenField ID="hdnActivityID" runat="server" />
<div style="width 100%">
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
<hr />
<div id="divUploadSuccessfull" visible="false" runat="server">
    <fieldset class="changePassword">
        <legend style="width: 210px">
            <asp:Label ID="Label3" runat="server" Text="Image Title & Description"></asp:Label>
        </legend>
        <uc1:ActivityImageListView ID="ActivityImageListView1" runat="server" />
    </fieldset>
</div>
<asp:HiddenField ID="hdnImgUploaded" runat="server" />
<asp:HiddenField ID="hdnSizeUploaded" runat="server" />
<asp:HiddenField ID="hdnActionCode" runat="server" />
