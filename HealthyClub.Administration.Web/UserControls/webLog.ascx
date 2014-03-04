<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="webLog.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.webLog" %>
<script src="../Scripts/DataTables-1.9.4/media/js/jquery.js"></script>
<script src="../Scripts/DataTables-1.9.4/media/js/jquery.dataTables.js"></script>
<style type="text/css" title="currentStyle">
    @import "../Content/DataTables-1.9.4/media/css/demo_page.css";
    @import "../Content/DataTables-1.9.4/media/css/demo_table_jui.css";
</style>
<link href="../Content/themes/smoothness/jquery-ui.css" rel="stylesheet" />
<script type="text/javascript">
    function initdatatable() {
        oTable = $('#tblActivities').dataTable({
            "oLanguage": { "sSearch": "Search Record:" },
            "iDisplayLength": 10,
            "bJQueryUI": true,
            "aaSorting": [[2, "asc"]],
            "sPaginationType": "full_numbers"
        });

        oTable = $('#tblLogAction').dataTable({
            "oLanguage": { "sSearch": "Search Record:" },
            "iDisplayLength": 10,
            "bJQueryUI": true,
            "aaSorting": [[2, "asc"]],
            "sPaginationType": "full_numbers"
        });
        oTable = $('#tblWebLogGroup').dataTable({
            "oLanguage": { "sSearch": "Search Record:" },
            "iDisplayLength": 10,
            "bJQueryUI": true,
            "aaSorting": [[2, "asc"]],
            "sPaginationType": "full_numbers"
        });
        oTable = $('#tblActLog').dataTable({
            "oLanguage": { "sSearch": "Search Record:" },
            "iDisplayLength": 10,
            "bJQueryUI": true,
            "aaSorting": [[2, "asc"]],
            "sPaginationType": "full_numbers"
        });

    }
    $(document).ready(function () {
        initdatatable();
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        initdatatable();
    });
