<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitiesManagerUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.ActivitiesManagerUC" %>
<script src="../Scripts/DataTables-1.9.4/media/js/jquery.js"></script>
<script src="../Scripts/DataTables-1.9.4/media/js/jquery.dataTables.js"></script>
<style type="text/css" title="currentStyle">
    @import "../Content/DataTables-1.9.4/media/css/demo_page.css";
    @import "../Content/DataTables-1.9.4/media/css/demo_table_jui.css";
    @import "../Content/themes/smoothness/jquery-ui.css";
</style>
<asp:HiddenField ID="hdnError" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
        oTable = $('#tblActivities').dataTable({
            "oLanguage": { "sSearch": "Search Record:" },
            "iDisplayLength": 10,
            "bJQueryUI": true,
            "aaSorting": [[0, "asc"]],
            "sPaginationType": "full_numbers"
        });
    });

    function DelAct() {
        if (confirm('Are you certain you want to delete the selected activities? Deleted activity cannot be recovered.')) {
            var btDel = document.getElementById('<%= lnkDelete.ClientID %>');

            if (btDel != null) {
                btDel.click();
            }
        }
    }
    function Accept() {
        var btn = document.getElementById('<%= lnkConfirm.ClientID %>');

        if (btn != null) {
            btn.click();
        }
    }
    function select() {
        var btn = document.getElementById('<%= lnkSelectAll.ClientID %>');

        if (btn != null) {
            btn.click();
        }
    }

    //function selectExp() {
    //    var btn = document.getElementById('= lnkSelectExps.ClientID ');
    //
    //    if (btn != null) {
    //        btn.click();
    //    }
    //}

    function unselect() {
        var btn = document.getElementById('<%= lnkUnselectAll.ClientID %>');

        if (btn != null) {
            btn.click();
        }
    }
    function ExtendPopup() {
        //alert('Move all item in this folder to ----------------');
        var btnExtendAct = document.getElementById('<%= btnExtend.ClientID %>');

        if (btnExtendAct != null) {
            btnExtendAct.click();
        }

    }
    function ExtendAct() {
        //alert('Move all item in this folder to ----------------');
        var btnExtendAct = document.getElementById('<%= btnExtendAct.ClientID %>');

        if (btnExtendAct != null) {
            btnExtendAct.click();
        }

    }
</script>

<div id="divError" runat="server" class="message error" visible="false">
    <h5>Error!</h5>
    <p>
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
    </p>
</div>
<div id="divSuccess" runat="server" class="message success" visible="false">
    <h5>Success!</h5>
    <p>
        <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
    </p>
</div>

<span>
    <button class="btn-icon btn-grey btn-plus" onclick="select()"><span></span>Select All</button>
    <asp:Button CssClass="btn btn-grey" Style="display: none" ID="lnkSelectAll" runat="server" OnClick="lnkSelectAll_Click" Text="Select All"></asp:Button>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-icon btn-grey btn-plus"><span>LinkButton</span></asp:LinkButton>
    <%--<button class="btn-icon btn-grey btn-plus" onclick="selectExp()"><span></span>Select Expired</button>
    <asp:Button CssClass="btn btn-grey" Style="display: none" ID="lnkSelectExps" runat="server" OnClick="lnkSelectExp_Click" Text="Select Expired"></asp:Button>&nbsp;&nbsp;&nbsp;
    --%>
    <button class="btn-icon btn-grey btn-minus" onclick="unselect()"><span></span>Unselect All</button>
    <asp:Button CssClass="btn btn-grey" Style="display: none" ID="lnkUnselectAll" runat="server" Text="Unselect All" OnClick="lnkUnselectAll_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
    
    <button class="btn-icon btn-grey btn-check" onclick="Accept()"><span></span>Approve</button>
    <asp:Button CssClass="btn btn-grey" Style="display: none" ID="lnkConfirm" runat="server" OnClick="lnkConfirm_Click" Text="Confirm"></asp:Button>&nbsp;&nbsp;&nbsp;
    
    <button class="btn-icon btn-grey btn-cross" onclick="DelAct(); "><span></span>Delete</button>
    <asp:Button CssClass="btn btn-grey" Style="display: none" ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" Text="Delete"></asp:Button>

    &nbsp;&nbsp;

    <asp:Button CssClass="btn btn-grey" ID="btnExtend" runat="server" Text="Extend Expired Activities" Style="top: -3px; position: relative"></asp:Button>
    <asp:Button CssClass="btn btn-grey" Style="display: none" ID="btnExtendAct" runat="server" OnClick="lnkExtendActivities_Click" Text="Extend Expired Activities"></asp:Button>
</span>
<div style="padding-top: 10px">
    <asp:Repeater ID="rptActivities" runat="server" OnItemDataBound="rptActivities_ItemDataBound">
        <HeaderTemplate>
            <table id="tblActivities" cellpadding="0" cellspacing="0" border="0" class="display">
                <thead>
                    <tr>
                        <th></th>
                        <th>Activity Provider</th>
                        <th>Activity Name</th>
                        <th>Active</th>
                        <th>Approved</th>
                        <th>Modified Date</th>
                        <th>Expiry Date</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="gradeA" style="text-align: center">
                <td>
                    <asp:CheckBox ID="chkAct" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="hdnProviderID" runat="server" Value='<%# Eval("ProviderID") %>' />
                    <asp:HyperLink ID="hlnkProviderID" runat="server"><%# Eval("ProviderName") %></asp:HyperLink>
                </td>
                <td>
                    <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%# Eval("ID") %>' />
                    <asp:Label ID="lblActID" runat="server" Visible="false"></asp:Label>
                    <asp:HyperLink ID="hlnkActivityName" runat="server"><%# Eval("Name") %></asp:HyperLink>
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status") %>' />
                </td>
                <td>
                    <asp:HiddenField ID="hdnIsApproved" runat="server" Value='<%# Eval("isApproved") %>' />
                    <asp:Label ID="lblApproved" runat="server" Text=""></asp:Label>

                </td>
                <td><%# Eval("ModifiedDateTime") %></td>
                <td><%# Eval("ExpiryDate") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
        <tfoot>
            <tr>
                <th>Activity Provider</th>
                <th>Activity Name</th>
                <th>Is Active</th>
                <th>Is Approved</th>
                <th>Modified Date</th>
                <th>Expiry Date</th>
            </tr>
        </tfoot>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>



<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnExtend"
    PopupControlID="divPopupExtendAct" CancelControlID="btnCancelExt" OkControlID="btnOkExt"
    OnOkScript="ExtendAct()">
</ajaxToolkit:ModalPopupExtender>
<div id="divPopupExtendAct" runat="server">
    <div style="text-align: left; border: 1px solid black; width: 200px; padding: 10px; background-color: #FFFFFF;">
        Extend expired activities by
        <asp:TextBox ID="txtExtend" runat="server" TextMode="Number" Width="100px" MaxLength="2"></asp:TextBox>
        &nbsp;Day(s)
        <div style="text-align: right">
            <asp:Button ID="btnCancelExt" runat="server" Text="Cancel" Width="50px" />
            &nbsp;&nbsp;
        <asp:Button ID="btnOkExt" runat="server" Text="Ok" Width="50px" />
        </div>
    </div>
</div>
