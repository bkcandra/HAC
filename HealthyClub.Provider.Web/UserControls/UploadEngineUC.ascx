<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadEngineUC.ascx.cs" Inherits="HealthyClub.Provider.Web.UserControls.UploadEngineUC" %>
<script type="text/javascript">
    function pageLoad(sender, args) {
        //Register the form and upload elements
        window.parent.register(
            $get('<%= this.ClientID %>'),
                $get('<%= this.fileUpload.ClientID %>')
            );
    }
    </script>
    <asp:FileUpload ID="fileUpload" runat="server" />