</script>
<div>
    Web Log
    <hr />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="float: left; width: 600px">
            <span>Administration Log</span>
            <asp:ListView ID="listviewActivities" runat="server" OnItemDataBound="listviewActivities_ItemDataBound" OnItemCommand="listviewActivities_ItemCommand">
                <LayoutTemplate>
                    <table id="tblActivities" cellpadding="0" cellspacing="0" border="0" class="display">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Log Category</th>
                                <th>Created Date</th>
                                <th>User Name</th>
                            </tr>
                        </thead>
                        <tbody>
                            <div id="ItemPlaceHolder" runat="server">
                            </div>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th></th>
                                <th>Log Category</th>
                                <th>Created Date</th>
                                <th>User Name</th>
                            </tr>
                        </tfoot>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="gradeA" style="text-align: center">
                        <td>
                            <asp:LinkButton ID="lnkDtails" runat="server" CommandName="ShowDetails" ClientIDMode="AutoID">Show Details</asp:LinkButton>
                            <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                            <asp:HiddenField ID="hdnMessage" runat="server" Value='<%#Eval("Message") %>' />
                            <asp:HiddenField ID="hdnCategory" runat="server" Value='<%#Eval("LogCategory") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDateTime") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("CreatedBy") %>'></asp:Label>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="ListViewWebLogGroup" runat="server" OnItemCommand="ListViewWebLogGroup_ItemCommand" OnItemDataBound="ListViewWebLogGroup_ItemDataBound">
                <LayoutTemplate>
                    <table id="tblWebLogGroup" cellpadding="0" cellspacing="0" border="0" class="display">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Activity</th>
                                <th>Reference Number</th>
                                <th>Email Sent Number</th>
                                <th>Last Sent</th>
                                <th>Activity Expiry Date</th>
                                <th>log Datetime</th>
                            </tr>
                        </thead>
                        <tbody>
                            <div id="ItemPlaceHolder" runat="server">
                            </div>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th></th>
                                <th>Activity</th>
                                <th>Reference Number</th>
                                <th>Email Sent Number</th>
                                <th>Last Sent</th>
                                <th>Activity Expiry Date</th>
                                <th>log Datetime</th>
                            </tr>

                        </tfoot>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="gradeA" style="text-align: center">
                        <td>
                            <asp:LinkButton ID="lnkDtails" runat="server" CommandName="ShowDetails" ClientIDMode="AutoID">Show Details</asp:LinkButton>
                            <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblActivityName" runat="server" Text="Category"></asp:Label>
                            <asp:HiddenField ID="hdnActID" runat="server" Value='<%#Eval("ActivityID") %>' />

                        </td>
                        <td>
                            <asp:Label ID="lblRefNumber" runat="server" Text='<%#Eval("ReferenceNumber") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEmailSent" runat="server" Text='<%#Eval("EmailSentNumber") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblLastSent" runat="server" Text='<%#Eval("LastSent") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblExpiryDate" runat="server" Text='<%#Eval("ExpiryDate") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDateTime") %>'></asp:Label>
                        </td>

                    </tr>
                </ItemTemplate>

            </asp:ListView>

        </div>
        <div style="float: right; width: 250px; height: 200px; text-align: center">
            <div style="top: 70px; position: relative">
                <asp:DropDownList ID="ddlMaintenanceType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaintenanceType_SelectedIndexChanged">
                    <asp:ListItem Text="Maintenance Log" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Activity Log" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnUpdateActivity" runat="server" Style="margin-top: 10px;"
                    Text="Update all Activity Status" OnClick="btnUpdateActivity_Click" />
            </div>
        </div>
        <div style="clear: both"></div>
        <div>
            <span>Log Action</span>
            <asp:ListView ID="ListViewActLog" runat="server" OnItemCommand="rptActLog_ItemCommand" OnItemDataBound="rptActLog_ItemDataBound">
                <LayoutTemplate>
                    <table id="tblActLog" cellpadding="0" cellspacing="0" border="0" class="display">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Log Category</th>
                                <th>Reference Number</th>
                                <th>Notification Type</th>
                                <th>Created Date</th>
                                <th>User Name</th>
                            </tr>
                        </thead>
                        <tbody>
                            <div id="ItemPlaceHolder" runat="server">
                            </div>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th></th>
                                <th>Log Category</th>
                                <th>Reference Number</th>
                                <th>Notification Type</th>
                                <th>Created Date</th>
                                <th>User Name</th>
                            </tr>
                        </tfoot>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="gradeA" style="text-align: center">
                        <td>
                            <asp:LinkButton ID="lnkDtails" runat="server" CommandName="ShowDetails" ClientIDMode="AutoID">Show Details</asp:LinkButton>
                            <asp:HiddenField ID="hdnActLogID" runat="server" Value='<%#Eval("ID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblActLogType" runat="server" Text="[Log Category]"></asp:Label>
                            <asp:HiddenField ID="hdnNote" runat="server" Value='<%#Eval("Note") %>' />
                            <asp:HiddenField ID="hdnMessage" runat="server" Value='<%#Eval("Message") %>' />
                            <asp:HiddenField ID="hdnLogType" runat="server" Value='<%#Eval("LogType") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblRefNumber" runat="server" Text='<%#Eval("ReferenceNumber") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnNotificationNumber" runat="server" Value='<%#Eval("NotificationNumber") %>' />
                            <asp:Label ID="lblNotificationNumber" runat="server" Text="[Notification]"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDateTime") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("CreatedBy") %>'></asp:Label>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="ListViewLogAction" runat="server" OnItemCommand="rptLogAction_ItemCommand" OnItemDataBound="rptLogAction_ItemDataBound">
                <LayoutTemplate>
                    <table id="tblLogAction" cellpadding="0" cellspacing="0" border="0" class="display">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Log Category</th>
                                <th>Created Date</th>
                                <th>User Name</th>
                            </tr>
                        </thead>
                        <tbody>
                            <div id="ItemPlaceHolder" runat="server">
                            </div>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th></th>
                                <th>Log Category</th>
                                <th>Created Date</th>
                                <th>User Name</th>
                            </tr>
                        </tfoot>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="gradeA" style="text-align: center">
                        <td>
                            <asp:LinkButton ID="lnkDtails" runat="server" CommandName="ShowDetails" ClientIDMode="AutoID">Show Details</asp:LinkButton>
                            <asp:HiddenField ID="hdnActionID" runat="server" Value='<%#Eval("ID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblActionLog" runat="server" Text="Category"></asp:Label>
                            <asp:HiddenField ID="hdnMessage" runat="server" Value='<%#Eval("Message") %>' />
                            <asp:HiddenField ID="hdnCategory" runat="server" Value='<%#Eval("ActionType") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("CreatedDateTime") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("CreatedBy") %>'></asp:Label>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>



        </div>
        <div style="float: left">
            Details<br />
            <asp:TextBox ID="txtMessage" runat="server" Enabled="false" TextMode="MultiLine"
                Width="425px" Height="100px"></asp:TextBox>
        </div>
        <div style="float: right; text-align: left">
            Note<br />
            <asp:TextBox ID="txtNote" runat="server" Enabled="false" TextMode="MultiLine"
                Width="425px" Height="100px"></asp:TextBox><br />
            <div style="float: left; text-align: center; width: 250px; padding-top: 4px;">
                <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div style="float: right;">
                <asp:HiddenField ID="hdnNoteMode" runat="server" />
                <asp:HiddenField ID="HdnNoteValue" runat="server" />
                <asp:HiddenField ID="hdnNoteActionID" runat="server" />
                <asp:Button ID="btnNoteEdit" runat="server" Text="Edit" OnClick="btnNoteEdit_Click" />
                &nbsp;
        <asp:Button ID="btnNoteSave" runat="server" Text="Save" OnClick="btnNoteSave_Click" />
                &nbsp;
        <asp:Button ID="btnNoteCancel" runat="server" Text="Cancel" OnClick="btnNoteCancel_Click" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
