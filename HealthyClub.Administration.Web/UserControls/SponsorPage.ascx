<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SponsorPage.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.SponsorPage" %>



<p>Perform variety of operations on the Sponsors as per your requirement.Below shows a list of Sponsors available. Click on any reward for its modification. Also perform the Deletion and addition of the Sponsors on the go</p>
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
        oTable = $('#tblSponsors').dataTable({
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

     <asp:Button CssClass="btn btn-grey" ID="lnkAdd" runat="server" OnClick="lnkAdd_Click" Text="Add"></asp:Button>
    &nbsp;&nbsp;

   
   
</span>
<div style="padding-top: 10px">
<asp:Repeater ID="rptSponsors" runat="server" OnItemDataBound="rptSponsors_ItemDataBound">
    <HeaderTemplate>
        <table id="tblSponsors" cellpadding="0" cellspacing="0" border="0" class="display">
            <thead>
                <tr>
                    <th></th>
                    <th>Sponsor Name</th>
                    <th>Address</th>
                    <th>Website</th>
                    <th>Phone Number</th>
                    <th>Status</th>
                    <th>Contract Expiry Date</th>
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
                <asp:HiddenField ID="hdnSponsorsID" runat="server" Value='<%# Eval("ID") %>'/>
           
                <asp:HyperLink ID="hlnkSponsorName" runat="server"><%# Eval("Name") %></asp:HyperLink>
            </td>
           <td>
            <asp:Label ID="Req_pts" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
            </td>
            
            <td>
            <asp:Label ID="Coupon_avl" runat="server" Text='<%# Eval("Website") %>'></asp:Label>
            </td>
              <td>
            <asp:Label ID="Label1" runat="server" Text='<%# Eval("PhoneNumber") %>'></asp:Label>
            </td>
            <td>
                <asp:HiddenField ID="hdnstatus" runat="server" Value='<%# Eval("Status") %>' />
                
               
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                
            </td>
                            
            
            <td><%# Eval("ContractExpiry") %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </tbody>
        <tfoot>
                <tr>
                    <th></th>
                    <th>Sponsor Name</th>
                    <th>Address</th>
                    <th>Website</th>
                    <th>Phone Number</th>
                    <th>Status</th>
                    <th>Contract Expiry Date</th>
                </tr>
        </tfoot>
        </table>
    </FooterTemplate>
</asp:Repeater>

 </div>



