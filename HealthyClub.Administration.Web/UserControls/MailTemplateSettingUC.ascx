<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailTemplateSettingUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.MailTemplateSettingUC" %>
<style>
    .limitOptions {
        padding: 5px;
        border: 1px solid #b7b7b7;
    }

    .left {
        float: left;
        width: 450px;
    }

    .right {
        float: right;
        width: 300px;
    }

    .Cleaner {
        clear: both;
        padding: 5px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>


        <div>
            <fieldset>
                <legend><strong>Email Sender Setting</strong></legend>
                <div style="padding: 10px">
                    <div class="left">
                        <strong>Welcome Email</strong>
                    </div>
                    <div class="left">
                        <asp:Label ID="lblSc1" runat="server" Text="This Email is a welcome email and will be sent after user register to healthy boroondara club"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:DropDownList ID="ddEmailTemplate1" runat="server" class="limitOptions" Style="float: right;">
                            <asp:ListItem Value="0">Select Template</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="Cleaner">
                    </div>
                    <div class="left">
                        <strong>Welcome Email for Provider</strong>
                    </div>
                    <div class="left">
                        <asp:Label ID="lblSc3" runat="server" Text="This Email is a welcome email and will be sent when user registered to healthy
                    boroondara club"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:DropDownList ID="ddEmailTemplate3" runat="server" class="limitOptions" Style="float: right">
                            <asp:ListItem Value="0">Select Template</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="Cleaner">
                    </div>
                    <div class="left">
                        <strong>Confirmation Email</strong>
                    </div>
                    <div class="left">
                        <asp:Label ID="lblSc2" runat="server" Text="This Email will be sent after user register to healthy boroondara club to ask them to confirm their email address"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:DropDownList ID="ddEmailTemplate2" runat="server" class="limitOptions" Style="float: right">
                            <asp:ListItem Value="0">Select Template</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="Cleaner">
                    </div>
                    <div class="left">
                        <strong>ProviderConfirmation Email</strong>
                    </div>
                    <div class="left">
                        <asp:Label ID="lblSc8" runat="server" Text="This Email will be sent after Provider register to healthy boroondara club to ask them to confirm their email address"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:DropDownList ID="ddEmailTemplate8" runat="server" class="limitOptions" Style="float: right">
                            <asp:ListItem Value="0">Select Template</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="Cleaner">
                    </div>
                    <div class="left">
                        <strong>Forgot Password</strong>
                    </div>
                    <div class="left">
                        <asp:Label ID="lblSc4" runat="server" Text="This Email will be sent when user trying to ask their old password"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:DropDownList ID="ddEmailTemplate4" runat="server" class="limitOptions" Style="float: right">
                            <asp:ListItem Value="0">Select Template</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="Cleaner">
                    </div>
                    <div class="left">
                        <strong>Expiring Notification 1</strong>
                    </div>
                    <div class="left">
                        <asp:Label ID="lblSc5" runat="server" Text="This Email will be sent 2 weeks before activity is expired"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:DropDownList ID="ddEmailTemplate5" runat="server" class="limitOptions" Style="float: right">
                            <asp:ListItem Value="0">Select Template</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="Cleaner">
                    </div>
                    <div class="left">
                        <strong>Expiring Notification 2</strong>
                    </div>
                    <div class="left">
                        <asp:Label ID="lblSc6" runat="server" Text="This Email will be sent 1 week before activity is expired"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:DropDownList ID="ddEmailTemplate6" runat="server" class="limitOptions" Style="float: right">
                            <asp:ListItem Value="0">Select Template</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="Cleaner">
                    </div>
                    <div class="left">
                        <strong>Expired Notification</strong>
                    </div>
                    <div class="left">
                        <asp:Label ID="lblSc7" runat="server" Text="This Email will be sent when activity is expired"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:DropDownList ID="ddEmailTemplate7" runat="server" class="limitOptions" Style="float: right">
                            <asp:ListItem Value="0">Select Template</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="Cleaner">
                    </div>
                    <div class="left" style="text-align: center">
                        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                    </div>
                    <div class="right">
                        <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click">Save</asp:LinkButton>
                        &nbsp;
                <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                        &nbsp;
                <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                        <asp:HiddenField ID="hdnMode" runat="server" />
                    </div>
                </div>
            </fieldset>
        </div>
    </ContentTemplate>

</asp:UpdatePanel>
