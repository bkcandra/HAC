<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailTemplateDetailUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.MailTemplateDetailUC" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<style>
    .style4 {
        font-size: 11px;
        color: #666699;
    }
</style>
<div class="grid_10">
    <div class="box sidebox">
        <h2>
            <asp:Label ID="lblPageSetup" runat="server" Text="Label"></asp:Label></h2>
        <div class="block">
            <asp:HiddenField ID="hdnEditMode" runat="server" />
            <table width="90%">
                <tr>
                    <td>Name&nbsp;
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                        <asp:TextBox ID="txtName" runat="server" Height="22px" Width="240px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Subject
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="lblSubject" runat="server"></asp:Label>
                        <asp:TextBox ID="txtSubject" runat="server" Height="22px" Width="240px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trDescription" runat="server">
                    <td style="vertical-align: top">Mail CC<br />
                        <span class="style4">Please enter email adresses separated by semicolon ( ; ).</span>
                    </td>
                    <td style="vertical-align: top">:
                    </td>
                    <td style="vertical-align: top">
                        <asp:Label ID="lblCC" runat="server"></asp:Label>
                        <asp:TextBox ID="txtCC" runat="server" Height="100px" TextMode="MultiLine" Width="320px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div id="divPageEditor" runat="server" visible="false">
                <h3>Page Editor</h3>
                <hr />
            </div>
            <CKEditor:CKEditorControl ID="CKEditorControl1" BasePath="~/Scripts/ckeditor/" runat="server">
            </CKEditor:CKEditorControl>
            <table>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                        &nbsp;&nbsp;
            <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click">Save</asp:LinkButton>
                        &nbsp;&nbsp;
            <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                        &nbsp;
                    </td>
                </tr>
            </table>

        </div>
    </div>
</div>
<div id="divContentPreviewHead" runat="server" visible="false">
    <div class="grid_10">
        <div class="box sidebox">
            <h2>Template Preview</h2>



            <div id="divContentPreview" runat="server" visible="false">
            </div>
            <asp:HiddenField ID="hdnMode" runat="server" />
            <asp:HiddenField ID="hdnTemplateID" runat="server" />
            <asp:HiddenField ID="hdnEmailType" runat="server" />
        </div>
    </div>
</div>
