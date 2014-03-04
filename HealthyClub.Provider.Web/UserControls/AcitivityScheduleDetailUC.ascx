<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AcitivityScheduleDetailUC.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.AcitivityScheduleDetailUC" %>
<div>
    <p class="title">
        Activity Schedule</p>
    <div id="divGridView" runat="server" visible="false" style="width: 80%">
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
            OnRowDataBound="GridView1_RowDataBound" OnSorting="GridView1_Sorting" Width="90%"
            PageSize="10">
            <EmptyDataRowStyle BackColor="#FCF7BA" />
            <EmptyDataTemplate>
                No Data Found.
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="Date" SortExpression="StartDateTime">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdnStartDateTime" runat="server" Value='<%#Eval("StartDateTime") %>' />
                        <asp:HiddenField ID="hdnEndDateTime" runat="server" Value='<%#Eval("EndDateTime") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Day">
                    <ItemTemplate>
                        <asp:Label ID="lblDay" runat="server" Text="Day"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Time">
                    <ItemTemplate>
                        <asp:Label ID="lblTime" runat="server" Text="Time"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="divWeeklyView" runat="server" visible="false">
    </div>
    <asp:HiddenField ID="hdnActivityID" runat="server" />
    <asp:HiddenField ID="hdnProviderLogin" runat="server" />
    <asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
</div>
