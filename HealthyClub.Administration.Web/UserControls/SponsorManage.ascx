<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SponsorManage.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.SponsorManage" %>

<style>
    td {
        text-align: left;
        vertical-align: top;
    }
</style>
<script>
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode > 47) && (charCode < 58)) || (charCode == 8))
            return true;

        return false;
    }

</script>

<div id="showreward" runat="server" style="border:1px solid black; background-color:white;">
    <table class="form">
        <tr>
            <td class="col1">
                <label>
                    Sponsor Name</label>
            </td>
            <td>
                <asp:Label ID="lblspnName" runat="server"></asp:Label>
                <asp:TextBox ID="txtspnName" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="vertical-align: middle;">
                <label>
                    Address</label>
            </td>
            <td>
                <asp:Label ID="lbladdress" runat="server" Height="50px" Width="209px" TextMode="MultiLine"></asp:Label>
                <asp:TextBox ID="txtaddress" runat="server" Height="50px" Width="209px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

          <tr>
            <td class="col1">
                <label>
                    Website</label>
            </td>
            <td>
                <asp:Label ID="lblwebsite" runat="server"></asp:Label>
                <asp:TextBox ID="txtwebsite" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="col1">
                <label>
                    Phone Number</label>
            </td>
            <td>
                <asp:Label ID="lblnumber" runat="server"></asp:Label>
                <asp:TextBox ID="txtnumber" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <label>
                    Contract Expiry Date</label>
            </td>
            <td>
                <asp:Label ID="lblCalendarFrom" runat="server"></asp:Label>
                <asp:TextBox ID="txtCalendarFrom" runat="server"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="txtCalendarFrom_TextBoxWatermarkExtender" runat="server"
                    TargetControlID="txtCalendarFrom" WatermarkText="ExpiryDate" WatermarkCssClass="bodyText2">
                </ajaxToolkit:TextBoxWatermarkExtender>
                <ajaxToolkit:CalendarExtender ID="txtCalendarFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCalendarFrom">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" PromptCharacter=" "
                    MaskType="Date" TargetControlID="txtCalendarFrom">
                </ajaxToolkit:MaskedEditExtender>
            </td>
            <asp:Label ID="lbldate" ForeColor="Red" runat="server" Text="Please enter valid date" Visible="false"></asp:Label>
        </tr>

        </table>
    
    <br />
    <table class="form">
<tr><td>
    <asp:Button CssClass="btn btn-grey" ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click" Text="Update"></asp:Button>


    &nbsp;&nbsp;
    <asp:Button CssClass="btn btn-grey" ID="lnkAdd" runat="server" OnClick="lnkAdd_Click" Text="Add"></asp:Button>


    &nbsp;&nbsp;
    <asp:Button CssClass="btn btn-grey" ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" Text="Edit"></asp:Button>
    &nbsp;&nbsp;

    </td></tr>
    </table>


<br />





<div id="addeddiv" visible="false" runat="server">
    <asp:Label ID="rewardadded" ForeColor="red" runat="server"></asp:Label>
</div>
</div>




<asp:HiddenField ID="hdnFormMode" runat="server" />
<asp:HiddenField ID="hdnSponsorID" runat="server" />