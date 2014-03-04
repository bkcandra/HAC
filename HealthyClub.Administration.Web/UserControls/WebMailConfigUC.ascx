<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebMailConfigUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.WebMailConfigUC" %>
<style type="text/css">
    .auto-style1 {
        width: 131px;
        vertical-align: top;
    }

    .auto-style2 {
        width: 764px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="padding: 10px; width: 700px">

            <asp:Panel ID="Panel1" runat="server">
                <table>
                    <tr>
                        <td class="auto-style1">Sender Account
                        </td>
                        <td>:
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtSender" runat="server" Width="200px" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="lblSender" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSender"
                                ErrorMessage="Sender Address Must Not be Empty"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Username
                        </td>
                        <td>:
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtUsername" runat="server" Width="200px" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsername"
                                ErrorMessage="UserName Must Not be Empty"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Password
                        </td>
                        <td>:
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px" MaxLength="255"></asp:TextBox>
                            <asp:Label ID="lblPassword" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">SMTP Address
                        </td>
                        <td>:
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtSMTPAddress" runat="server" Width="200px"></asp:TextBox>
                            <asp:Label ID="lblSMTPAddress" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSMTPAddress"
                                ErrorMessage="SMTP Address Must Not be Empty"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">SMTP Port
                        </td>
                        <td>:
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtSMTPPort" runat="server" MaxLength="5" Width="50px"></asp:TextBox>
                            <asp:Label ID="lblSMTPPort" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSMTPAddress"
                                ErrorMessage="SMTP Address Must Not be Empty"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Enable SSL
                        </td>
                        <td>:
                        </td>
                        <td class="auto-style2">
                            <asp:CheckBox ID="chkSsl" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Using IIS Server</td>
                        <td>:
                        </td>
                        <td class="auto-style2">
                            <asp:CheckBox ID="chkIIS" runat="server" Text="Uncheck this if you are using exchange server or other smtp server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td class="auto-style2">
                            <asp:Label ID="lblNotif" runat="server" ForeColor="Silver" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td class="auto-style2">
                            <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click">Save</asp:LinkButton>
                            &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                            &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                            &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkTest" runat="server" OnClick="lnkTest_Click">Test</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </div>
        <br />
        <asp:HiddenField ID="hdnMode" runat="server" />
        <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
    </ContentTemplate>
</asp:UpdatePanel>

