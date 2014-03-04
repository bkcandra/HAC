<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardsManager.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.RewardsManager" %>


<p>Perform variety of operations on the Rewards as per your requirement.Below shows a list of Rewards available. Click on any reward for its modification. Also perform the Deletion and addition of the Rewards on the go</p>
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
        oTable = $('#tblRewards').dataTable({
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
    <!--<button class="btn-icon btn-grey btn-plus" onclick="select()"><span></span>Select All</button>-->
    <asp:Button CssClass="btn btn-grey" ID="lnkSelectAll" runat="server" OnClick="lnkSelectAll_Click" Text="Select All"></asp:Button>&nbsp;&nbsp;&nbsp;
    
    <%--<button class="btn-icon btn-grey btn-plus" onclick="selectExp()"><span></span>Select Expired</button>
    <asp:Button CssClass="btn btn-grey" Style="display: none" ID="lnkSelectExps" runat="server" OnClick="lnkSelectExp_Click" Text="Select Expired"></asp:Button>&nbsp;&nbsp;&nbsp;
    --%>
    <!--<button class="btn-icon btn-grey btn-minus" Style="display: none" onclick="unselect()"><span></span>Unselect All</button>-->
    <asp:Button CssClass="btn btn-grey" ID="lnkUnselectAll" runat="server" Text="Unselect All" OnClick="lnkUnselectAll_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
    
   
        
    <!--<button class="btn-icon btn-grey btn-cross" onclick="DelAct(); "><span></span>Delete</button>-->
    <asp:Button CssClass="btn btn-grey" ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" Text="Delete"></asp:Button>

    &nbsp;&nbsp;

   
   
</span>
<div style="padding-top: 10px">
<asp:Repeater ID="rptRewards" runat="server" OnItemDataBound="rptRewards_ItemDataBound">
    <HeaderTemplate>
        <table id="tblRewards" cellpadding="0" cellspacing="0" border="0" class="display">
            <thead>
                <tr>
                    <th></th>
                    <th>Reward Name</th>
                    <th>Provider Name</th>
                    <th>Reward Type</th>
                    <th>Active</th>
                    <th>Required_Pts</th>
                    <th>Coupons Available</th>
                    <th>Coupons Left</th>
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
                <asp:HiddenField ID="hdnRewardsID" runat="server" Value='<%# Eval("ID") %>'/>
               <asp:Label ID="lblRwdID" runat="server" Visible="false" Text = '<%# Eval("ID") %>'></asp:Label>
                <asp:HyperLink ID="hlnkRewardName" runat="server"><%# Eval("RewardsName") %></asp:HyperLink>
            </td>
            <td>
                <asp:HiddenField ID="hdnProviderID" runat="server" Value='<%# Eval("ProviderID") %>'/>
                <asp:HyperLink ID="hlnkSponsorName" runat="server"><asp:Label ID ="lblsponsor" runat="server"></asp:Label></asp:HyperLink>
            </td>
            <td>
                <asp:HiddenField ID="hdnRwdtype" runat="server" Value='<%# Eval("RewardType") %>' />
                <asp:Label ID="lblRwdtype" runat="server"></asp:Label>

            </td>
            <td>
                <asp:HiddenField ID="Timesused" runat="server" Value='<%# Eval("NofTimeUsed") %>' />
                <asp:HiddenField ID="Usagetimes" runat="server" Value='<%# Eval("UsageTimes") %>' />
               
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                
            </td>
            <td>
            <asp:Label ID="Req_pts" runat="server" Text='<%# Eval("RequiredRewardPoint") %>'></asp:Label>
            </td>
            <td>
            <asp:Label ID="Coupon_avl" runat="server" Text='<%# Eval("UsageTimes") %>'></asp:Label>
            </td>
            <td>
            <asp:Label ID="Coupons_Left" runat="server"></asp:Label>
            </td>
            
            <td><%# Eval("ModifiedDateTime") %></td>
            <td><%# Eval("RewardExpiryDate") %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </tbody>
        <tfoot>
            <tr>
                    <th></th>
                    <th>Reward Name</th>
                    <th>Provider Name</th>
                    <th>Reward Type</th>
                    <th>Active</th>
                    <th>Required_Pts</th>
                    <th>Coupons Available</th>
                    <th>Coupons Left</th>
                    <th>Modified Date</th>
                    <th>Expiry Date</th>
            </tr>
        </tfoot>
        </table>
    </FooterTemplate>
</asp:Repeater>

 </div>



