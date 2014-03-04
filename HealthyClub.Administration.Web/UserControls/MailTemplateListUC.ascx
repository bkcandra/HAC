<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailTemplateListUC.ascx.cs"
    Inherits="HealthyClub.Administration.Web.UserControls.MailTemplateListUC" %>
<style>
    .left {
        float: left;
        width: 600px;
    }

    .right {
        float: right;
        width: 300px;
    }

    .Cleaner {
        clear: both;
        padding: 1px;
    }

    p {
        margin-right: 0cm;
        margin-left: 0cm;
        font-size: 12.0pt;
        font-family: "Times New Roman","serif";
    }

        p.MsoNormal {
            margin-bottom: .0001pt;
            font-size: 12.0pt;
            font-family: "Times New Roman","serif";
            margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }

    h1 {
        margin-right: 0cm;
        margin-left: 0cm;
        font-size: 24.0pt;
        font-family: "Times New Roman","serif";
        font-weight: bold;
    }

    .style1 {
        width: 100.0%;
        font-size: 10.0pt;
        font-family: "Times New Roman", serif;
        border-left-style: none;
        border-left-color: inherit;
        border-left-width: medium;
        border-right-style: none;
        border-right-color: inherit;
        border-right-width: medium;
        border-top-style: none;
        border-top-color: inherit;
        border-top-width: medium;
        border-bottom: 1.0pt dashed #EDEDED;
    }

    .style2 {
        width: 450.0pt;
        font-size: 10.0pt;
        font-family: "Times New Roman", serif;
    }

    .style3 {
        width: 420.0pt;
        font-size: 10.0pt;
        font-family: "Times New Roman", serif;
    }
</style>
<div class="left">
    <asp:Label ID="lblWarning" runat="server" ForeColor="Red" Visible="false"></asp:Label>
</div>
<div class="left">
    <asp:HyperLink ID="hlnkNewTemplate" runat="server" Text="New Template"></asp:HyperLink>
</div>
<div class="Cleaner">
</div>
<asp:GridView ID="gridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="gridView1_RowCommand"
    Width="100%">
    <EmptyDataTemplate>
        No Email Template Available
    </EmptyDataTemplate>
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditDynamicTemplate" Text="Edit"></asp:LinkButton>
                <asp:HiddenField ID="hdnEmailTemplateID" runat="server" Value='<%#Eval("ID") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkDelete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete Selected Template?');"
                    CommandName="DeleteDynamicTemplate" Text="Delete"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Template Name">
            <ItemTemplate>
                <asp:Label ID="lblTemplateName" runat="server" Text='<%#Eval("EmailName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Template Subject">
            <ItemTemplate>
                <asp:Label ID="lblTemplateTitle" runat="server" Text='<%#Eval("EmailSubject") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
<asp:HiddenField ID="hdnEmailType" runat="server" />
