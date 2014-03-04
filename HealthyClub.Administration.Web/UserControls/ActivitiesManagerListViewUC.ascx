<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitiesManagerListViewUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.ActivitiesManagerListViewUC1" %>
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

    function Print() {
        var btn = document.getElementById('<%= lnkPrint.ClientID %>');

         if (btn != null) {
             btn.click();
         }
         
    }

    function PrintPanel() {
        var panel = document.getElementById("<%=printsheet.ClientID%>");
        var printWindow = window.open('', '', 'height=500,width=1000');
        printWindow.document.write('<html><head><title>Print Your forms</title>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(panel.innerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        setTimeout(function () {
            printWindow.print();
        }, 500);
        return false;
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
    <asp:LinkButton ID="lnkSelectAll" runat="server" class="btn-icon btn-grey btn-plus" OnClick="lnkSelectAll_Click"><span></span>Select All</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkUnselectAll" runat="server" class="btn-icon btn-grey btn-minus" OnClick="lnkUnselectAll_Click"><span></span>Unselect All</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkConfirm" runat="server" Class="btn-icon btn-grey btn-check" OnClick="lnkConfirm_Click"><span></span>Confirm</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkDelete" runat="server" class="btn-icon btn-grey btn-cross" OnClick="lnkDelete_Click"><span></span>Delete</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkPrint" runat="server" class ="btn-icon btn-grey btn-cross" OnClick="lnkPrint_Click"><span></span>Get Form</asp:LinkButton>&nbsp;&nbsp;&nbsp;
    
    

    <asp:Button CssClass="btn btn-grey" ID="btnExtend" runat="server" Text="Extend Expired Activities" />
    <asp:Button CssClass="btn btn-grey" Style="display: none" ID="btnExtendAct" runat="server" OnClick="lnkExtendActivities_Click" Text="Extend Expired Activities"></asp:Button>
</span>
<div id="PrintForm" runat="server" visible="false">
    Froms are generated..<a href="javascript:void(0)" onclick="return PrintPanel();">Click Here</a>&nbsp;to print your attendance forms.
</div>
<div style="padding-top: 10px">
    <asp:ListView ID="listviewActivities" runat="server" OnItemDataBound="listviewActivities_ItemDataBound">
        <LayoutTemplate>
            <table id="tblActivities" cellpadding="0" cellspacing="0" border="0" class="display">
                <thead>
                    <tr>
                        <th>&nbsp;</th>
                        <th>Activity Provider</th>
                        <th>Activity Name</th>
                        <th>Active</th>
                        <th>Approved</th>
                        <th>Modified Date</th>
                        <th>Expiry Date</th>
                    </tr>
                </thead>
                <tbody>
                    <div id="ItemPlaceHolder" runat="server">
                    </div>
                </tbody>
                <tfoot>
                    <tr>
                        <th>&nbsp;</th>
                        <th>Activity Provider</th>
                        <th>Activity Name</th>
                        <th>Is Active</th>
                        <th>Is Approved</th>
                        <th>Modified Date</th>
                        <th>Expiry Date</th>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
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
                    <asp:Label ID="lblActID" runat="server" Text = '<%# Eval("ID") %>' Visible="false"></asp:Label>
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
        <EmptyDataTemplate>
            <div style="color: Red; margin: 10px 2px; font-size: 15px">No activities listed.</div>
        </EmptyDataTemplate>
    </asp:ListView>
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

<div id="printsheet" runat="server">

    <style>
        
        table.empty{
            display: inline-table;

        }
        
        
        table td.empty {
        
         width:20px;
        }

    </style>
<asp:ListView ID="Attendanceview" runat="server">
    <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
    <ItemTemplate>
   <div id="AttSheet" runat="server" style=" 
    padding-top:10px;
    width:550px;
   padding-bottom:10px;
    border:solid 1px;
    height:930px;
">
       <div id="head">
           <span style="font-size:large;padding-left:150px"><strong>Activity Attendance Report</strong></span>
       </div>
       <br />
        <div class="upper" style="padding-left:10px;">
            <table>
                <tbody>
                    
                    <tr>
                        <td>
                <asp:Image ID="QRcode" Style="width: 150px; max-height: 150px" runat="server"/>

                            </td>
                        <td></td>
                        <td align="left" style="text-align: justify; padding: 5px">
                            <span><strong>Activity ID: <asp:Label ID="lblActivityID" Text='<%# Eval("actid") %>' runat="server"></asp:Label></strong></span>
                        <br />
                        <span><strong>Activity Name: </strong> <asp:Label ID="lblActname" Text='<%# Eval("ActName") %>' runat="server"></asp:Label></span>

                            <br />
                        <span style="display:inline;"><strong> Date: </strong><table style="display:inline-table;"><tr><td class="empty">&nbsp;</td><td class="empty">&nbsp;</td><td>/</td><td class="empty">&nbsp;</td><td class="empty">&nbsp;</td><td>/</td><td class="empty">&nbsp;</td><td class="empty">&nbsp;</td></tr></table>

                        </span>

                        </td>
                        </tr>

            </tbody>
            </table>
            </div>
            <hr />
            <div id ="lowerleft" style="padding-left:90px;float:left;">
                <strong>IDs of the members attended this activity</strong>
                <br />
                <table style="border:solid 1px;margin-bottom:2px;">
                  <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr style="border:solid 1px;" runat="server">
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table>
                </div>
       <div id ="lowerright" style="padding-left:300px;margin-top:28px;">
                <table style="border:solid 1px;margin-bottom:2px;">
                  <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table><table style="border:solid 1px;margin-bottom:2px;">
                    <tr>
                      <td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td><td style="width:20px;">&nbsp;</td>
                  </tr></table>

        </div>
       </div>
    </ItemTemplate>
</asp:ListView>
</div>
